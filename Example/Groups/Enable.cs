using TNT.ToolStripItemManager;

namespace Example.Groups;

public class Enable() : ToolStripItemGroup(ResourceToImage("Example.Images.accept.png"))
{
  public override string Text => "Enable";

  public override string ToolTipText => "Enable Example (License Required)";

  public override bool CheckOnClick => true;
}
