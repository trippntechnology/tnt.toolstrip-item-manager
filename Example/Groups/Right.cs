using System;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace Test.Groups
{
	class Right : ToolStripItemGroup
	{
		public Right()
			: base(ResourceToImage("Test.Images.shape_align_right.png"))
		{
		}

		public override string Text => "Right";

		public override string ToolTipText => "Right TT";

		public override void CheckedChanged(object sender, EventArgs e)
		{
			var toolStripItem = sender as ToolStripItem;
			if (!toolStripItem.GetChecked()) return;

			(ExternalObject as Label).TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			base.CheckedChanged(sender, e);
		}
	}
}
