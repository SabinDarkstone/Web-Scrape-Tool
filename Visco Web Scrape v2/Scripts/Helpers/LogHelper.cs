namespace Visco_Web_Scrape_v2.Scripts.Helpers {

	public partial class LogHelper {

		private static void Log(object messagge, LogLevel level) {
			System.Diagnostics.Debug.WriteLine("[{0}]: {1}", level.ToString().ToUpper(), messagge);
		}

		public static void Trace(object message) {
			Log(message, LogLevel.Trace);
		}

		public static void Debug(object message) {
			Log(message, LogLevel.Debug);
		}

		public static void Info(object message) {
			Log(message, LogLevel.Info);
		}

		public static void Warn(object message) {
			Log(message, LogLevel.Warn);
		}

		public static void Error(object message) {
			Log(message, LogLevel.Error);
		}

		public static void Fatal(object message) {
			Log(message, LogLevel.Fatal);
		}
	}

}