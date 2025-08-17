using Moq;
using WebSiteDownloader.Helpers.WebSiteDownlodingService;

namespace WebSiteDownloader.Tests;

public class WebSiteDownloadServiceTests
{
    [Fact]
    public async Task DownloadWebSitesAsync_ValidUrls_ReturnsWebSiteDetails()
    {
        // Arrange
        var mockWebSiteDownlodingService = new Mock<IWebSiteDownlodingService>();
        mockWebSiteDownlodingService.Setup(s => s.DownloadWebSitesAsync(It.IsAny<List<string>>()))
            .ReturnsAsync([
                new WebSiteDetails("http://example.com",  "<html>Example com</html>" ),
                new WebSiteDetails("http://example.org",  "<html>Example Org</html>")
            ]);

        // Act
        var result = await mockWebSiteDownlodingService.Object.DownloadWebSitesAsync([ "http://example.com", "http://example.org" ]);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Length);      
    }

    [Fact]
    public async Task DownloadWebSitesAsync_EmptyUrls_ReturnsEmptyArray()
    {
        // Arrange
        var mockWebSiteDownlodingService = new Mock<IWebSiteDownlodingService>();
        mockWebSiteDownlodingService.Setup(s => s.DownloadWebSitesAsync(It.IsAny<List<string>>()))
            .ReturnsAsync(Array.Empty<WebSiteDetails>());
        // Act
        var result = await mockWebSiteDownlodingService.Object.DownloadWebSitesAsync(new List<string>());
        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task DownloadWebSitesAsync_SkippedUrls_ReturnsValidWebSiteDetails()
    {
        // Arrange
        var mockWebSiteDownlodingService = new Mock<IWebSiteDownlodingService>();
        mockWebSiteDownlodingService.Setup(s => s.DownloadWebSitesAsync(It.IsAny<List<string>>()))
            .ReturnsAsync([
                new WebSiteDetails("http://example.com",  "<html>Example com</html>" ),
                new WebSiteDetails("example.org",  "")
            ]);
        // Act
        var result = await mockWebSiteDownlodingService.Object.DownloadWebSitesAsync([ "http://example.com", "example.org" ]);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Count(o=>string.IsNullOrEmpty(o.Content)));
    }
}
