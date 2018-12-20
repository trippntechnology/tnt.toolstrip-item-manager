using System;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace Test.Groups
{
	class Left : ToolStripItemGroup
	{
		public Left()
			: base(ResourceToImage("Test.Images.shape_align_left.png"))
		{
		}

		public override string Text => "Left";

		public override string ToolTipText => "Left TT";

		public override void CheckedChanged(object sender, EventArgs e)
		{
			var toolStripItem = sender as ToolStripItem;
			if (!toolStripItem.GetChecked()) return;

			(ExternalObject as Label).TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			base.CheckedChanged(sender, e);
		}
	}
}
