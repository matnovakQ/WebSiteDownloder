using System.Diagnostics;

namespace WebSiteDownloader.Helpers.FileWriter;

public class FileWriter : IFileWriter
{
    public void WriteContent(string filePath, string content)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
        }
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("Content cannot be null or empty.", nameof(content));
        }
        if (File.Exists(filePath))
        {
            filePath = Path.Combine(Path.GetDirectoryName(filePath)??string.Empty,
                $"{Path.GetFileNameWithoutExtension(filePath)}_{Stopwatch.GetTimestamp()}{Path.GetExtension(filePath)}");
        }
        File.WriteAllText(filePath, content);
    }
}