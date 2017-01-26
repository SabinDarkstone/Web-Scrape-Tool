using Quartz;
using Visco_Web_Scrape_v2.Forms;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Search.Process {

	public class ScheduledCrawl : IJob {

		private readonly GrantSearch searchForm;

		public ScheduledCrawl(Configuration config, CombinedResults results, Job job, MainForm mainForm) {
			searchForm = new GrantSearch(config, results, job, mainForm);
		}

		public void Execute(IJobExecutionContext context) {
			searchForm.ShowDialog();
		}
	}
}
