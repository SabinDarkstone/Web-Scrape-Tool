using System;
using System.ComponentModel;
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

		public TimeSpan CurrentDomain;
		public TimeSpan TotalSearch;
		public Configuration Config;
		public CombinedResults Results { get; set; }
		public CancellationTokenSource Cts = new CancellationTokenSource();

		private BackgroundWorker worker;
		private readonly Job jobToRun;
		private readonly MainForm parent;

		public GrantSearch(Configuration configuration, CombinedResults results, Job job, MainForm parent) {
			InitializeComponent();

			Config = configuration;
			Results = results;
			jobToRun = job;
			this.parent = parent;
		}

		private void UpdateFields(Progress progress) {
			try {
				lblCurrentStatus.Text = progress.SearchStatus.ToString();
				lblCurrentDomain.Text = progress.Website.Name;
				lblCurrentUrl.Text = progress.CurrentUrl;
				lblPagesCrawledCount.Text = progress.SearchedPages.ToString();
				lblPagesSkippedCount.Text = progress.IgnoredPages.ToString();
				lblResultsFound.Text = progress.ResultsFound.ToString();
			} catch (Exception e) {
				LogHelper.Fatal(e.Message);
				LogHelper.Trace(e.StackTrace);
			}

		}

		public void Stop(DoWorkEventArgs e) {
			e.Cancel = true;
		}

		private void btnSaveResults_Click(object sender, EventArgs e) {
			LogHelper.Debug("Saving results...");

			DialogResult = DialogResult.OK;
			Hide();
		}

		// TODO
		private void btnCancelCrawl_Click(object sender, EventArgs e) {
			if (worker.IsBusy) {
				var confirm = MessageBox.Show(Resources.ConfirmCancelCrawl, Resources.ConfirmationRequired, MessageBoxButtons.YesNo,
					MessageBoxIcon.Question);

				if (confirm == DialogResult.No) return;

				worker.CancelAsync();
				timerTotal.Enabled = false;
			}
		}

		private void GrantSearch_Shown(object sender, EventArgs e) {
			// Initialize results
			Results.Begin();

			// Prepare progress bar
			progressbar.Maximum = jobToRun.WebsitesToCrawl.Count + 1;

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

		/// <summary>
		/// Iterates through the list of websites in the jobToRun.
		/// Handles what happens before and after the crawl is completed.
		/// </summary>
		private void worker_DoWork(object sender, DoWorkEventArgs e) {
			// Update UI to match "in-progress" conditions
			btnCancelCrawl.Invoke(new MethodInvoker(delegate { btnCancelCrawl.Text = Resources.ButtonStopText; }));
			btnCancelCrawl.Enabled = true;
			btnSaveResults.Enabled = false;

			var myJob = e.Argument as Job;
			if (myJob == null) throw new NullReferenceException("No website list.");

			var myWorker = sender as BackgroundWorker;
			Results.Begin();

			double i = 0;
			foreach (var website in myJob.WebsitesToCrawl) {
				i++;
				parent.PercentSearchComplete = (i / myJob.WebsitesToCrawl.Count) * 100;
				progressbar.Invoke(new MethodInvoker(
					delegate { progressbar.Value = jobToRun.WebsitesToCrawl.ToList().IndexOf(website) + 1; }));
				CurrentDomain = TimeSpan.Zero;

				// Check for cancellation
				if (Cts.IsCancellationRequested) {
					LogHelper.Debug("Cancellation requested, skipping " + website.Name);
					continue;
				}

				// Start the crawl
				var grantCrawler = new GrantCrawler(jobToRun, Config, website, myWorker, Cts, this);

				// After website search is deemed completed
				var resultsToAdd = grantCrawler.Results;
				LogHelper.Debug("Current domain time: " + CurrentDomain);
				resultsToAdd.SearchTime = CurrentDomain;
			}
			
			Results.End();
		}

		// TODO
		private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			var progress = e.UserState as Progress;

			UpdateFields(progress);
		}

		private void timerTotal_Tick(object sender, EventArgs e) {
			TotalSearch += TimeSpan.FromSeconds(1);
			CurrentDomain += TimeSpan.FromSeconds(1);

			lblCurrentDomainTime.Text = TextHelper.FormatTime(CurrentDomain);
			lblTotalTime.Text = TextHelper.FormatTime(TotalSearch);
		}

		// TODO
		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			Results.End();
			btnCancelCrawl.Enabled = true;
			btnCancelCrawl.Text = Resources.ButtonCloseText;
			btnSaveResults.Enabled = true;
		}

		// TODO
		private void GrantSearch_Load(object sender, EventArgs e) {
			lblCurrentStatus.Text = "";
			lblCurrentDomain.Text = "";
			lblCurrentDomainTime.Text = "";
			lblCurrentUrl.Text = "";
			lblPagesCrawledCount.Text = "";
			lblPagesSkippedCount.Text = "";
			lblResultsFound.Text = "";
			lblTotalTime.Text = "";
		}
	}

}