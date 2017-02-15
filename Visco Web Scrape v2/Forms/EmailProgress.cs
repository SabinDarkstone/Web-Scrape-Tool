using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Properties;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class EmailProgress : Form {

		public Configuration Config;
		public CombinedResults Results;

		private string[] filenames;
		private readonly bool isScheduled;

		private SmtpClient emailClient;

		private readonly List<Recipient> sendToList;

		public EmailProgress(Configuration config, CombinedResults res, bool isScheduledEmail) {
			InitializeComponent();

			Config = config;
			Results = res;

			sendToList = new List<Recipient>();
			sendToList.AddRange(Config.Recipients);

			isScheduled = isScheduledEmail;
		}

		private async Task<bool> PrepareExcelDocument() {
			var grantWorkbookName = string.Format("Grants_{0}-{1}-{2}.xlsx", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Year);
			var otherWorkbookName = string.Format("Others_{0}-{1}-{2}.xlsx", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Year);

			filenames = new[] {
				grantWorkbookName, otherWorkbookName
			};

			if (File.Exists(Reference.Files.ExportDirectory + grantWorkbookName) &&
				File.Exists(Reference.Files.ExportDirectory + otherWorkbookName)) {
				return true;
			} else {
				lblCurrentStatus.Text = "Generating files...";
				var exporter = new ResultViewer(Config, Results, true, filenames);
				await exporter.StartExport();

				return File.Exists(Reference.Files.ExportDirectory + grantWorkbookName) &&
					File.Exists(Reference.Files.ExportDirectory + otherWorkbookName);
			}
		}

		private void SendEmail() {
			lblCurrentStatus.Text = "Sending emails...";
			try {
				emailClient = new SmtpClient {
					Host = "Smtp.Gmail.com",
					Port = 587,
					EnableSsl = true,
					Timeout = 10000,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential("viscograntbot@gmail.com", "DarkstoneConcepts")
				};

				// Create attachments
				var grantWorkbookAttachment = new Attachment(Reference.Files.ExportDirectory + filenames[0], MediaTypeNames.Application.Octet);
				var otherWorkbookAttachment = new Attachment(Reference.Files.ExportDirectory + filenames[1], MediaTypeNames.Application.Octet);

				var grantWorkbookDisposition = grantWorkbookAttachment.ContentDisposition;
				grantWorkbookDisposition.CreationDate = File.GetCreationTime(filenames[0]);
				grantWorkbookDisposition.ModificationDate = File.GetLastWriteTime(filenames[0]);
				grantWorkbookDisposition.ReadDate = File.GetLastAccessTime(filenames[0]);

				var otherWorkbookDisposition = otherWorkbookAttachment.ContentDisposition;
				otherWorkbookDisposition.CreationDate = File.GetCreationTime(filenames[1]);
				otherWorkbookDisposition.ModificationDate = File.GetLastWriteTime(filenames[1]);
				otherWorkbookDisposition.ReadDate = File.GetLastAccessTime(filenames[1]);

				var body = FileHelper.Email.GetBody();
				var subject = FileHelper.Email.GetSubject();

				body = body.Replace("<date>", DateTime.Today.ToShortDateString());
				body = body.Replace("<time>", DateTime.Now.ToShortTimeString());

				// Create the message
				var message = new MailMessage {
					From = new MailAddress("viscograntbot@gmail.com", "VISCO Web Scraper"),
					Body = body,
					Subject = subject
				};

				progressBar1.Maximum = Config.Recipients.Count;

				// Send an email to each recipient
				foreach (var recipient in Config.Recipients) {
					LogHelper.Debug(string.Format("Preparing message for {0}", recipient.Name));
					message.To.Clear();
					message.To.Add(new MailAddress(recipient.Address, recipient.Name));
					message.Attachments.Add(grantWorkbookAttachment);
					message.Attachments.Add(otherWorkbookAttachment);

					LogHelper.Debug(string.Format("Sending message to {0} at {1}", recipient.Name, recipient.Address));
					emailClient.Send(message);
				}

				lblCurrentStatus.Text = "Finished sending emails";
				Thread.Sleep(2000);
				this.Close();

			} catch (Exception ex) {
				LogHelper.Error(ex.Message);
				LogHelper.Trace(ex.StackTrace);
			}
		}

		private async void EmailProgress_Shown(object sender, EventArgs e) {
			if (await PrepareExcelDocument()) {
				SendEmail();

				if (isScheduled) {
					Close();
				}
			} else {
				MessageBox.Show(Resources.MailSendError, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}

}