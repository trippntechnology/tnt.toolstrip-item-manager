using System.Diagnostics.CodeAnalysis;
using TNT.ToolStripItemManager.Extension;

namespace UnitTests;

[ExcludeFromCodeCoverage]
[TestClass()]
public class ExtensionsTests
{
	static List<ToolStripItem> toolStripItems = new List<ToolStripItem>();
	//static ToolStripMenuItem menuItem = null;
	//static ToolStripButton button = null;

	[ClassInitialize]
	public static void InitializeClass(TestContext tc)
	{
		toolStripItems.Add(new ToolStripMenuItem("Menu"));
		toolStripItems.Add(new ToolStripButton("Button"));
	}

	[TestMethod()]
	public void Extensions_GetSetCheckedTest()
	{
		toolStripItems.ForEach(it => Assert.IsFalse(it.GetChecked()));
		toolStripItems.ForEach(it => it.SetChecked(true));
		toolStripItems.ForEach(it => Assert.IsTrue(it.GetChecked()));
		toolStripItems.ForEach(it => it.SetChecked(false));
		toolStripItems.ForEach(it => Assert.IsFalse(it.GetChecked()));
	}

	[TestMethod()]
	public void Extensions_ToImage_WithValidResource_ReturnsImage()
	{
		// Arrange
		string resourceName = "UnitTests.Images.shape_align_bottom.png";

		// Act
		Image? result = resourceName.ToImage();

		// Assert
		Assert.IsNotNull(result);
		Assert.IsInstanceOfType(result, typeof(Bitmap));
		//Assert.IsTrue(result.Width > 0);
		//Assert.IsTrue(result.Height > 0);
	}

	[TestMethod()]
	public void Extensions_ToImage_WithInvalidResource_ReturnsNull()
	{
		// Arrange
		string resourceName = "NonExistent.Resource.png";

		// Act
		Image? result = resourceName.ToImage();

		// Assert
		Assert.IsNull(result);
	}

	[TestMethod()]
	public void Extensions_ToImage_WithValidResource_DisposesProperly()
	{
		// Arrange
		string resourceName = "UnitTests.Images.shape_align_bottom.png";

		// Act
		Image? result = resourceName.ToImage();

		// Assert
		Assert.IsNotNull(result);
		// Should not throw when disposed
		result.Dispose();
	}

	[TestMethod()]
	public void Extensions_ToImage_WithEmptyString_ReturnsNull()
	{
		// Arrange
		string resourceName = string.Empty;

		// Act
		Image? result = resourceName.ToImage();

		// Assert
		Assert.IsNull(result);
	}

	//[TestMethod()]
	//public void Extensions_GetCheckedTest()
	//{
	//	menuItem.SetChecked(false);
	//	button.SetChecked(false);

	//	Assert.IsFalse(menuItem.Checked);
	//	Assert.IsFalse(button.Checked);

	//	menuItem.SetChecked(true);
	//	button.SetChecked(true);

	//	Assert.IsTrue(menuItem.Checked);
	//	Assert.IsTrue(button.Checked);

	//}
}
