using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.ToolStripItemManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TNT.ToolStripItemManager.Tests
{
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
}