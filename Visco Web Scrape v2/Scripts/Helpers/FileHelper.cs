using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Properties;
using Visco_Web_Scrape_v2.Search.Items;

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

		public static CombinedResults LoadResults() {
			var stream = CheckForFile(Reference.Files.ResultsFile);
			if (stream.Length == 0) {
				LogHelper.Debug("Results file is empty");
				return null;
			} else {
				IFormatter formatter = new BinaryFormatter();
				var res = formatter.Deserialize(stream) as CombinedResults;
				stream.Close();
				LogHelper.Debug("Loading results file");
				return res;
			}
		}

		public static void SaveResults(CombinedResults results) {
			LogHelper.Debug("Saving results file...");
			var stream = CheckForFile(Reference.Files.ResultsFile);
			IFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, results);
			LogHelper.Debug("Results file saved with length: " + stream.Length);
			stream.Close();
		}

		private static Stream CheckForFile(string filename) {
			if (!Directory.Exists(Reference.Files.AppFileDirectory)) {
				Directory.CreateDirectory(Reference.Files.AppFileDirectory);
			}

			if (!File.Exists(filename)) {
				DialogResult dialog = DialogResult.None;
				if (filename == Reference.Files.SettingsFile) {
					dialog = MessageBox.Show(Resources.NoSettingsFound, Resources.ConfirmationRequired, MessageBoxButtons.YesNo,
						MessageBoxIcon.Error);
				} else if (filename == Reference.Files.ResultsFile) {
					dialog = MessageBox.Show(Resources.NoResultsFileFound, Resources.ConfirmationRequired, MessageBoxButtons.YesNo,
						MessageBoxIcon.Error);
				}

				switch (dialog) {
					case DialogResult.Yes:
						return new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
					case DialogResult.No:
						throw new FileNotFoundException(Resources.SettingsFileName);
					default:
						MessageBox.Show(Resources.UnknownError, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return null;
				}
			} else {
				return new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
			}
		}
	}

}