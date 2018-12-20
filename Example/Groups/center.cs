using System;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace Test.Groups
{
	class Center : ToolStripItemGroup
	{
		public Center()
			: base(ResourceToImage("Test.Images.shape_align_center.png"))
		{
		}

		public override string Text => "Center";

		public override string ToolTipText => "Center TT";

		public override void CheckedChanged(object sender, EventArgs e)
		{
			var toolStripItem = sender as ToolStripItem;
			if (!toolStripItem.GetChecked()) return;

			(ExternalObject as Label).TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			base.CheckedChanged(sender, e);
		}
	}
}
