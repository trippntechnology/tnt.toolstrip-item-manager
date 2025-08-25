using TNT.ToolStripItemManager;

namespace Example.Groups;

public class HideShow() : ToolStripItemGroup(ResourceToImage("Example.Images.eye.png"))
{
  public override string Text => "Hide/Show";

  public override string ToolTipText => "Hide/Show Example (Licensed Required)";

  public override bool CheckOnClick => true;

  public override void OnLicenseChanged(bool isLicensed)
  {
    base.OnLicenseChanged(isLicensed);
    if (!isLicensed) Checked = false;
  }
}
