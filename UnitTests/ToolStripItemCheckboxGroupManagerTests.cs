using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using TNT.ToolStripItemManager.Tests.Groups;

namespace TNT.ToolStripItemManager.Tests
{
	[TestClass]
	public class ToolStripItemCheckboxGroupManagerTests
	{
		[TestMethod]
		public void ToolStripItemCheckboxGroupManager_Test()
		{
			var toolStripStatusLabel = new ToolStripStatusLabel();
			var toolStripMenuItemOne = new ToolStripMenuItem();
			var toolStripButtonOne = new ToolStripButton();
			var toolStripMenuItemTwo = new ToolStripMenuItem();
			var toolStripButtonTwo = new ToolStripButton();
			var obj = new object();
			var bitmap = ToolStripItemGroup.ResourceToImage("TNT.ToolStripItemManager.Tests.Images.shape_align_bottom.png");
			var itemGroupManager = new ProtectedAccess(toolStripStatusLabel);
			var one = itemGroupManager.Create<One>(new ToolStripItem[] { toolStripMenuItemOne, toolStripButtonOne }, bitmap, obj);
			var two = itemGroupManager.Create<Two>(new ToolStripItem[] { toolStripMenuItemTwo, toolStripButtonTwo }, bitmap, obj);

			one.Checked = true;

			Assert.AreEqual(one.Text, toolStripMenuItemOne.Text);
			Assert.AreEqual(one.ToolTipText, toolStripMenuItemOne.ToolTipText);
			Assert.IsTrue(toolStripMenuItemOne.Checked);
			Assert.IsTrue(toolStripButtonOne.Checked);
			Assert.AreEqual(two.Text, toolStripMenuItemTwo.Text);
			Assert.AreEqual(two.ToolTipText, toolStripMenuItemTwo.ToolTipText);
			Assert.IsFalse(toolStripMenuItemTwo.Checked);
			Assert.IsFalse(toolStripButtonTwo.Checked);

			toolStripMenuItemOne.PerformClick();

			toolStripMenuItemTwo.PerformClick();

			Assert.IsFalse(toolStripMenuItemOne.Checked);
			Assert.IsFalse(toolStripButtonOne.Checked);
			Assert.IsTrue(toolStripMenuItemTwo.Checked);
			Assert.IsTrue(toolStripButtonTwo.Checked);
		}


		[TestMethod]
		public void ToolStripItemGroupManager_CreateHome()
		{
			var toolStripStatusLabel = new ToolStripStatusLabel();
			var toolStripMenuItemOne = new ToolStripMenuItem();
			var toolStripButtonOne = new ToolStripButton();
			var toolStripMenuItemTwo = new ToolStripMenuItem();
			var toolStripButtonTwo = new ToolStripButton();
			var obj = new object();
			var bitmap = ToolStripItemGroup.ResourceToImage("TNT.ToolStripItemManager.Tests.Images.shape_align_bottom.png");
			var itemGroupManager = new ProtectedAccess(toolStripStatusLabel);
			var nonHomeGroup= itemGroupManager.Create<One>(new ToolStripItem[] { toolStripMenuItemOne, toolStripButtonOne }, bitmap, obj);

			Assert.IsFalse(nonHomeGroup.Checked);
			Assert.IsNull(itemGroupManager.HomeGroup);

			itemGroupManager.Toggle();
			Assert.IsFalse(nonHomeGroup.Checked);

			var homeGroup = itemGroupManager.CreateHome<Two>(new ToolStripItem[] { toolStripMenuItemTwo, toolStripButtonTwo }, bitmap, obj);

			Assert.AreEqual(homeGroup, itemGroupManager.HomeGroup);

			Assert.IsNull(itemGroupManager.PreviouslyCheckedGroup);
			toolStripMenuItemTwo.PerformClick();
			Assert.IsNull(itemGroupManager.PreviouslyCheckedGroup);

			toolStripMenuItemOne.PerformClick();
			Assert.AreEqual(nonHomeGroup, itemGroupManager.PreviouslyCheckedGroup);
			Assert.IsTrue(nonHomeGroup.Checked);

			itemGroupManager.Toggle();
			Assert.IsFalse(nonHomeGroup.Checked);
			Assert.IsTrue(homeGroup.Checked);

			itemGroupManager.Toggle();
			Assert.IsTrue(nonHomeGroup.Checked);
			Assert.IsFalse(homeGroup.Checked);
		}

		public class ProtectedAccess : ToolStripItemCheckboxGroupManager
		{
			public ProtectedAccess(ToolStripStatusLabel statusLabel)
				: base(statusLabel)
			{
			}

			public void CallApplicationIdle()
			{
				base.Application_Idle(null, null);
			}
		}
	}
}
