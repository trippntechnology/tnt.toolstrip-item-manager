using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.Groups;

namespace Test
{
	public partial class Form1 : Form
	{
		private One _One;
		private Two _Two;
		private Three _Three;
		private Four _Four;

		public Form1()
		{
			InitializeComponent();

			_One = new One(oneToolStripMenuItem.Image, toolStripStatusLabel1);
			_One.Add(oneToolStripMenuItem);
			_One.Add(toolStripButton1);
			_One.Add(aToolStripMenuItem);

			_Two = new Two(toolStripStatusLabel1, Open_OnMouseClick);
			_Two.Add(twoToolStripMenuItem);
			_Two.Add(toolStripSplitButton2);
			_Two.Add(bToolStripMenuItem);

			_Three = new Three(toolStripStatusLabel1);
			_Three.Add(threeToolStripMenuItem);
			_Three.Add(toolStripButton3);
			_Three.Add(cToolStripMenuItem);

			_Four = new Four(toolStripStatusLabel1);
			_Four.Add(toolStripButton4);

			Application.Idle += Application_Idle;
		}

		private void DropButton_OnMouseClick(object sender, EventArgs e)
		{
			MessageBox.Show("DropButton_OnMouseClick");
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			_One.Enabled = _Three.Checked;
			_One.Visible = _Four.Checked;
		}

		private void Open_OnMouseClick(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.ShowDialog();
			}
		}

		private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
		{
			MessageBox.Show("ButtonClick");
		}
	}
}
