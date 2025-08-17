using WebSiteDownloader;
using WebSiteDownloader.Helpers.FileReader;
using WebSiteDownloader.Helpers.FileWriter;
using WebSiteDownloader.Helpers.WebSiteDownlodingService;

WebSiteDownloadCommand webSiteDownloadCommand = new WebSiteDownloadCommand(new URLListReader(),new FileWriter(),new WebSiteDownlodingService());
await webSiteDownloadCommand.Parse(args).InvokeAsync();

