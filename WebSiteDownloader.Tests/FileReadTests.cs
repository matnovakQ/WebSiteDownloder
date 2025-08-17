using Moq;
using WebSiteDownloader.Helpers.FileReader;

namespace WebSiteDownloader.Tests;

public class FileReadTests
{
    [Fact]
    public void ReadInvalidFilePath_ThrowsArgumentException()
    {
        // Arrange
        var mockFileReader = new Mock<IFileReader>();
        mockFileReader.Setup(fr => fr.ReadUrls(It.Is<string>(o=>!o.EndsWith(".txt"))))
            .Throws(new ArgumentException("Invalid file format. Only .txt files are supported."));
        // Act & Assert
        Assert.Throws<ArgumentException>(() => mockFileReader.Object.ReadUrls("text.txtt"));
    }

    [Fact]
    public void ReadUrls_ValidFile_ReturnsUrls()
    {
        // Arrange
        var mockFileReader = new Mock<IFileReader>();
        mockFileReader.Setup(fr => fr.ReadUrls(It.IsAny<string>()))
            .Returns(FileReadResult.Success(["http://example.com", "http://example.org"]));    
        
        // Act
        var result = mockFileReader.Object.ReadUrls("dummyPath");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Urls.Count);
    }

    [Fact]
    public void ReadUrls_ValidFile_InvalidUrls()
    {
        // Arrange
        var mockFileReader = new Mock<IFileReader>();
        mockFileReader.Setup(fr => fr.ReadUrls(It.IsAny<string>()))
            .Returns(FileReadResult.Failure("No URLs found in the file."));

        // Act
        var result = mockFileReader.Object.ReadUrls("dummyPath");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No URLs found in the file.", result.ErrorMessage);
    }

    [Fact]
    public void ReadUrls_FileNotFound_ReturnsError()
    {
        // Arrange
        var mockFileReader = new Mock<IFileReader>();
        mockFileReader.Setup(fr => fr.ReadUrls(It.IsAny<string>()))
            .Returns(FileReadResult.Failure("File not found"));
        // Act
        var result = mockFileReader.Object.ReadUrls("nonExistentFile.txt");
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("File not found", result.ErrorMessage);
    }

    [Fact]
    public void ReadUrls_EmptyFile_ReturnsError()
    {
        // Arrange
        var mockFileReader = new Mock<IFileReader>();
        mockFileReader.Setup(fr => fr.ReadUrls(It.IsAny<string>()))
            .Returns(FileReadResult.Failure("File is empty"));
        
        // Act
        var result = mockFileReader.Object.ReadUrls("emptyFile.txt");
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("File is empty", result.ErrorMessage);
    }
}
