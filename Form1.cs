using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Text;

// http://www.codeproject.com/KB/files/filesearcher.aspx
// http://www.csharp-examples.net/get-files-from-directory/
// http://msdn.microsoft.com/en-us/library/ms143316.aspx
// http://www.dotnetperls.com/backgroundworker
// http://msdn.microsoft.com/en-us/library/cc221403(v=vs.95).aspx
// http://msdn.microsoft.com/en-us/library/aa288453(v=vs.71).aspx
// http://stackoverflow.com/questions/444798/case-insensitive-containsstring
// http://www.devexpress.com/Support/Center/p/Q234936.aspx
// http://stackoverflow.com/questions/4919969/disabling-richtextbox-autoscroll
// http://blog.guymahieu.com/2006/11/15/systemdelegate-is-not-a-delegate-type/
// http://www.jonasjohn.de/snippets/csharp/xmlserializer-example.htm

namespace SimpleFileSearcher {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            ffrControl.FileSelected += new FileSelectedEventHandler(ffrControl_FileSelected);
        }

        private void Form1_Load(object sender, EventArgs e) {
            UserSettings.Load();
            UpdateControls(true);

            var args = Environment.GetCommandLineArgs();

            if (args != null && args.Length > 1) {
                cbBasePath.Text = args[1];
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            UpdateControls(false);
            UserSettings.Save();
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            if (!bwSearcher.IsBusy) {
                if (!Directory.Exists(cbBasePath.Text)) {
                    MessageBox.Show(string.Format("Directory '{0}' does not exist", cbBasePath.Text), "Error");
                    return;
                }
                
                btnSearch.Enabled = false;
                btnCancel.Enabled = true;
                btnCancel.BringToFront();
                ffrControl.ClearContent();
                ffrControl.CurrentSearchString = tbContentPattern.Text;

                if (!cbBasePath.Items.Contains(cbBasePath.Text))
                    cbBasePath.Items.Add(cbBasePath.Text);

                bwSearcher.RunWorkerAsync(new SearchQueryParameters(
                    cbBasePath.Text,
                    cbFilePatterns.Text,
                    tbContentPattern.Text));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            bwSearcher.CancelAsync();
        }

        private void bwSearcher_DoWork(object sender, DoWorkEventArgs e) {
            SearchQueryParameters searchQueryParameters = e.Argument as SearchQueryParameters;
            string[] filePatterns = searchQueryParameters.FilePatterns.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string[][] files = new string[filePatterns.Length][]; 

            for (int i = 0; i < filePatterns.Length; i++)
                files[i] = Directory.GetFiles(searchQueryParameters.BasePath, filePatterns[i], SearchOption.AllDirectories);

            int count = 0, n = 0;
            double k = 1;

            for (int i = 0; i < files.Length; i++) {
                count += files[i].Length;
            }

            for (int i = 0; i < files.Length; i++) {
                for (int j = 0; j < files[i].Length; j++) {

                    if (bwSearcher.CancellationPending) {
                        e.Cancel = true;
                        return;
                    }

                    string currentFile = files[i][j];
                    StreamReader streamReader = new StreamReader(currentFile, Encoding.Default);
                    string fileContent = streamReader.ReadToEnd();
                    streamReader.Dispose();

                    int firstMatchIndex = fileContent.IndexOf(searchQueryParameters.ContentPattern, StringComparison.OrdinalIgnoreCase);

                    if (firstMatchIndex >= 0) {
                        string relativePath = currentFile.Substring(searchQueryParameters.BasePath.Length);

                        int fullLineStart = fileContent.LastIndexOf(Environment.NewLine, firstMatchIndex);
                        int fullLineEnd = fileContent.IndexOf(Environment.NewLine, firstMatchIndex);

                        if (fullLineStart != - 1)
                            fullLineStart += Environment.NewLine.Length;

                        if (fullLineStart == -1)
                            fullLineStart = Math.Max(firstMatchIndex - 5, 0);
                        if (fullLineEnd == -1)
                            fullLineEnd = Math.Min(firstMatchIndex + searchQueryParameters.ContentPattern.Length + 5, fileContent.Length);
  
                        string fullLine = fileContent.Substring(fullLineStart, fullLineEnd - fullLineStart);

                        ffrControl.Invoke(new FileFound(UpdateFindResults), new object[] { relativePath, fullLine, ++n });
                    }
                    
                    //System.Threading.Thread.Sleep(150);
                    bwSearcher.ReportProgress((int)Math.Round((k++ / count) * 100));
                }

            }
        }

        private void bwSearcher_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            pbProgress.Value = e.ProgressPercentage;
        }

        private void bwSearcher_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            btnSearch.Enabled = true;
            btnCancel.Enabled = false;
            btnSearch.BringToFront();

            if (e.Cancelled)
                MessageBox.Show("Search cancelled.");
        }

        private void UpdateFindResults(string filePath, string fullLine, int fileNumber) {
            ffrControl.AppendFileInfo(filePath, fullLine);

            this.Text = string.Format("SimpleFileSearcher ({0} items found)", fileNumber);
        }

        private void btnShowExplorer_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start(cbBasePath.Text);
        }

        private void btnRemoveBasePathItem_Click(object sender, EventArgs e) {
            int index = cbBasePath.Items.IndexOf(cbBasePath.Text);

            if (index != -1) {
                cbBasePath.Items.RemoveAt(index);

                if (index == cbBasePath.Items.Count)
                    index--;
                
                cbBasePath.SelectedIndex = index;
            }
        }

        void ffrControl_FileSelected(object sender, FileSelectedEventArgs e) {
            /*System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo();

            processStartInfo.FileName = "notepad.exe";
            processStartInfo.Arguments = cbBasePath.Text + e.FilePath;

            System.Diagnostics.Process process = System.Diagnostics.Process.Start(processStartInfo);*/

            FileViewerForm fileViewForm = new FileViewerForm();

            fileViewForm.ViewFile(cbBasePath.Text + e.FilePath, ffrControl.CurrentSearchString);
            fileViewForm.Show();
        }

        private void UpdateControls(bool fromSettingsToControls) {
            if (fromSettingsToControls) {
                cbBasePath.Text = UserSettings.Instance.BasePath;
                tbContentPattern.Text = UserSettings.Instance.ContentPattern;
                cbFilePatterns.Items.Clear();
                for (int i = 0; i < UserSettings.Instance.FilePatterns.Count; i++) {
                    cbFilePatterns.Items.Add(UserSettings.Instance.FilePatterns[i]);
                }
                cbBasePath.Items.Clear();
                for (int i = 0; i < UserSettings.Instance.MRUItems.Count; i++) {
                    cbBasePath.Items.Add(UserSettings.Instance.MRUItems[i].Value);
                }
                cbFilePatterns.SelectedIndex = Math.Max( UserSettings.Instance.SelectedFilePatternIndex, 0);
            }
            else {
                UserSettings.Instance.BasePath = cbBasePath.Text;
                UserSettings.Instance.ContentPattern = tbContentPattern.Text;
                UserSettings.Instance.FilePatterns.Clear();
                for (int i = 0; i < cbFilePatterns.Items.Count; i++) {
                    UserSettings.Instance.FilePatterns.Add(cbFilePatterns.Items[i].ToString());
                }
                UserSettings.Instance.MRUItems.Clear();
                for (int i = 0; i < cbBasePath.Items.Count; i++) {
                    UserSettings.Instance.MRUItems.Add(new MRUItem(cbBasePath.Items[i].ToString(), true));
                }
                UserSettings.Instance.SelectedFilePatternIndex = cbFilePatterns.SelectedIndex;
            }
        }
    }

    public delegate void FileFound(string filePath, string fullLine, int fileNumber);

    public class SearchQueryParameters {
        public string BasePath { get; set; }
        public string FilePatterns { get; set; }
        public string ContentPattern { get; set; }

        public SearchQueryParameters(string basePath, string filePatterns, string contentPattern) {
            this.BasePath = basePath;
            this.FilePatterns = filePatterns;
            this.ContentPattern = contentPattern;
        }
    }
}