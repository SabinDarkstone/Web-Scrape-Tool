using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Properties;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Items;
using Visco_Web_Scrape_v2.Search.Process;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class GrantSearch : Form {

		public static class ElapsedTime {

			public static int TotalSeconds { get; set; }
			public static int TotalMinutes { get; set; }
			public static int TotalHours { get; set; }

			public static int CurrentSeconds { get; set; }
			public static int CurrentMinutes { get; set; }
			public static int CurrentHours { get; set; }
		}

		public Configuration Config;
		public List<SearchResults> AllResults { get; private set; }
		public CancellationTokenSource Cts = new CancellationTokenSource();
		public Progress LastProgress { get; private set; }

		private BackgroundWorker worker;
		private readonly Job jobToRun;
		private readonly Predicate<Website> predicate;

		public GrantSearch(Configuration configuration, Job job) {
			InitializeComponent();

			Config = configuration;
			jobToRun = job;
			predicate = AlreadyRunToday;

			foreach (var website in jobToRun.WebsitesToCrawl) {
				if (website.LastCrawlDate == DateTime.Today) {
					LogHelper.Debug(website.Name + "  already ran today");
				} else {
					LogHelper.Debug(website.Name + " not yet run today");
				}
			}
		}

		public bool AlreadyRunToday(Website arg) => arg.LastCrawlDate != DateTime.Today;

		private void UpdateFields(Progress progress) {
			LastProgress = progress;

			if (progress.CurrentStatus == Progress.Status.Cancelled) {
				return;
			}

			lblCurrentDomain.Text = progress.Domain ?? lblCurrentDomain.Text;
			lblCurrentUrl.Text = progress.Url ?? lblCurrentDomain.Text;
			lblPagesCrawledCount.Text = progress.TotalPageCount == null
				? lblPagesCrawledCount.Text
				: progress.TotalPageCount.ToString();
			lblResultsFound.Text = progress.RelevantPageCount == null
				? lblResultsFound.Text
				: progress.RelevantPageCount.ToString();
			lblPagesSkippedCount.Text = progress.SkippedPageCount.ToString();
			lblCurrentStatus.Text = progress.CurrentStatus.ToString();
			progressbar.Value = progress.DomainNumber + 1;
		}

		public void Stop(DoWorkEventArgs e) {
			e.Cancel = true;
		}

		private void btnSaveResults_Click(object sender, EventArgs e) {
			Config.LastCrawl.Date = DateTime.Now;

			DialogResult = DialogResult.OK;
			Hide();
		}

		private void btnCancelCrawl_Click(object sender, EventArgs e) {
			if (worker.IsBusy) {
				var check =
					MessageBox.Show(Resources.ConfirmCancelCrawl, Resources.ConfirmationRequired, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (check == DialogResult.No)
					return;

				worker.ReportProgress(0, new Progress(Progress.Status.Cancelling));
				Debug.WriteLine("Sending cancellation request...");
				worker.CancelAsync();
			} else {
				LastProgress.CurrentStatus = Progress.Status.Cancelled;
				Config.LastCrawl.CompletionStatus = "Canceled Early";
				DialogResult = DialogResult.Cancel;
				Close();
			}
		}

		public List<SearchResults> CompareLists(List<SearchResults> savedResults, bool onlyNewResults) {
			/* Old code
			var newList = savedResults;

			if (newList.Count == 0) return AllResults;

			// Mark all current results as old
			foreach (var domain in newList) {
				LogHelper.Debug(domain + " " + domain.ResultList.Count);
				if (domain.ResultList.Count != 0) {
					foreach (var website in domain.ResultList) {
						website.IsNew = false;
					}
				}
			}

			foreach (var recentDomain in AllResults) {
				foreach (var recentWebsite in recentDomain.ResultList) {

					var foundMatch = false;
					foreach (var domain in newList) {
						foreach (var website in domain.ResultList) {

							if (recentWebsite.Url.Equals(website.Url)) {
								foundMatch = true;
								break;
							}
						}
						if (foundMatch) {
							break;
						} else {
							if (recentDomain.Name.Equals(domain.Name)) {
								domain.AddNewResult(recentWebsite);
								LogHelper.Debug(domain.Name + " " + recentWebsite.Url);
							}
						}
					}
				}
			}

			return newList;
			*/

			var newList = new List<SearchResults>();

			foreach (var domain in Config.Websites) {
				// Create a new list with all the domains in the current settings file
				// This will include ALL domains even if new ones were added since the last search
				var results = new SearchResults(domain);

				// Check the previous search to see if there are results for the current domain
				foreach (var resDomain in savedResults) {
					// Ignore the domains that do not match
					if (!resDomain.RootUrl.Equals(domain.Url))
						continue;

					foreach (var result in resDomain.ResultList) {
						results.AddNewResult(result);
					}
				}

				// Check recent search results to see if there are any new results in the domain to add
				foreach (var newResDomain in AllResults) {
					// Ignore the domains that do no match
					if (!newResDomain.RootUrl.Equals(domain.Url))
						continue;

					// For the correct domain, go through each result
					foreach (var result in newResDomain.ResultList) {
						// Skip anything that already exists
						if (results.CheckExists(result))
							continue;

						// Add the result with the datetime of today (just in case) since it is determined
						// to be new to the list from the last result
						result.DateFound = DateTime.Today;
						results.AddNewResult(result);
					}
				}

				// Now that the domain is finished being populated, add it to newList
				newList.Add(results);
			}

			/* UNDONE: Moved to the export class
			if (onlyNewResults) {
				// Remove any results that do not have the datetime marked as today
				foreach (var domain in newList) {
					foreach (var result in domain.ResultList) {
						if (!result.DateFound.Equals(DateTime.Today)) {
							domain.ResultList.Remove(result);
						}
					}
				}
			}
			*/

			return newList;
		}

		private void GrantSearch_Shown(object sender, EventArgs e) {
			// Initialize results
			AllResults = new List<SearchResults>();

			// Initialize the background worker
			worker = new BackgroundWorker {
				WorkerReportsProgress = true,
				WorkerSupportsCancellation = true
			};

			// Register worker events
			worker.DoWork += worker_DoWork;
			worker.ProgressChanged += worker_ProgressChanged;
			worker.RunWorkerCompleted += worker_RunWorkerCompleted;

			// Run the worker
			worker.RunWorkerAsync(jobToRun);
		}

		private void worker_DoWork(object sender, DoWorkEventArgs e) {
			// Update UI to match "in-progress" conditions
			try {
				btnCancelCrawl.Invoke(new MethodInvoker(delegate { btnCancelCrawl.Text = "Stop"; }));

				btnCancelCrawl.Enabled = true;
				btnSaveResults.Enabled = false;
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}

			// Go through each domain and setup a crawler to crawl it
			var websites = e.Argument as Job;
			var crawlQueue = websites.WebsitesToCrawl.FindAll(predicate);

			var myWorker = sender as BackgroundWorker;
			if (websites == null) throw new NullReferenceException("No website list.");
			foreach (var website in crawlQueue) {
				// Reset current domain timer
				ElapsedTime.CurrentHours = 0;
				ElapsedTime.CurrentMinutes = 0;
				ElapsedTime.CurrentSeconds = 0;

				// Initialize and run the crawler
				var grantCrawler = new GrantCrawler(Config, website, myWorker, Cts);
				if (Cts.IsCancellationRequested) {
					lblCurrentStatus.Text = "Cancelled";
					LastProgress.CurrentStatus = Progress.Status.Cancelled;
					return;
				}

				if (grantCrawler.Successful) {
					website.LastCrawlDate = DateTime.Today;
					AllResults.Add(grantCrawler.Results);
				}
			}

		}

		private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			var progress = e.UserState as Progress;
			UpdateFields(progress);
		}

		private void timerTotal_Tick(object sender, EventArgs e) {
			ElapsedTime.TotalSeconds++;
			ElapsedTime.CurrentSeconds++;

			if (ElapsedTime.TotalSeconds == 60) {
				ElapsedTime.TotalSeconds = 0;
				ElapsedTime.TotalMinutes++;

				if (ElapsedTime.TotalMinutes == 60) {
					ElapsedTime.TotalMinutes = 0;
					ElapsedTime.TotalHours++;
				}
			}

			if (ElapsedTime.CurrentSeconds == 60) {
				ElapsedTime.CurrentSeconds = 0;
				ElapsedTime.CurrentMinutes++;

				if (ElapsedTime.CurrentMinutes == 60) {
					ElapsedTime.CurrentMinutes = 0;
					ElapsedTime.CurrentHours++;
				}
			}

			UpdateTimers();
		}

		private void UpdateTimers() {
			string currSec, currMin, currHr;
			string totSec, totMin, totHr;

			if (ElapsedTime.CurrentSeconds < 10) {
				currSec = "0" + ElapsedTime.CurrentSeconds;
			} else {
				currSec = ElapsedTime.CurrentSeconds.ToString();
			}

			if (ElapsedTime.CurrentMinutes < 10) {
				currMin = "0" + ElapsedTime.CurrentMinutes;
			} else {
				currMin = ElapsedTime.CurrentMinutes.ToString();
			}

			if (ElapsedTime.CurrentHours < 10) {
				currHr = "0" + ElapsedTime.CurrentHours;
			} else {
				currHr = ElapsedTime.CurrentHours.ToString();
			}

			if (ElapsedTime.TotalSeconds < 10) {
				totSec = "0" + ElapsedTime.TotalSeconds;
			} else {
				totSec = ElapsedTime.TotalSeconds.ToString();
			}

			if (ElapsedTime.TotalMinutes < 10) {
				totMin = "0" + ElapsedTime.TotalMinutes;
			} else {
				totMin = ElapsedTime.TotalMinutes.ToString();
			}

			if (ElapsedTime.TotalHours < 10) {
				totHr = "0" + ElapsedTime.TotalHours;
			} else {
				totHr = ElapsedTime.TotalHours.ToString();
			}

			lblCurrentDomainTime.Text = currHr + ":" + currMin + ":" + currSec;
			lblTotalTime.Text = totHr + ":" + totMin + ":" + totSec;
		}

		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			btnCancelCrawl.Text = "Cancel";
			btnCancelCrawl.Enabled = false;
			btnSaveResults.Enabled = true;

			lblCurrentDomain.Text = "";
			lblCurrentUrl.Text = "";
		}

		private void GrantSearch_Load(object sender, EventArgs e) {
			// Clear out placeholder fields
			lblCurrentDomain.Text = "";
			lblCurrentUrl.Text = "";
			lblPagesCrawledCount.Text = "";
			lblResultsFound.Text = "";

			// Set progress bar maximum to one more than the number of websites
			progressbar.Maximum = jobToRun.WebsitesToCrawl.Count + 1;
		}
	}
}
