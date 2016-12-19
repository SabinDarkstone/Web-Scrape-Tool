using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Visco_Web_Scrape.Properties;
using Settings = Visco_Web_Scrape.Objects.Settings;

namespace Visco_Web_Scrape.Helpers {

	public static class FileHelper {

		/// <summary>
		/// Load settings of previous session from binary file
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public static Settings LoadSettingsFromFile(string filename) {
			var stream = CheckForFile(filename);
			if (stream.Length == 0) {
				LogHelper.Warning("Settings file is empty, no settings are being loaded.");
				return null;
			}
			IFormatter formatter = new BinaryFormatter();
			LogHelper.Info("Reading settings from file.");
			var settings = formatter.Deserialize(stream) as Settings;
			LogHelper.Info("Operation completed.");
			stream.Close();

			return settings;
		}

		/// <summary>
		/// Write current session's settings to file
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="settings"></param>
		public static void WriteSettingsToFile(string filename, Settings settings) {
			var stream = CheckForFile(filename);
			IFormatter formatter = new BinaryFormatter();
			LogHelper.Info("Writing settings to file.");
			formatter.Serialize(stream, settings);
			LogHelper.Info("Operation completed.");
			stream.Close();
		}

		/// <summary>
		/// Combines write and load into one method.
		/// Essentially it takes the current settings in the session and writes them to the file.
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="settings"></param>
		/// <returns></returns>
		public static Settings CycleSettingsFile(string filename, Settings settings) {
			WriteSettingsToFile(filename, settings);
			return LoadSettingsFromFile(filename);
		}

		/// <summary>
		/// Checks to see if there is a settings file by the given name
		/// </summary>
		/// <param name="filename"></param>
		/// <returns>Returns the file stream if found, otherwise it creates a blank file and returns that stream.</returns>
		private static Stream CheckForFile(string filename) {
			if (!File.Exists(filename)) {
				LogHelper.Warning("Settings file not found.");
				var dialog = MessageBox.Show(Resources.SettingsFileNotFound, Resources.SettingsFileNotFoundCaption,
					MessageBoxButtons.YesNo);

				switch (dialog) {
					case DialogResult.No:
						LogHelper.Fatal("Settings file not found and new file not created.");
						throw new FileNotFoundException("Settings file");
					case DialogResult.Yes:
						LogHelper.Info("Creating new settins file.");
						return new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
				}
			}

			LogHelper.Info("Found previously used settings file.");
			return new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
		}
	}

}