using Moq;
using WebSiteDownloader.Helpers.FileReader;
using WebSiteDownloader.Helpers.FileWriter;
using WebSiteDownloader.Helpers.WebSiteDownlodingService;

namespace WebSiteDownloader.Tests;

public class ArgumentParserTests
{
    [Theory]
    [InlineData("--input file.txt --output resuts",true)]
    [InlineData("-i file.txt -o resuts", true)]
    [InlineData("--input --output resuts", false)]
    [InlineData("--input --output", false)]
    public void AllParsed(string arguments,bool valid)
    {
        // Arrange
        var mockFileReader = new Mock<IFileReader>();
        var mockFileWriter = new Mock<IFileWriter>();
        var mockWebSiteDownlodingService = new Mock<IWebSiteDownlodingService>();

        // Act
        WebSiteDownloadCommand webSiteDownloadCommand = new WebSiteDownloadCommand(mockFileReader.Object, mockFileWriter.Object, mockWebSiteDownlodingService.Object);
        var results = webSiteDownloadCommand.Parse(arguments.Split(' '));

        // Assert
        Assert.NotNull(results);
        Assert.Equal(valid, results.Errors.Count == 0);
    }
}
