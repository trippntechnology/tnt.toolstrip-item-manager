using TNT.ToolStripItemManager;

namespace Example.Groups;

class Center() : ToolStripItemGroup(ResourceToImage("Example.Images.shape_align_center.png"))
{
  public override string Text => "Center";

  public override string ToolTipText => "Center TT (License Required)";
}
