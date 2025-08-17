namespace WebSiteDownloader.Helpers.WebSiteDownlodingService;

public class WebSiteDownlodingService : IWebSiteDownlodingService
{
    public async Task<WebSiteDetails[]> DownloadWebSitesAsync(List<string> urls)
    {
        var downloadingtasks = urls.Select(url => DownloadWebsiteAsync(url));
        return await Task.WhenAll(downloadingtasks);
    }

    private async Task<WebSiteDetails> DownloadWebsiteAsync(string url)
    {
        using var httpClient = new HttpClient();
        try
        {
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return new WebSiteDetails(url, content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error downloading {url}: {ex.Message}");
            return new WebSiteDetails(url, string.Empty); // Return empty content on error
        }
    }
}