namespace WebSiteDownloader.Helpers.FileWriter;

public interface IFileWriter
{
    /// <summary>
    /// Savings the website contents to the specified output folder.
    /// </summary>
    /// <param name="filePath">The path to the file where the content will be saved.</param>
    /// <param name="content">The content to be written to the file.</param>
    void WriteContent(string filePath, string content);
}
