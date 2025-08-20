using TNT.ToolStripItemManager;

namespace Example.Groups;

class Left : ToolStripItemGroup
{
	public Left()
		: base(ResourceToImage("Example.Images.shape_align_left.png"))
	{
	}

	public override string Text => "Left";

	public override string ToolTipText => "Left TT (License Required)";

	public override void CheckedChanged(object? sender, EventArgs e)
	{
		if (sender is ToolStripItem toolStripItem)
		{
			if (!toolStripItem.GetChecked()) return;
			if (ExternalObject is Label label) label.TextAlign = ContentAlignment.MiddleLeft;
		}

		base.CheckedChanged(sender, e);
	}
}
