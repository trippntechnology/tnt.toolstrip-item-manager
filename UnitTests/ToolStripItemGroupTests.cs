using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.ToolStripItemManager;
using System.Drawing;
using Moq;
using System.IO;
using System.Windows.Forms;

namespace TNT.ToolStripItemManager.Tests
{
	[TestClass]
	public class ToolStripItemGroupTests
	{
		class TestToolStripItemGroup : ToolStripItemGroup
		{
			public TestToolStripItemGroup(Image image = null)
				: base(image)
			{

			}

			public override string Text => "Test";

			public override string ToolTipText => "Tool Tip Test";
		}

		[TestMethod]
		public void ToolStripItemGroup_ConstructorTest_Null_Image()
		{
			var sut = new TestToolStripItemGroup();
			Assert.IsNull(sut.Image);
		}

		[TestMethod]
		public void ToolStripItemGroup_ConstructorTest()
		{
			var sut = new TestToolStripItemGroup(new Bitmap(10, 10));
			Assert.IsNotNull(sut.Image);
		}

		[TestMethod]
		public void TestToolStripItemGroup_ResourceToImage_Null()
		{
			Assert.IsNull(ToolStripItemGroup.ResourceToImage("bogus"));
		}

		[TestMethod]
		public void TestToolStripItemGroup_Add()
		{
			var bitmap = ToolStripItemGroup.ResourceToImage("TNT.ToolStripItemManager.Tests.Images.shape_align_bottom.png");
			var sut = new TestToolStripItemGroup(bitmap);
			Assert.IsNotNull(sut.Image);
			var menuStripItem = new ToolStripMenuItem();
			var buttonStripItem = new ToolStripButton();
			var splitButtonStripItem = new ToolStripSplitButton();
			var dropDownButtonStripItem = new ToolStripDropDownButton();
			sut.Add(menuStripItem);
			sut.Add(buttonStripItem);
			sut.Add(splitButtonStripItem);
			sut.Add(dropDownButtonStripItem);

			sut.ForEach(it =>
			{
				Assert.AreEqual(sut.Text, it.Text);
				Assert.AreEqual(sut.Image, it.Image);
			});


		}
	}
}
