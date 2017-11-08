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
		public One(Image image, ToolStripStatusLabel label, EventHandler onClick)
			: base(image, label, onClick)
		{
			base.Image = ResourceToImage("Test.Images.shape_align_bottom.png");
		}

		public override string Text => "One";

		public override string ToolTipText => "Tool tip one";
	}
}
