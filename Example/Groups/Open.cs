using TNT.ToolStripItemManager;

namespace Example.Groups;

public class Open() : ToolStripItemGroup(ResourceToImage("Example.Images.folder.png"))
{
  public override string Text => "File Open";

  public override string ToolTipText => "File Open Example (License Required)";
}
