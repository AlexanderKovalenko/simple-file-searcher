using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;

namespace SimpleFileSearcher {
    public partial class FindFilesResultControl : UserControl {
        private Document Document { get { return reContent.Document; } }

        public event FileSelectedEventHandler FileSelected;

        public string CurrentSearchString { get; set; }

        public FindFilesResultControl() {
            InitializeComponent();
            AdjustRichEditSettings();
        }

        void reContent_DocumentLoaded(object sender, EventArgs e) {
            AdjustRichEditSettings();
        }

        private void AdjustRichEditSettings() {
            //reContent.ReadOnly = true;
            
            reContent.Appearance.Text.Font = new Font("Courier New", 10F);

            //reContent.ActiveViewType = RichEditViewType.Simple;
            //reContent.Views.SimpleView.Padding = new Padding(5, 5, 0, 0);
            //reContent.Views.SimpleView.BackColor = Color.FromArgb(216, 228, 248);

            reContent.ReadOnly = true;

            reContent.ActiveViewType = RichEditViewType.Draft;
            reContent.Views.DraftView.Padding = new Padding(5, 5, 0, 0);
            reContent.Views.DraftView.BackColor = Color.FromArgb(216, 228, 248);
            reContent.Options.HorizontalRuler.Visibility = RichEditRulerVisibility.Hidden;
            Document.Sections[0].Page.Width = 10000;

            Document.ParagraphStyles["Normal"].LineSpacingType = ParagraphLineSpacing.Double;

            reContent.LookAndFeel.UseDefaultLookAndFeel = false;
            reContent.LookAndFeel.UseWindowsXPTheme = true;
        }

        public void AppendFileInfo(string filePath, string matchedLine) {
            // Append file info line to document
            DocumentRange lineRange = Document.AppendText(filePath + "  |  " + matchedLine + Environment.NewLine);

            int start = lineRange.Start.ToInt() + filePath.Length + 5;
            HighlightOccurances(Document.CreateRange(start,  lineRange.End.ToInt() - start));

            // Enables "auto-follow" feature if carent is positioned at the end of the document
            if (Document.CaretPosition.ToInt() == Document.Range.End.ToInt() - 1)
                reContent.ScrollToCaret();
        }

        private void HighlightOccurances(DocumentRange lineRange) {
            ISearchResult searchResult = Document.StartSearch(this.CurrentSearchString, SearchOptions.None, SearchDirection.Forward, lineRange);

            while (searchResult.FindNext()) {
                CharacterProperties cp = Document.BeginUpdateCharacters(searchResult.CurrentResult);
                cp.Bold = true;
                cp.ForeColor = Color.Blue;
                //cp.BackColor = Color.Yellow;
                cp.Underline = UnderlineType.ThickSingle;
                Document.EndUpdateCharacters(cp);
            }
        }

        public void ClearContent() {
            reContent.Text = string.Empty;
        }

        private void followItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Document.CaretPosition = Document.CreatePosition(Document.Range.End.ToInt() - 1);
        }

        private void reContent_DoubleClick(object sender, EventArgs e) {
            Paragraph paragraph = Document.GetParagraph(Document.CaretPosition);
            string paragraphText = Document.GetText(paragraph.Range);
            string filePath = paragraphText.Substring(0, paragraphText.IndexOf("  |  "));

            OnFileSelected(new FileSelectedEventArgs(filePath));
        }

        protected virtual void OnFileSelected(FileSelectedEventArgs e) {
            if (FileSelected != null)
                FileSelected(this, e);
        }
    }

    public delegate void FileSelectedEventHandler(object sender, FileSelectedEventArgs e);

    public class FileSelectedEventArgs : EventArgs {
        private string filePath;

        public FileSelectedEventArgs(string filePath) {
            this.filePath = filePath;
        }

        public string FilePath {
            get {
                return filePath;
            }
        }
    } 
}
