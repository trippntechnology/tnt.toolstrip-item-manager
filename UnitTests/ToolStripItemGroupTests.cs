using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Windows.Forms;
using TNT.ToolStripItemManager.Tests.Helpers;

namespace TNT.ToolStripItemManager.Tests
{
	[TestClass]
	public class ToolStripItemGroupTests
	{
		static TestToolStripItemGroup sut = null;
		static Image ToolStripItemGroupImage = ToolStripItemGroup.ResourceToImage("TNT.ToolStripItemManager.Tests.Images.shape_align_bottom.png");

		[ClassInitialize]
		public static void InitializeClass(TestContext tc)
		{
			sut = new TestToolStripItemGroup(ToolStripItemGroupImage);
			Assert.IsNotNull(sut.Image);
			Assert.AreEqual(ToolStripItemGroupImage, sut.Image);
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
		public void ToolStripItemGroup_ResourceToImage_Null()
		{
			Assert.IsNull(ToolStripItemGroup.ResourceToImage("bogus"));
		}

		//[TestMethod]
		public void ToolStripItemGroup_Add()
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

			sut.Checked = true;

			// ToolStripItem checks
			sut.ForEach(it =>
			{
				//Assert.IsNotNull(it.MouseEnter);
				Assert.AreEqual(sut.Text, it.Text);
				Assert.AreEqual(sut.Image, it.Image);
			});

			sut.ForEach(it =>
			{
				if (it is ToolStripButton toolStripButton)
				{
					Assert.AreEqual(sut.CheckOnClick, toolStripButton.CheckOnClick);
					Assert.AreEqual(sut.Checked, toolStripButton.Checked);
					//Assert.AreEqual(sut.CheckedChanged, toolStripButton.CheckedChanged);
				}

				Assert.AreEqual(sut.Text, it.Text);
				Assert.AreEqual(sut.Image, it.Image);
			});
		}

		[TestMethod]
		public void ToolStripItemGroup_Add_ToolStripMenuItem()
		{
			var statusLabel = new ToolStripStatusLabel();
			var tsiGroupManager = new ToolStripItemGroupManager(statusLabel);

			//var sut = new TestToolStripItemGroup(ToolStripItemGroupImage);

			var item = new ToolStripMenuItem()
			{
				CheckOnClick = true,
				Checked = true
			};

			var mouseClicked1Called = false;
			sut = tsiGroupManager.Create<TestToolStripItemGroup>(new[] { item }, ToolStripItemGroupImage, onClick: (s, e) => { mouseClicked1Called = true; });

			sut.Add(item);

			Assert.IsFalse(item.CheckOnClick);
			Assert.IsFalse(item.Checked);
			Assert.IsFalse(sut.Checked);

			//var checkChangedCalled = false;
			//item.CheckedChanged += (s, e) => { checkChangedCalled = true; };

			item.Checked = true;

			Assert.IsTrue(sut.Checked);
			//Assert.IsTrue(checkChangedCalled);

			var mouseClick2Called = false;
			sut.MyMouseClick += (s, e) => { mouseClick2Called = true; };
			//sut.OnMouseClick += (s, e) => { mouseClickCalled = true; };

			item.PerformClick();
			//sut.MouseClick(null, null);
			Assert.IsTrue(mouseClicked1Called);
			Assert.IsTrue(mouseClick2Called);

			Assert.AreEqual(sut.Image, item.Image);
			Assert.AreEqual(sut.Text, item.Text);
			Assert.AreEqual(sut.ToolTipText, item.ToolTipText);
		}

