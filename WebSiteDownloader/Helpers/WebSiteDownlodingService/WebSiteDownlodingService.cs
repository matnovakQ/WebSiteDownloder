using Polly;
using Polly.Retry;
using Polly.Timeout;

namespace WebSiteDownloader.Helpers.WebSiteDownlodingService;

public class WebSiteDownlodingService : IWebSiteDownlodingService
{
    private readonly ResiliencePipeline _pipeline;
    private readonly HttpClient httpClient;
    public WebSiteDownlodingService()
    {
        _pipeline = new ResiliencePipelineBuilder()
            .AddRetry(new RetryStrategyOptions
            {
                ShouldHandle = new PredicateBuilder().Handle<Exception>(),
                Delay = TimeSpan.FromSeconds(1),
                MaxRetryAttempts = 3,
                BackoffType = DelayBackoffType.Exponential,
                UseJitter = true
            })
            .AddTimeout(new TimeoutStrategyOptions
            {
                Timeout = TimeSpan.FromSeconds(10)
            })
            .Build();
        
        httpClient = new HttpClient();
    }

    public async Task<WebSiteDetails[]> DownloadWebSitesAsync(List<string> urls)
    {
        var downloadingtasks = urls.Select(url => DownloadWebsiteAsync(url));
        return await Task.WhenAll(downloadingtasks);
    }

    private async Task<WebSiteDetails> DownloadWebsiteAsync(string url)
    {
        try
        {
            var response = await _pipeline.ExecuteAsync(async ct => await httpClient.GetAsync(url, ct));
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