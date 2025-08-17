using Moq;
using WebSiteDownloader.Helpers.FileWriter;

namespace WebSiteDownloader.Tests;

public class FileWriteTests
{
    [Fact]
    public void WriteContent_ValidContent_WritesToFile()
    {
        // Arrange
        var mockFileWriter = new Mock<IFileWriter>();
        string filePath = @"results\test.txt";
        string content = "<html></html>";

        // Act
        mockFileWriter.Object.WriteContent(filePath, content);

        // Assert
        mockFileWriter.Verify(fw => fw.WriteContent(filePath, content), Times.Once);
    }
}
