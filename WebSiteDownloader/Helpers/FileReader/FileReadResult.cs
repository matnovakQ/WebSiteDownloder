namespace WebSiteDownloader.Helpers.FileReader;

public record FileReadResult(bool IsSuccess, List<string> Urls, string? ErrorMessage = null)
{
    public static FileReadResult Success(List<string> urls) => new(true, urls);
    public static FileReadResult Failure(string errorMessage) => new(false, new List<string>(), errorMessage);
}