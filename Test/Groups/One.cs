using System;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace Test.Groups
{
	public class One : ToolStripItemGroup
	{
		public One()
			: base(ResourceToImage("Test.Images.shape_align_bottom.png"))
		{

		}

		public override void MouseClick(object sender, EventArgs e)
		{
			base.MouseClick(sender, e);
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.ShowDialog();
				Form1 form = base.ExternalObject as Form1;
				form.Text = ofd.FileName;
			}
		}

		public override string Text => "One";

		public override string ToolTipText => "Tool tip one";

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			base.OnApplicationIdle(sender, e);

			var three = base.ToolStripItemGroupManager["Three"];
			var four = base.ToolStripItemGroupManager["Four"];

			if (three!= null)
				Enabled = three.Checked;
			if (four != null)
				Visible = four.Checked;
		}
	}
}
