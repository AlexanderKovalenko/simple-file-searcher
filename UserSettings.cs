using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System;

namespace SimpleFileSearcher {
    public class UserSettings {
        public string BasePath { get; set; }
        private List<string> filePatterns;
        public List<string> FilePatterns {
            get {
                if (filePatterns == null)
                    filePatterns = new List<string>();
                return filePatterns;
            }
            set { filePatterns = value; }
        }
        private List<MRUItem> mruItems;
        public List<MRUItem> MRUItems {
            get {
                if (mruItems == null)
                    mruItems = new List<MRUItem>();
                return mruItems;
            }
            set { mruItems = value; }
        }
        private int selectedFilePatternIndex = -1;
        public int SelectedFilePatternIndex {
            get { return selectedFilePatternIndex; }
            set { selectedFilePatternIndex = value; }
        }
        public string ContentPattern { get; set; }

        #region Singleton pattern
        
        private UserSettings() { } // Serialization constructor

        private static UserSettings instance;
        
        public static UserSettings Instance {
            get {
                if (instance == null) {
                    instance = new UserSettings();
                }
                return instance;
            }
        }

        #endregion Singleton pattern

        private static string path = Application.StartupPath + "\\UserSettings.xml";

        public static void Save() {
            if (!File.Exists(path))
                File.Create(path).Close();
            
            XmlSerializer xmlFormat = new XmlSerializer(typeof(UserSettings));

            using (Stream fStream = new FileStream(path, FileMode.Truncate, FileAccess.Write)) {
                xmlFormat.Serialize(fStream, UserSettings.Instance);
            }
        }

        public static void Load() {
            if (!File.Exists(path))
                UserSettings.Save();
            
            XmlSerializer xmlFormat = new XmlSerializer(typeof(UserSettings));

            using (Stream fStream = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                UserSettings.instance = (UserSettings)xmlFormat.Deserialize(fStream);
            }

            EnsureDefaultSettings();
        }

        private static void EnsureDefaultSettings() {
            if (string.IsNullOrEmpty(UserSettings.Instance.BasePath))
                UserSettings.Instance.BasePath = Application.StartupPath;

            if (string.IsNullOrEmpty(UserSettings.Instance.ContentPattern))
                UserSettings.Instance.ContentPattern = "Test";

            if (UserSettings.Instance.FilePatterns.Count == 0) {
                UserSettings.Instance.FilePatterns.Add("*.cs;");
                UserSettings.Instance.SelectedFilePatternIndex = 0;
            }

            if (UserSettings.Instance.MRUItems.Count == 0)
                UserSettings.Instance.MRUItems.Add(new MRUItem(UserSettings.Instance.BasePath, true));
        }
    }

    public class MRUItem : IEquatable<MRUItem> {
        public string Value { get; set; }
        public bool Pinned { get; set; }

        public MRUItem(string value, bool pinned) {
            this.Value = value;
            this.Pinned = pinned;
        }

        private MRUItem() { } // Serialization constructor

        #region IEquatable<MRUItem> Members

        public bool Equals(MRUItem other) {
            return string.Compare(this.Value.ToLower(), other.Value.ToLower()) == 0;
        }

        #endregion
    }
}