		[TestMethod]
		public void ToolStripItemGroup_Add_ToolStripButton()
		{
			var statusLabel = new ToolStripStatusLabel();
			var tsiGroupManager = new ToolStripItemGroupManager(statusLabel);

			var item = new ToolStripButton()
			{
				CheckOnClick = true,
				Checked = true
			};

			var mouseClicked1Called = false;
			sut = tsiGroupManager.Create<TestToolStripItemGroup>(new[] { item }, ToolStripItemGroupImage, onClick: (s, e) => { mouseClicked1Called = true; });

			sut.Add(item);

			Assert.IsFalse(item.CheckOnClick);
			Assert.IsFalse(item.Checked);
			Assert.IsFalse(sut.Checked);

			item.Checked = true;

			Assert.IsTrue(sut.Checked);

			var mouseClick2Called = false;
			sut.MyMouseClick += (s, e) => { mouseClick2Called = true; };

			item.PerformClick();
			Assert.IsTrue(mouseClicked1Called);
			Assert.IsTrue(mouseClick2Called);

			Assert.AreEqual(sut.Image, item.Image);
			Assert.AreEqual(sut.Text, item.Text);
			Assert.AreEqual(sut.ToolTipText, item.ToolTipText);
		}

		[TestMethod]
		public void ToolStripItemGroup_Add_ToolStripSplitButton()
		{
			var statusLabel = new ToolStripStatusLabel();
			var tsiGroupManager = new ToolStripItemGroupManager(statusLabel);

			var item = new ToolStripSplitButton();

			var mouseClicked1Called = false;
			sut = tsiGroupManager.Create<TestToolStripItemGroup>(new[] { item }, ToolStripItemGroupImage, onClick: (s, e) => { mouseClicked1Called = true; });

			sut.Add(item);

			var mouseClick2Called = false;
			sut.MyMouseClick += (s, e) => { mouseClick2Called = true; };

			item.PerformButtonClick();
			Assert.IsTrue(mouseClicked1Called);
			Assert.IsTrue(mouseClick2Called);

			Assert.AreEqual(sut.Image, item.Image);
			Assert.AreEqual(sut.Text, item.Text);
			Assert.AreEqual(sut.ToolTipText, item.ToolTipText);
		}

		[TestMethod]
		public void ToolStripItemGroup_Add_CustomToolStripItem()
		{
			var statusLabel = new ToolStripStatusLabel();
			var tsiGroupManager = new ToolStripItemGroupManager(statusLabel);

			var item = new TestToolStripItem();

			var mouseClicked1Called = false;
			sut = tsiGroupManager.Create<TestToolStripItemGroupAddOverride>(new[] { item }, ToolStripItemGroupImage, onClick: (s, e) => { mouseClicked1Called = true; });

			sut.Add(item);

			var mouseClick2Called = false;
			sut.MyMouseClick += (s, e) => { mouseClick2Called = true; };

			item.PerformClick();
			Assert.IsTrue(mouseClicked1Called);
			Assert.IsTrue(mouseClick2Called);

			Assert.AreEqual(sut.Image, item.Image);
			Assert.AreEqual(sut.Text, item.Text);
			Assert.AreEqual(sut.ToolTipText, item.ToolTipText);
		}

		[TestMethod]
		public void ToolStripItemGroup_MouseEnter_MouseLeave()
		{
			var statusLabel = new ToolStripStatusLabel();
			var tsiGroupManager = new ToolStripItemGroupManager(statusLabel);

			//var sut = new TestToolStripItemGroup(ToolStripItemGroupImage);

			var item = new TestToolStripItem();

			sut = tsiGroupManager.Create<TestToolStripItemGroupAddOverride>(new[] { item });

			sut.Add(item);

			var mouseEntered = false;
			var mouseLeft = false;

			item.MouseEnter += (s, e) =>
			{
				mouseEntered = true;
			};

			item.MouseLeave += (s, e) =>
			{
				mouseLeft = true;
			};

			Assert.AreEqual(String.Empty, statusLabel.Text);

			item.PerformMouseEnter(new EventArgs());
			Assert.IsTrue(mouseEntered);
			Assert.AreEqual(sut.ToolTipText, statusLabel.Text);

			item.PerformMouseLeave(null);
			Assert.IsTrue(mouseLeft);
			Assert.AreEqual(String.Empty, statusLabel.Text);
		}
	}
}
