using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Utils;

namespace SimpleFileSearcher {
    public partial class FileViewerForm : Form {
        private Document Document { get { return reContent.Document; } }
        private string currentFilePath;
        private string currentSearchString;
        private List<DocumentRange> occurrenceRanges;
        private int currentOccurrenceIndex = -1;

        public FileViewerForm() {
            InitializeComponent();

            AdjustRichEditSettings();
        }

        public void ViewFile(string filePath, string searchString) {
            currentFilePath = filePath;
            currentSearchString = searchString;
            this.Text = string.Format("FileViewer ({0})", filePath);
            reContent.LoadDocument(filePath, DocumentFormat.PlainText);
        }

        private void reContent_DocumentLoaded(object sender, EventArgs e) {
            AdjustRichEditSettings();
            PrepareOccurrenceRangesAndHighlightThem();
            nextItem1_ItemClick(null, null);
        }

        private void AdjustRichEditSettings() {
            //reContent.ReadOnly = true;

            reContent.Appearance.Text.Font = new Font("Courier New", 10F);

            //reContent.ActiveViewType = RichEditViewType.Simple;
            //reContent.Views.SimpleView.Padding = new Padding(5, 5, 0, 0);
            //reContent.Views.SimpleView.BackColor = Color.FromArgb(216, 228, 248);

            reContent.ActiveViewType = RichEditViewType.Draft;
            reContent.Views.DraftView.Padding = new Padding(5, 5, 0, 0);
            reContent.Views.DraftView.BackColor = Color.FromArgb(216, 228, 248);
            reContent.Options.HorizontalRuler.Visibility = RichEditRulerVisibility.Hidden;
            Document.Sections[0].Page.Width = 10000;

            reContent.LookAndFeel.UseDefaultLookAndFeel = false;
            reContent.LookAndFeel.UseWindowsXPTheme = true;
        }

        private void PrepareOccurrenceRangesAndHighlightThem() {
            occurrenceRanges = new List<DocumentRange>(10);
            ISearchResult searchResult = Document.StartSearch(this.currentSearchString, SearchOptions.None, SearchDirection.Forward, Document.Range);

            while (searchResult.FindNext()) {
                occurrenceRanges.Add(searchResult.CurrentResult);
                CharacterProperties cp = Document.BeginUpdateCharacters(searchResult.CurrentResult);
                cp.Bold = true;
                cp.ForeColor = Color.Blue;
                //cp.BackColor = Color.Yellow;
                cp.Underline = UnderlineType.ThickSingle;
                Document.EndUpdateCharacters(cp);
            }
        }

        private void previousItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            currentOccurrenceIndex--;

            if (currentOccurrenceIndex <= -1)
                currentOccurrenceIndex = occurrenceRanges.Count - 1;

            ScrollToCurrentOccurrence();
        }

        private void nextItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            currentOccurrenceIndex++;

            if (currentOccurrenceIndex == occurrenceRanges.Count)
                currentOccurrenceIndex = 0;

            ScrollToCurrentOccurrence();
        }

        private void copyItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Clipboard.SetText(currentFilePath);
            System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(currentFilePath));
        }

        private void ScrollToCurrentOccurrence() {
            DocumentRange range = occurrenceRanges[currentOccurrenceIndex];

            Point docPoint = Units.PixelsToDocuments(new Point(10, 10), reContent.DpiX, reContent.DpiY);
            DocumentPosition pos = reContent.GetPositionFromPoint(docPoint);

            int diff = range.Start.ToInt() - pos.ToInt();

            Document.BeginUpdate();
            Document.CaretPosition = range.Start;
            ScrollLines(10, diff);
            Document.Selection = range;
            Document.EndUpdate();
        }

        void ScrollLines(int lineCount, int direction) {
            DocumentPosition pos = Document.CaretPosition;
            Paragraph paragraph = Document.GetParagraph(pos);
            Paragraph paragraphNext = paragraph;

            direction = Math.Sign(direction);

            if (direction == 0)
                return;

            for (int i = 0; i < lineCount; i++) {
                while (paragraphNext == paragraph) {
                    int intPos = pos.ToInt() + direction;
                    
                    pos = Document.CreatePosition(intPos < 0 ? 0 : intPos);
                    paragraphNext = Document.GetParagraph(pos);

                    if (direction > 0 && paragraphNext == Document.Paragraphs[Document.Paragraphs.Count - 1])
                        break;
                    if (direction < 0 && paragraphNext == Document.Paragraphs[0])
                        break;
                }
                paragraph = paragraphNext;
            }

            Document.CaretPosition = pos;
            reContent.ScrollToCaret(); 
        }
    }
}