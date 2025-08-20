using TNT.ToolStripItemManager;

namespace Example.Groups;

public class One : ToolStripItemGroup
{
	public One()
		: base(ResourceToImage("Example.Images.shape_align_bottom.png"))
	{

	}

	public override void OnMouseClick(object? sender, EventArgs e)
	{
		using (OpenFileDialog ofd = new OpenFileDialog())
		{
			ofd.ShowDialog();
			if (ExternalObject is Form1 form)
			{
				form.Text = ofd.FileName;
			}
		}
	}

	public override string Text => "One";

	public override string ToolTipText => "Tool tip one";

	public override void OnApplicationIdle(object? sender, EventArgs e)
	{
		base.OnApplicationIdle(sender, e);
		if (ToolStripItemGroupManager == null) return;

		ToolStripItemGroupManager.TryGetValue("Enable", out ToolStripItemGroup? isEnabled );
		ToolStripItemGroupManager.TryGetValue("Hide/Show", out ToolStripItemGroup? isVisible);

		if (isEnabled != null)
			Enabled = isEnabled.Checked;
		if (isVisible != null)
			Visible = isVisible.Checked;
	}
}
