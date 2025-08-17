namespace WebSiteDownloader.Helpers.WebSiteDownlodingService;

public interface IWebSiteDownlodingService
{
    /// <summary>
    /// Downloads the content of the specified websites.
    /// </summary>
    /// <param name="urls">List of URLs to download.</param>
    Task<WebSiteDetails[]> DownloadWebSitesAsync(List<string> urls);
}
