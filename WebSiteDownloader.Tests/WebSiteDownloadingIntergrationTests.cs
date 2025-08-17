using Moq;
using WebSiteDownloader.Helpers.FileReader;
using WebSiteDownloader.Helpers.FileWriter;
using WebSiteDownloader.Helpers.WebSiteDownlodingService;

namespace WebSiteDownloader.Tests;

public class WebSiteDownloadingIntergrationTests
{
    [Fact]
    public async Task WebSiteDownloadCommand_IntegrationTest_test4valid()
    {
        // Arrange
        WebSiteDownloadCommand webSiteDownloadCommand = new WebSiteDownloadCommand(new URLListReader(), new FileWriter(), new WebSiteDownlodingService());
        // Act
        if (Directory.Exists("results"))
        {
            Directory.Delete("results", true);
        }
        await webSiteDownloadCommand.Parse(["-i", "testfiles/test4valid.txt", "-o", "results"]).InvokeAsync();
        // Assert
        Assert.True(Directory.Exists("results"), "Results directory should exist after command execution.");
        Assert.Equal(4, Directory.GetFiles("results").Length);

        //cleanup
        Directory.Delete("results", true);
    }

    [Fact]
    public async Task WebSiteDownloadCommand_IntegrationTest_test1000valid()
    {
        // Arrange
        WebSiteDownloadCommand webSiteDownloadCommand = new WebSiteDownloadCommand(new URLListReader(), new FileWriter(), new WebSiteDownlodingService());
        // Act
        if (Directory.Exists("results"))
        {
            Directory.Delete("results", true);
        }        
        await webSiteDownloadCommand.Parse(["-i", "testfiles/test1000valid.txt", "-o", "results"]).InvokeAsync();
        // Assert
        Assert.True(Directory.Exists("results"), "Results directory should exist after command execution.");
        Assert.Equal(1000, Directory.GetFiles("results").Length);
        //cleanup
        //Directory.Delete("results", true);
    }
}