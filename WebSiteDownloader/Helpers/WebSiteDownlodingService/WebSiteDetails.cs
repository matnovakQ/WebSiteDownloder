namespace WebSiteDownloader.Helpers.WebSiteDownlodingService;

public record WebSiteDetails(string Url, string Content)
{
    public string FileName => $"{Uri.EscapeDataString(Url)}.html";
}