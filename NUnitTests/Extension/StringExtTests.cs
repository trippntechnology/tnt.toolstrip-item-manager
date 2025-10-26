using System.Diagnostics.CodeAnalysis;
using TNT.ToolStripItemManager.Extension;

namespace NUnitTests.Extension;

[ExcludeFromCodeCoverage]
[TestFixture]
public class StringExtTests
{

    [Test]
    public void Extensions_ToImage_WithValidResource_ReturnsImage()
    {
        // Arrange
        string resourceName = "NUnitTests.Images.shape_align_bottom.png";

        // Act
        Image? result = resourceName.ToImage();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<Bitmap>());
        //Assert.IsTrue(result.Width > 0);
        //Assert.IsTrue(result.Height > 0);
    }

    [Test]
    public void Extensions_ToImage_WithInvalidResource_ReturnsNull()
    {
        // Arrange
        string resourceName = "NonExistent.Resource.png";

        // Act
        Image? result = resourceName.ToImage();

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Extensions_ToImage_WithValidResource_DisposesProperly()
    {
        // Arrange
        string resourceName = "NUnitTests.Images.shape_align_bottom.png";

        // Act
        Image? result = resourceName.ToImage();

        // Assert
        Assert.That(result, Is.Not.Null);
        // Should not throw when disposed
        result.Dispose();
    }

    [Test]
    public void Extensions_ToImage_WithEmptyString_ReturnsNull()
    {
        // Arrange
        string resourceName = string.Empty;

        // Act
        Image? result = resourceName.ToImage();

        // Assert
        Assert.That(result, Is.Null);
    }
}
