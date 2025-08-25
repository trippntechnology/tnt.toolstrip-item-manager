using TNT.ToolStripItemManager;

namespace Example.Groups;

class Left() : ToolStripItemGroup(ResourceToImage("Example.Images.shape_align_left.png"))
{
  public override string Text => "Left";

  public override string ToolTipText => "Left TT (License Required)";
}
