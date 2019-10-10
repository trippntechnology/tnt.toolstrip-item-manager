using System;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace Test.Groups
{
	public class Two : ToolStripItemGroup
	{
		public Two()
			:base (ResourceToImage("Test.Images.shape_align_center.png"))
		{

		}

		public override string Text => "Two";

		public override string ToolTipText => "Tool tip two";

		public override void OnMouseClick(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.ShowDialog();
			}
		}
	}
}
