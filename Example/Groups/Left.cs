using TNT.ToolStripItemManager;

namespace Example.Groups;

class Left : ToolStripItemGroup
{
	public Left()
		: base(ResourceToImage("Example.Images.shape_align_left.png"))
	{
	}

	public override string Text => "Left";

	public override string ToolTipText => "Left TT";

	public override void CheckedChanged(object sender, EventArgs e)
	{
		var toolStripItem = sender as ToolStripItem;
		if (!toolStripItem.GetChecked()) return;

		(ExternalObject as Label).TextAlign = ContentAlignment.MiddleLeft;
		base.CheckedChanged(sender, e);
	}
}
