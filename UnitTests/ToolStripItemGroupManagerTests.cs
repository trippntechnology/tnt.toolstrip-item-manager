using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using TNT.ToolStripItemManager.Tests.Groups;

namespace TNT.ToolStripItemManager.Tests
{
	[TestClass()]
	public class ToolStripItemGroupManagerTests
	{
		[TestMethod()]
		public void ToolStripItemGroupManager_Test()
		{
			var called = false;
			var toolStripStatusLabel = new ToolStripStatusLabel();
			var toolStripMenuItem = new ToolStripMenuItem();
			var obj = new object();
			var bitmap = ToolStripItemGroup.ResourceToImage("TNT.ToolStripItemManager.Tests.Images.shape_align_bottom.png");
			var itemGroupManager = new ProtectedAccess(toolStripStatusLabel);
			var one = itemGroupManager.Create<One>(new ToolStripItem[] { toolStripMenuItem }, bitmap, obj, (a, b) =>
			 {
				 called = true;
			 });

			Assert.AreEqual(one.Text, toolStripMenuItem.Text);
			Assert.AreEqual(one.ToolTipText, toolStripMenuItem.ToolTipText);

			//Assert.IsTrue(called);
		}

		[TestMethod()]
		public void Create_Test()
		{

		}

		public class ProtectedAccess : ToolStripItemGroupManager
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