using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Console_Scraper.Helpers {

	public static class FileHelper {

		private static void CheckForDirectory() {
			if (!Directory.Exists(References.Files.AppFileDirectory)) {
				Directory.CreateDirectory(References.Files.AppFileDirectory);
			}
		}

		private static Stream CheckForFile(string filename) {
			CheckForDirectory();

			if (!File.Exists(filename)) {
				return new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
			} else {
				return new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
			}
		}

		public static Settings LoadSettings() {
			var stream = CheckForFile(References.Files.SettingsFile);
			if (stream.Length == 0) {
				stream.Close();
				return new Settings();
			}

			IFormatter formatter = new BinaryFormatter();
			var settings = formatter.Deserialize(stream) as Settings;
			stream.Close();
			return settings;
		}

		public static void SaveSettings(Settings settings) {
			var stream = CheckForFile(References.Files.SettingsFile);
			IFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, settings);
			stream.Close();
		}
	}

}