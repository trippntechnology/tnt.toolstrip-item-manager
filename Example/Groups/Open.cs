using System;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace Example.Groups;

public class Open : ToolStripItemGroup
{
	public Open()
		: base(ResourceToImage("Example.Images.folder.png"))
	{

	}

	public override string Text => "File Open";

	public override string ToolTipText => "File Open Example (License Required)";

	public override void OnMouseClick(object? sender, EventArgs e)
	{
		using (OpenFileDialog ofd = new OpenFileDialog())
		{
			ofd.ShowDialog();
		}
	}
}
