using System;
using System.Windows.Forms;

namespace TNT.ToolStripItemManager.Tests.Helpers
{
	class TestToolStripItem : ToolStripItem
	{
		public virtual new event EventHandler MouseEnter;
		public virtual new event EventHandler MouseLeave;

		public void PerformMouseEnter(EventArgs e)
		{
			MouseEnter?.Invoke(null, e);
		}

		public void PerformMouseLeave(EventArgs e)
		{
			MouseLeave?.Invoke(null, e);
		}
	}
}
