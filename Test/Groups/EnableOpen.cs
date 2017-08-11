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
	public class EnableOpen : ToolStripItemGroup
	{
		public EnableOpen(Image image, ToolStripStatusLabel toolStripStatusLabel, EventHandler onMouseClick) : base(image, toolStripStatusLabel, onMouseClick)
		{
		}

		public override string Text => "Enable Open";

		public override string ToolTipText => "Enables Open";

		public override bool CheckOnClick => true;
	}
}
