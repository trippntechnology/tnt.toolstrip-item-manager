using System;
using System.Drawing;

namespace TNT.ToolStripItemManager.Tests.Helpers
{
	class TestToolStripItemGroupAddOverride : TestToolStripItemGroup
	{
		public TestToolStripItemGroupAddOverride(Image image = null)
			: base(image)
		{

		}

		public TestToolStripItemGroupAddOverride() : base() { }

		public override void Add<T>(T toolStripItem)
		{
			var ttsmi = toolStripItem as TestToolStripItem;
			base.Add(ttsmi);
			ttsmi.MouseEnter += this.MouseEnter;
			ttsmi.MouseLeave += this.MouseLeave;
		}
	}
}
