﻿using TNT.ToolStripItemManager;

namespace Example.Groups;

public class One : ToolStripItemGroup
{
	public One()
		: base(ResourceToImage("Example.Images.shape_align_bottom.png"))
	{

	}

	public override void OnMouseClick(object sender, EventArgs e)
	{
		using (OpenFileDialog ofd = new OpenFileDialog())
		{
			ofd.ShowDialog();
			Form1 form = ExternalObject as Form1;
			form.Text = ofd.FileName;
		}
	}

	public override string Text => "One";

	public override string ToolTipText => "Tool tip one";

	public override void OnApplicationIdle(object sender, EventArgs e)
	{
		base.OnApplicationIdle(sender, e);
		ToolStripItemGroupManager.TryGetValue("Three", out ToolStripItemGroup three);
		ToolStripItemGroupManager.TryGetValue("Four", out ToolStripItemGroup four);

		if (three != null)
			Enabled = three.Checked;
		if (four != null)
			Visible = four.Checked;
	}
}
