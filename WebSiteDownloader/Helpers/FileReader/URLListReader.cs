namespace WebSiteDownloader.Helpers.FileReader;

public class URLListReader : IFileReader
{
    public FileReadResult ReadUrls(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return FileReadResult.Failure("File path cannot be null or empty.");           
        }
        if (!File.Exists(filePath))
        {
            return FileReadResult.Failure("File does not exist.");
        }
        if (Path.GetExtension(filePath).ToLower() != ".txt")
        {
            return FileReadResult.Failure( "Invalid file format. Only .txt files are supported.");
        }

        try
        {
            var urls = File.ReadAllLines(filePath)
                .Where(urlLine => IsUrlValid(urlLine))
                .ToList();

            if (urls.Count == 0)
            {
                return FileReadResult.Failure( "No URLs found in the file.");
            }
            return FileReadResult.Success(urls);
        }
        catch (Exception ex)
        {
            return FileReadResult.Failure($"Error reading file: {ex.Message}");
        }
    }

    private bool IsUrlValid(string urlLine)
    {
        return !string.IsNullOrWhiteSpace(urlLine) &&
            Uri.IsWellFormedUriString(urlLine, UriKind.Absolute) &&
            (urlLine.StartsWith("http://") || urlLine.StartsWith("https://"));
    }
}
