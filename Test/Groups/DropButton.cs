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
	public class DropButton : ToolStripItemGroup
	{
		public DropButton(Image image, ToolStripStatusLabel toolStripStatusLabel, EventHandler onMouseClick) : base(image, toolStripStatusLabel, onMouseClick)
		{
		}

		public override string Text => "Drop Button";

		public override string ToolTipText => "Drop Button Example";
	}
}
