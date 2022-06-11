using TNT.ToolStripItemManager;

namespace Example.Groups;

class Center : ToolStripItemGroup
{
	public Center()
		: base(ResourceToImage("Example.Images.shape_align_center.png"))
	{
	}

	public override string Text => "Center";

	public override string ToolTipText => "Center TT";

	public override void CheckedChanged(object sender, EventArgs e)
	{
		var toolStripItem = sender as ToolStripItem;
		if (!toolStripItem.GetChecked()) return;

		(ExternalObject as Label).TextAlign = ContentAlignment.MiddleCenter;
		base.CheckedChanged(sender, e);
	}
}
