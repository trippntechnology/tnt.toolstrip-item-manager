using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace Test.Groups
{
	public class Four : ToolStripItemGroup
	{
		public Four(ToolStripStatusLabel label)
			: base(null, label, null)
		{
			base.Image = ResourceToImage("Test.Images.shape_align_middle.png");
		}

		public override string Text => "Four";

		public override string ToolTipText => "Tool tip four";

		public override bool CheckOnClick => true;
	}
}
