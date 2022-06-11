using TNT.ToolStripItemManager;

namespace Example.Groups;

class Right : ToolStripItemGroup
{
	public Right()
		: base(ResourceToImage("Example.Images.shape_align_right.png"))
	{
	}

	public override string Text => "Right";

	public override string ToolTipText => "Right TT";

	public override void CheckedChanged(object sender, EventArgs e)
	{
		var toolStripItem = sender as ToolStripItem;
		if (!toolStripItem.GetChecked()) return;

		(ExternalObject as Label).TextAlign = ContentAlignment.MiddleRight;
		base.CheckedChanged(sender, e);
	}
}
