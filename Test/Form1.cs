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
		private Open _Open;
		private EnableOpen _EnableOpen;
		private DropButton _DropButton;

		public Form1()
		{
			InitializeComponent();

			_Open =  new Open(toolStripButton1.Image, toolStripStatusLabel1, Open_OnMouseClick);
			_Open.Add(toolStripButton1);
			_Open.Add(toolStripMenuItem2);

			_EnableOpen = new EnableOpen(toolStripButton2.Image, toolStripStatusLabel1, EnableOpen_OnMouseClick);
			_EnableOpen.Add(toolStripButton2);
			_EnableOpen.Add(toolStripMenuItem3);

			_DropButton = new DropButton(toolStripSplitButton1.Image, toolStripStatusLabel1, DropButton_OnMouseClick);
			_DropButton.Add(toolStripSplitButton1);
			_DropButton.Add(toolStripMenuItem4);

			Application.Idle += Application_Idle;
		}

		private void DropButton_OnMouseClick(object sender, EventArgs e)
		{
			MessageBox.Show("DropButton_OnMouseClick");
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			_Open.Enabled = _EnableOpen.Checked;
		}

		private void Open_OnMouseClick(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.ShowDialog();
			}
		}

		private void EnableOpen_OnMouseClick(object sender, EventArgs e)
		{
			EnableOpen eo = sender as EnableOpen;

			//_Open.Enabled = eo.Checked;
		}

		private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
		{
			MessageBox.Show("ButtonClick");
		}
	}
}
