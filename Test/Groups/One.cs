using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace Test.Groups
{
	public class One : ToolStripItemGroup
	{
		public One(Image image, ToolStripStatusLabel label)
			: base(image, label)
		{
			base.Image = ResourceToImage("Test.Images.shape_align_bottom.png");
			base.OnMouseClick += OpenFile;
		}

		private void OpenFile(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.ShowDialog();
			}
		}

		public override string Text => "One";

		public override string ToolTipText => "Tool tip one";
	}
}
