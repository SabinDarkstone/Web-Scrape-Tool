﻿using System;
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

		private void btnCancelCrawl_Click(object sender, EventArgs e) {
			if (worker.IsBusy) {
				var confirm = MessageBox.Show(Resources.ConfirmCancelCrawl, Resources.ConfirmationRequired, MessageBoxButtons.YesNo,
					MessageBoxIcon.Question);

				if (confirm == DialogResult.No) return;

				worker.CancelAsync();
				timerTotal.Enabled = false;
			} else {
				Close();
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

			foreach (var website in myJob.WebsitesToCrawl) {
				progressbar.Invoke(new MethodInvoker(
					delegate { progressbar.Value = jobToRun.WebsitesToCrawl.ToList().IndexOf(website) + 1; }));
				CurrentDomain = TimeSpan.Zero;

				// Check for cancellation
				if (Cts.IsCancellationRequested) {
					LogHelper.Debug("Cancellation requested, skipping " + website.Name);
					//var skippedResults = new WebsiteResults(website) {WebsiteStatus = WebsiteResults.Status.Skipped};
					//if (Results.AllResults.Any(site => site.RootWebsite.Url.Equals(website.Url))) {
					//	var firstOrDefault = Results.AllResults.FirstOrDefault(j => j.RootWebsite.Url.Equals(website.Url));
					//	if (firstOrDefault != null)
					//		firstOrDefault.WebsiteStatus = WebsiteResults.Status.Skipped;
					//} else {
					//	Results.AllResults.Add(skippedResults);
					//}
					Results.AllResults.First(i => i.RootWebsite.Url.Equals(website.Url)).WebsiteStatus = WebsiteResults.Status.Skipped;
					continue;
				}

				// Start the crawl
				var grantCrawler = new GrantCrawler(jobToRun, Config, website, myWorker, Cts);

				if (grantCrawler.Results.WebsiteStatus == WebsiteResults.Status.Interrupted) {
					LogHelper.Debug(website.Name + " cancelled early");
				} else {
					LogHelper.Debug("Crawl completed, saving results");
				}

				var resultsToAdd = grantCrawler.Results;
				resultsToAdd.SearchTime = CurrentDomain;
				Results.UpdateResults(resultsToAdd);
				LogHelper.Debug("Finished writing results of " + resultsToAdd.RootWebsite.Name + " to master list");
			}
			
			Results.End();

			worker.ReportProgress(0, new Progress(Progress.Status.Cancelled));
		}

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

		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			Results.End();
			btnCancelCrawl.Enabled = true;
			btnCancelCrawl.Text = Resources.ButtonCloseText;
			btnSaveResults.Enabled = true;

			if (jobToRun.IsScheduledJob) {
				LogHelper.Debug("Automated search complete");
				Thread.Sleep(2000);
				DialogResult = DialogResult.OK;
				Hide();
			}
		}

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