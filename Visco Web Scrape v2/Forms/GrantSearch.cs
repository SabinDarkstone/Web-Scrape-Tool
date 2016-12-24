using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Search;
using Visco_Web_Scrape_v2.Search.Items;
using Visco_Web_Scrape_v2.Search.Process;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class GrantSearch : Form {

		public Configuration Config;
		public List<SearchResults> AllResults { get; private set; }
		public CancellationTokenSource cts = new CancellationTokenSource();

		private BackgroundWorker worker;

		private readonly Job jobToRun;

		public GrantSearch(Configuration configuration, Job job) {
			InitializeComponent();

			Config = configuration;
			jobToRun = job;
		}

		private void UpdateFields(Progress progress) {
			if (progress.CurrentStatus == Progress.Status.Cancelled) {
				lblCurrentStatus.Text = "Cancelled";
				return;
			}

			lblCurrentDomain.Text = progress.Domain == null ? lblCurrentDomain.Text : progress.Domain;
			lblCurrentUrl.Text = progress.Url == null ? lblCurrentDomain.Text : progress.Url;
			lblPagesCrawledCount.Text = progress.TotalPageCount == null
				? lblPagesCrawledCount.Text
				: progress.TotalPageCount.ToString();
			lblResultsFound.Text = progress.RelevantPageCount == null
				? lblResultsFound.Text
				: progress.RelevantPageCount.ToString();
			lblCurrentStatus.Text = progress.CurrentStatus.ToString();
			progressbar.Value = progress.DomainNumber + 1;
		}

		public void Stop(DoWorkEventArgs e) {
			e.Cancel = true;
		}

		private void btnSaveResults_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.OK;
			Hide();
		}

		private void btnCancelCrawl_Click(object sender, EventArgs e) {
			if (worker.IsBusy) {
				var check =
					MessageBox.Show(
						"Are you sure you want to cancel the proess? Doing so and saving afterwards will clear your previous results and replace them with only the results found so far.",
						"Really Cancel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (check == DialogResult.No) return;

				worker.ReportProgress(0, new Progress(Progress.Status.Cancelling));
				Debug.WriteLine("Sending cancellation request...");
				worker.CancelAsync();
			} else {
				DialogResult = DialogResult.Cancel;
				Close();
			}
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
			var myWorker = sender as BackgroundWorker;
			if (websites == null) throw new NullReferenceException("No website list.");
			foreach (var website in websites.WebsitesToCrawl) {
				// Initialize and run the crawler
				var grantCrawler = new GrantCrawler(Config, website, jobToRun.KeywordsToSearchFor.ToArray(), myWorker, e, this);
				grantCrawler.Run(cts);

				// Check crawler status
				if (grantCrawler.Successful) {
					AllResults.Add(grantCrawler.Results);
				}
			}
		}

		private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			var progress = e.UserState as Progress;
			UpdateFields(progress);
		}

		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			btnCancelCrawl.Text = "Cancel";
			btnCancelCrawl.Enabled = false;
			btnSaveResults.Enabled = true;
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
