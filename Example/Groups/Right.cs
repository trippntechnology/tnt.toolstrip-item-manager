using TNT.ToolStripItemManager;

namespace Example.Groups;

class Right() : ToolStripItemGroup(ResourceToImage("Example.Images.shape_align_right.png"))
{
  public override string Text => "Right";

  public override string ToolTipText => "Right TT (License Required)";
}
