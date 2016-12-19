using Abot.Poco;

namespace Visco_Web_Scrape.Operations.Configs {

	public static class FastSettings {

		public static CrawlConfiguration Config = new CrawlConfiguration {
			MaxConcurrentThreads = 25,
			MaxPagesToCrawl = 20000,
			MaxPagesToCrawlPerDomain = 0,  // No maximum
			MaxPageSizeInBytes = 0,  // No maximum
			MaxRobotsDotTextCrawlDelayInSeconds = 5,
			UserAgentString = "Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko",  // Let's see if this works
			CrawlTimeoutSeconds = 20,  // This may need to be changed
			ConfigurationExtensions = null,  // Meh
			IsUriRecrawlingEnabled = false,
			IsExternalPageCrawlingEnabled = false,  // May need to be changed depending on results of crawl
			IsExternalPageLinksCrawlingEnabled = false,  // May need to be changed depending on results of crawl (IsExternalPageCrawlingEnabled needs to be set to true if this is true)
			IsRespectUrlNamedAnchorOrHashbangEnabled = false,  // May need to be changed depending on results of crawl
			DownloadableContentTypes = "text/html, pdf",  // I have no idea if this will work
			HttpServicePointConnectionLimit = 10,
			HttpRequestTimeoutInSeconds = 0,  // Maybe this won't be looked at?
			HttpRequestMaxAutoRedirects = 20,  // Follow for a while, but not forever
			IsHttpRequestAutoRedirectsEnabled = true,
			IsHttpRequestAutomaticDecompressionEnabled = false,  // Maybe this will help with speed?
			IsSendingCookiesEnabled = false,  // Maybe this will help with speed?
			IsSslCertificateValidationEnabled = false,  // Maybe this will help with speed?
			MinAvailableMemoryRequiredInMb = 0,  // Do not care, should not be an issue
			MaxMemoryUsageInMb = 0,  // Do not want crawler to stop prematurely
			MaxMemoryUsageCacheTimeInSeconds = 0,  // Do not care for fast crawl
			MaxCrawlDepth = 16,  // Adjust as is needed to get a proper number of pages
			MaxLinksPerPage = 0,  // Do not care, go to all of them please
			IsForcedLinkParsingEnabled = true,  // Sure?
			MaxRetryCount = 4,  // Adjust as needed
			MinRetryDelayInMilliseconds = 50,  // Adjust for speed of fast crawl

			IsRespectRobotsDotTextEnabled = true,  // Sure, I'll be nice...for now
			IsRespectMetaRobotsNoFollowEnabled = false,  // I'll do what I want
			IsRespectHttpXRobotsTagHeaderNoFollowEnabled = false,  // I'll do what I want
			IsRespectAnchorRelNoFollowEnabled = false,  // I'll do what I want
			IsIgnoreRobotsDotTextIfRootDisallowedEnabled = false,  // It needs to be done...for SCIENCE!
			RobotsDotTextUserAgentString = "grantSearch",  // Suuuureee...
			MinCrawlDelayPerDomainMilliSeconds = 5,  // This needs to be fast

			IsAlwaysLogin = false,
			LoginUser = "",
			LoginPassword = "",
		};
	}

}