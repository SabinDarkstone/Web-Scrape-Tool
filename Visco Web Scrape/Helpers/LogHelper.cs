using System;

namespace Visco_Web_Scrape.Helpers {

	public static class LogHelper {

		public enum LogLevel {
			Debug,
			Info,
			Warning,
			Error,
			Fatal,

			Crawl
		}

		private static void Log(object message, LogLevel level) {
			System.Diagnostics.Debug.WriteLine("[{0}]: {1}", level.ToString().ToUpper(), message);
		}

		public static void Debug(object message) {
			Log(message, LogLevel.Debug);
		}

		public static void Info(object message) {
			Log(message, LogLevel.Info);
		}

		public static void Warning(object message) {
			Log(message, LogLevel.Warning);
		}

		public static void Error(object message) {
			Log(message, LogLevel.Error);
		}

		public static void Fatal(object message) {
			Log(message, LogLevel.Fatal);
		}

		public static void Crawl(object message) {
			Log(message, LogLevel.Crawl);
		}
	}

}