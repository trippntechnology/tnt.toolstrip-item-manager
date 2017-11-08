using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace Test.Groups
{
	public class Two : ToolStripItemGroup
	{
		public Two(ToolStripStatusLabel label, EventHandler click)
			:base(null, label, click)
		{
			base.Image = ResourceToImage("Test.Images.shape_align_center.png");
		}

		public override string Text => "Two";

		public override string ToolTipText => "Tool tip two";
	}
}
