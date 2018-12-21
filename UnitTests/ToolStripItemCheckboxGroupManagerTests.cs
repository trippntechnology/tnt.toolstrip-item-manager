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
			var called1 = false;
			var called2 = false;
			var toolStripStatusLabel = new ToolStripStatusLabel();
			var toolStripMenuItemOne = new ToolStripMenuItem();
			var toolStripButtonOne = new ToolStripButton();
			var toolStripMenuItemTwo = new ToolStripMenuItem();
			var toolStripButtonTwo = new ToolStripButton();
			var obj = new object();
			var bitmap = ToolStripItemGroup.ResourceToImage("TNT.ToolStripItemManager.Tests.Images.shape_align_bottom.png");
			var itemGroupManager = new ProtectedAccess(toolStripStatusLabel);
			var one = itemGroupManager.Create<One>(new ToolStripItem[] { toolStripMenuItemOne, toolStripButtonOne }, bitmap, obj, (a, b) => { called1 = true; });
			var two = itemGroupManager.Create<Two>(new ToolStripItem[] { toolStripMenuItemTwo, toolStripButtonTwo }, bitmap, obj, (a, b) => { called2 = true; });

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
			Assert.IsTrue(called1);

			toolStripMenuItemTwo.PerformClick();
			Assert.IsTrue(called2);

			Assert.IsFalse(toolStripMenuItemOne.Checked);
			Assert.IsFalse(toolStripButtonOne.Checked);
			Assert.IsTrue(toolStripMenuItemTwo.Checked);
			Assert.IsTrue(toolStripButtonTwo.Checked);
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
