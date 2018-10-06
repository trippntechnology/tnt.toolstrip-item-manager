using System;
using System.Windows.Forms;
using Test.Groups;
using TNT.ToolStripItemManager;

namespace Test
{
	public partial class Form1 : Form
	{
		private One _One;
		private Two _Two;
		private Three _Three;
		private Four _Four;

		ToolStripItemGroupManager ItemGroupManager;

		public Form1()
		{
			InitializeComponent();

			ItemGroupManager = new ToolStripItemGroupManager(toolStripStatusLabel1);

			_One = ItemGroupManager.Create<One>(new ToolStripItem[] { oneToolStripMenuItem, toolStripButton1, aToolStripMenuItem }, oneToolStripMenuItem.Image, this);
			_Two = ItemGroupManager.Create<Two>(new ToolStripItem[] { twoToolStripMenuItem, toolStripSplitButton2, bToolStripMenuItem }, onClick: Open_OnMouseClick);
			_Three = ItemGroupManager.Create<Three>(new ToolStripItem[] { threeToolStripMenuItem, toolStripButton3, cToolStripMenuItem });
			_Four = ItemGroupManager.Create<Four>(new ToolStripItem[] { toolStripButton4 });
		}

		private void Open_OnMouseClick(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.ShowDialog();
			}
		}
	}
}
