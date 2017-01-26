using System;
using System.Collections.Generic;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Scripts {

	[Serializable]
	public class Configuration {
	
		// TODO: Remove
		public enum ExportType { Excel, Plain, Xml }

		/// <summary>
		/// List of websites set by user
		/// </summary>
		public HashSet<Website> Websites { get; set; }

		/// <summary>
		/// Keyword whitelist
		/// </summary>
		public HashSet<Keyword> PageWords { get; set; }

		/// <summary>
		/// URL blacklist
		/// </summary>
		public HashSet<Keyword> UrlWords { get; set; }

		/// <summary>
		/// List of people and email addresses to send results files to
		/// </summary>
		public HashSet<Recipient> Recipients { get; set; }
		
		/// <summary>
		/// Number of webpages to crawl on each domain (maximum)
		/// </summary>
		public int PagesToCrawlPerDomain { get; set; }

		/// <summary>
		/// Whether to use URL blacklist to speed up search
		/// </summary>
		public bool EnableUrlFiltering { get; set; }

		/// <summary>
		/// Whether to include information about URL parts
		/// </summary>
		public bool EnableUrlAnalysis { get; set; }

		/// <summary>
		/// Whether to display the date of discovery for results on excel export
		/// </summary>
		public bool IncludeDate { get; set; }

		/// <summary>
		/// Whether to only export results that were newly found as of last search
		/// </summary>
		public bool OnlyNewResults { get; set; }

		/// <summary>
		/// Whether to automatically send email of excel file upon search completion
		/// </summary>
		public bool EnableSendEmail { get; set; }

		/// <summary>
		/// Filetype for data export
		/// </summary>
		public ExportType ExportMethod { get; set; }

		/// <summary>
		/// Whether to allow links as results
		/// </summary>
		public bool EnableLinkResultFilter { get; set; }

		/// <summary>
		/// Whether to allow results with the same contexts
		/// </summary>
		public bool EnableContextSensitiveFilter { get; set; }

		/// <summary>
		/// Whether to run searches automatically based on a schedule
		/// </summary>
		public bool EnableScheduler { get; set; }

		/// <summary>
		/// The time to start the search
		/// </summary>
		public int SearchHour { get; set; }

		/// <summary>
		/// The day of the week to start the search
		/// </summary>
		public Dictionary<DayOfWeek, bool> ScheduleDaysOfWeek { get; set; }

		/// <summary>
		/// The number of weeks between searches
		/// </summary>
		public int RepeatWeekCount { get; set; }

		public Configuration() {
			Websites = new HashSet<Website>();
			PageWords = new HashSet<Keyword>();
			UrlWords = new HashSet<Keyword>();
			Recipients = new HashSet<Recipient>();
			ScheduleDaysOfWeek = new Dictionary<DayOfWeek, bool> {
				{DayOfWeek.Sunday, false},
				{DayOfWeek.Monday, false},
				{DayOfWeek.Tuesday, false},
				{DayOfWeek.Wednesday, false},
				{DayOfWeek.Thursday, false},
				{DayOfWeek.Friday, false},
				{DayOfWeek.Saturday, false}
			};

		}
	}
}
