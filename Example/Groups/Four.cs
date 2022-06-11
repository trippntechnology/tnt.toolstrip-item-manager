using TNT.ToolStripItemManager;

namespace Example.Groups;

public class Four : ToolStripItemGroup
{
	public Four()
		: base(ResourceToImage("Example.Images.shape_align_middle.png"))
	{

	}

	public override string Text => "Four";

	public override string ToolTipText => "Tool tip four";

	public override bool CheckOnClick => true;

	public override void OnLicenseChanged(bool isLicensed)
	{
		base.OnLicenseChanged(isLicensed);
		if (!isLicensed) Checked = false;
	}
}
