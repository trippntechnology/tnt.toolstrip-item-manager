using TNT.ToolStripItemManager;

namespace Example.Groups;

class Right : ToolStripItemGroup
{
	public Right()
		: base(ResourceToImage("Example.Images.shape_align_right.png"))
	{
	}

	public override string Text => "Right";

	public override string ToolTipText => "Right TT (License Required)";

	public override void CheckedChanged(object? sender, EventArgs e)
	{
		if (sender is ToolStripItem toolStripItem)
		{
			if (!toolStripItem.GetChecked()) return;
			if (ExternalObject is Label label) label.TextAlign = ContentAlignment.MiddleRight;
		}

		base.CheckedChanged(sender, e);
	}
}
