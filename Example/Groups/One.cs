using TNT.ToolStripItemManager;

namespace Example.Groups;

public class One() : ToolStripItemGroup(ResourceToImage("Example.Images.shape_align_bottom.png"))
{
  public override string Text => "One";

  public override string ToolTipText => "Tool tip one";

  public override void OnApplicationIdle(object? sender, EventArgs e)
  {
    base.OnApplicationIdle(sender, e);
    if (ToolStripItemGroupManager == null) return;

    ToolStripItemGroupManager.TryGetValue("Enable", out ToolStripItemGroup? isEnabled);
    ToolStripItemGroupManager.TryGetValue("Hide/Show", out ToolStripItemGroup? isVisible);

    Enabled = isEnabled?.Checked ?? false;
    Visible = isVisible?.Checked ?? false;
  }
}
