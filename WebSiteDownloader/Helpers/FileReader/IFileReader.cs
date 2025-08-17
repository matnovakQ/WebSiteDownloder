namespace WebSiteDownloader.Helpers.FileReader;

public interface IFileReader
{
    /// <summary>
    /// Reads the file and returns a list of URLs.
    /// </summary>
    /// <param name="filePath">The path to the file containing URLs.</param>
    /// <returns>File read details</returns>
    FileReadResult ReadUrls(string filePath);
}
