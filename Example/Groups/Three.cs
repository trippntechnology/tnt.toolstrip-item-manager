using TNT.ToolStripItemManager;

namespace Example.Groups;

public class Three : ToolStripItemGroup
{
	public Three()
		: base(ResourceToImage("Example.Images.shape_align_left.png"))
	{
	}

	public override string Text => "Three";

	public override string ToolTipText => "Tool tip three";

	public override bool CheckOnClick => true;
}
