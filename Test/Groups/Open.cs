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
	public class Open : ToolStripItemGroup
	{
		public Open(Image image, ToolStripStatusLabel toolStripStatusLabel, EventHandler onMouseClick) : base(image, toolStripStatusLabel, onMouseClick)
		{
		}

		public override string Text => "&Open";

		public override string ToolTipText => "Opens new file";
	}
}
