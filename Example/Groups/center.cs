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

	public override void CheckedChanged(object? sender, EventArgs e)
	{
		if (sender is ToolStripItem toolStripItem)
		{
			if (!toolStripItem.GetChecked()) return;
		}
		if (ExternalObject is Label label) { label.TextAlign = ContentAlignment.MiddleCenter; }
		base.CheckedChanged(sender, e);
	}
}
