using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Items;
using Visco_Web_Scrape_v2.Search.Process;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class GrantSearch : Form {

		public Configuration Config;
		public List<SearchResults> AllResults { get; private set; }
		public CancellationTokenSource Cts = new CancellationTokenSource();

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
			DialogResult = DialogResult.OK;
			Hide();
		}

		private void btnCancelCrawl_Click(object sender, EventArgs e) {
			if (worker.IsBusy) {
				var check =
					MessageBox.Show(Reference.Messages.CancelCrawl, Reference.Messages.CancelQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (check == DialogResult.No)
					return;

				worker.ReportProgress(0, new Progress(Progress.Status.Cancelling));
				Debug.WriteLine("Sending cancellation request...");
				worker.CancelAsync();
			} else {
				DialogResult = DialogResult.Cancel;
				Close();
			}
		}

		// Figure out a way to check if a website url exists in the current results list
		// 1. Mark all current results as old
		// 2. Compare url from just completed search with those that are saved
		// 3. If a match is found, ignore it.  If no match is found, add the url and mark it as new
		public List<SearchResults> CompareLists(List<SearchResults> savedResults) {
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
			if (websites == null)
				throw new NullReferenceException("No website list.");
			foreach (var website in websites.WebsitesToCrawl) {
				// Initialize and run the crawler
				var grantCrawler = new GrantCrawler(Config, website, myWorker, Cts);

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

			lblCurrentDomain.Text = "";
			lblCurrentStatus.Text = "Completed";
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
