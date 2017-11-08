using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace Test.Groups
{
	public class Three : ToolStripItemGroup
	{
		public Three(ToolStripStatusLabel label)
			: base(null, label, null)
		{
			base.Image = ResourceToImage("Test.Images.shape_align_left.png");
		}

		public override string Text => "Three";

		public override string ToolTipText => "Tool tip three";

		public override bool CheckOnClick => true;
	}
}
