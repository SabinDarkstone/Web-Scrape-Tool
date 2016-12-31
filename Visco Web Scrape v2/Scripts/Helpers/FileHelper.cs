using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Visco_Web_Scrape_v2.Scripts.Helpers {

	public static class FileHelper {

		public static Configuration LoadConfiguration() {
			var stream = CheckForFile(Reference.Files.SettingsFile);
			if (stream.Length == 0) {
				return null;
			} else {
				IFormatter formatter = new BinaryFormatter();
				var config = formatter.Deserialize(stream) as Configuration;
				stream.Close();
				return config;
			}
		}

		public static void SaveConfiguration(Configuration config) {
			var stream = CheckForFile(Reference.Files.SettingsFile);
			IFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, config);
			stream.Close();
		}

		private static Stream CheckForFile(string filename) {
			if (!Directory.Exists(Reference.Files.AppFileDirectory)) {
				Directory.CreateDirectory(Reference.Files.AppFileDirectory);
			}

			if (!File.Exists(filename)) {
				var dialog = MessageBox.Show(Reference.Messages.NoSettingsFileFound, Reference.Messages.SettingsLoadError, MessageBoxButtons.YesNo, MessageBoxIcon.Error);

				switch (dialog) {
					case DialogResult.Yes:
						return new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
					default:
						throw new FileNotFoundException("Settings file");
				}
			} else {
				return new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
			}
		}
	}
}
