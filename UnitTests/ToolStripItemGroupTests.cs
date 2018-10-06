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

			var externalObject = new TestToolStripItem();
			var item = new TestToolStripItem();

			sut = tsiGroupManager.Create<TestToolStripItemGroupAddOverride>(new[] { item }, ToolStripItemGroupImage, externalObject);

			Assert.AreEqual(tsiGroupManager, sut.ToolStripItemGroupManager);

			sut.Add(item);

			item.PerformClick();

			var mouseClick2Called = false;
			sut.MyMouseClick += (s, e) => { mouseClick2Called = true; };

			item.PerformClick();
			Assert.IsTrue(mouseClick2Called);

			Assert.AreEqual(sut.Image, item.Image);
			Assert.AreEqual(sut.Text, item.Text);
			Assert.AreEqual(sut.ToolTipText, item.ToolTipText);
			Assert.AreEqual(externalObject, sut.ExternalObject);
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

		[TestMethod]
		public void ToolStripItemGroup_ApplicationIdle()
		{
			var testItem = new TestToolStripItem();
			var sut = new ToolStripItemGroupManager(null);
			var group1 = sut.Create<ApplicationIdleToolStripItemGroup.Group1>(new ToolStripItem[] { testItem });
			var group2 = sut.Create<ApplicationIdleToolStripItemGroup.Group2>(new ToolStripItem[] { testItem });
			var group3 = sut.Create<ApplicationIdleToolStripItemGroup.Group3>(new ToolStripItem[] { testItem });

			Application.RaiseIdle(null);

			Assert.IsTrue(group1.ApplicationIdleCalled);
			Assert.IsTrue(group2.ApplicationIdleCalled);
			Assert.IsTrue(group3.ApplicationIdleCalled);
		}

		[TestMethod]
		public void ToolStripItemGroup_Enabled()
		{
			var toolStripMenuItem = new ToolStripMenuItem();
			var toolStripButton = new ToolStripButton();
			var toolStripMenuButton = new ToolStripSplitButton();
			var sut = new TestToolStripItemGroup();
			sut.Add(toolStripMenuItem);
			sut.Add(toolStripButton);
			sut.Add(toolStripMenuButton);

			sut.ForEach(i => Assert.IsTrue(i.Enabled));
			Assert.IsTrue(sut.Enabled);
			sut.Enabled = false;
			sut.ForEach(i => Assert.IsFalse(i.Enabled));
			Assert.IsFalse(sut.Enabled);
		}

		[TestMethod]
		public void ToolStripItemGroup_Visible()
		{
			var toolStripMenuItem = new ToolStripMenuItem() { Text = "Menu" };
			var toolStripButton = new ToolStripButton() { Text = "Button" };
			var toolStripMenuButton = new ToolStripSplitButton() { Text = "Split" };
			var sut = new TestToolStripItemGroup();
			sut.Add(toolStripMenuItem);
			sut.Add(toolStripButton);
			sut.Add(toolStripMenuButton);

			sut.ForEach(i => Assert.IsTrue(i.Available));
			Assert.IsTrue(sut.Visible);
			sut.Visible = false;
			sut.ForEach(i => Assert.IsFalse(i.Available));
			Assert.IsFalse(sut.Visible);
		}
	}
}
