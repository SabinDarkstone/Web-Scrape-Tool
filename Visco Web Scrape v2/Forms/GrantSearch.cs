using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
		public CombinedResults Results { get; set; }
		public CancellationTokenSource Cts = new CancellationTokenSource();
		public Progress LastProgress { get; private set; }

		private BackgroundWorker worker;
		private readonly Job jobToRun;

		public GrantSearch(Configuration configuration, CombinedResults results, Job job) {
			InitializeComponent();

			Config = configuration;
			Results = results;
			jobToRun = job;
		}

		private void UpdateFields(Progress progress) {
			lblCurrentStatus.Text = progress.CurrentStatus.ToString();
			lblCurrentDomain.Text = progress.Domain ?? lblCurrentDomain.Text;
			lblCurrentUrl.Text = progress.Url ?? lblCurrentUrl.Text;
			lblPagesCrawledCount.Text = CrawlHelper.TotalPages.ToString();
			lblPagesSkippedCount.Text = CrawlHelper.SkippedPages.ToString();
			lblResultsFound.Text = progress.RelevantPageCount == null ? lblResultsFound.Text : progress.RelevantPageCount.ToString();
		}

		public void Stop(DoWorkEventArgs e) {
			e.Cancel = true;
		}

		private void btnSaveResults_Click(object sender, EventArgs e) {
			foreach (var website in Results.AllResults) {
				foreach (var result in website.ResultList) {
					LogHelper.Info("Found result on " + website.RootWebsite.Name + "\n" + result.PageUrl);
				}
			}

			DialogResult = DialogResult.OK;
			Hide();
		}

		// TODO: Fix completion status issue
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
				DialogResult = DialogResult.Cancel;
				Close();
			}
		}

		private void GrantSearch_Shown(object sender, EventArgs e) {
			// Initialize results
			Results.StartNewSearch();

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
			var myJob = e.Argument as Job;

			var myWorker = sender as BackgroundWorker;
			if (myJob == null) throw new NullReferenceException("No website list.");
			foreach (var website in myJob.WebsitesToCrawl) {
				// Reset current domain timer
				ElapsedTime.CurrentHours = 0;
				ElapsedTime.CurrentMinutes = 0;
				ElapsedTime.CurrentSeconds = 0;

				// Set website results as not being new
				var websiteResults = Results.AllResults.FirstOrDefault(i => i.RootWebsite.Url.Equals(website.Url));
				websiteResults?.StartNewSearch();

				// Initialize and run the crawler
				CrawlHelper.CurrentDomain++;
				var grantCrawler = new GrantCrawler(jobToRun, Config, website, myWorker, Cts, this);
				if (Cts.IsCancellationRequested) {
					lblCurrentStatus.Text = "Cancelled";
					LastProgress.CurrentStatus = Progress.Status.Cancelled;
					return;
				}

				if (grantCrawler.Successful) {
					var firstOrDefault = Results.AllResults.FirstOrDefault(i => i.RootWebsite.Url.Equals(website.Url));
					if (firstOrDefault != null) {
						firstOrDefault.CompletedSearch = true;
					}
				}
			}

		}

		private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			var progress = e.UserState as Progress;
			progressbar.Value = CrawlHelper.CurrentDomain;
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
