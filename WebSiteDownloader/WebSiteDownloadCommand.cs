using System.CommandLine;
using WebSiteDownloader.Helpers.FileReader;
using WebSiteDownloader.Helpers.FileWriter;
using WebSiteDownloader.Helpers.WebSiteDownlodingService;

namespace WebSiteDownloader;

public class WebSiteDownloadCommand : RootCommand
{
    private readonly IFileReader _fileReader;
    private readonly IFileWriter _fileWriter;
    private readonly IWebSiteDownlodingService _webSiteDownlodingService;

    public WebSiteDownloadCommand(IFileReader fileReader, IFileWriter fileWriter, IWebSiteDownlodingService webSiteDownlodingService) : base("CLI for downloading large amount of website content")
    {
        _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
        _fileWriter = fileWriter ?? throw new ArgumentNullException(nameof(fileWriter));
        _webSiteDownlodingService = webSiteDownlodingService ?? throw new ArgumentNullException(nameof(fileWriter));

        Option<string> fileInnputOption = new Option<string>("--input", ["-i"]) { Description = "File containing url addresses in new line", Required = true };
        Option<string> outputFolderOption = new Option<string>("--output", ["-o"]) { Description = "Folder for storing website content", Required = true };

        Options.Add(fileInnputOption);
        Options.Add(outputFolderOption);

        SetAction(async parseResult =>
        {
            string? filePath = parseResult.GetValue<string>(fileInnputOption);
            string? outputFolder = parseResult.GetValue<string>(outputFolderOption);

            await StartDownloading(filePath, outputFolder);
        });
    }

    private async Task StartDownloading(string? filePath, string? outputFolder)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("File path cannot be null or empty.");
            return;
        }
        if (string.IsNullOrWhiteSpace(outputFolder))
        {
            Console.WriteLine("Output folder cannot be null or empty.");
            return;        
        }
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        FileReadResult fileReadResult = _fileReader.ReadUrls(filePath);
        if (!fileReadResult.IsSuccess)
        {
            Console.WriteLine($"Error reading file: {fileReadResult.ErrorMessage}");
            return;
        }
        WebSiteDetails[] websiteDetails = await _webSiteDownlodingService.DownloadWebSitesAsync(fileReadResult.Urls);
        if (websiteDetails.Length == 0)
        {
            Console.WriteLine("No websites were downloaded.");
            return;
        }

        foreach (WebSiteDetails details in websiteDetails)
        {
            _fileWriter.WriteContent(Path.Combine(outputFolder, details.FileName), details.Content);
        }
    }
}
