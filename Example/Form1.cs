using Example.Groups;
using TNT.ToolStripItemManager;

namespace Example;

public partial class Form1 : Form
{
  ToolStripItemGroupManager ItemGroupManager;
  ToolStripItemCheckboxGroupManager CheckboxItemGroupManager;

  public Form1()
  {
    InitializeComponent();

    ItemGroupManager = new ToolStripItemGroupManager(toolStripStatusLabel1)
    {
      IsLicensed = IsLicensed,
      OnClick = item =>
      {
        if (item is One)
        {
          using OpenFileDialog ofd = new OpenFileDialog();
          ofd.ShowDialog();
          Text = ofd.FileName;
        }
        else if (item is Open)
        {
          using OpenFileDialog ofd = new OpenFileDialog();
          ofd.ShowDialog();
        }
      },
    };
    CheckboxItemGroupManager = new ToolStripItemCheckboxGroupManager(toolStripStatusLabel1)
    {
      IsLicensed = IsLicensed,
      OnClick = item =>
      {
        if (item is Left)
        {
          label1.TextAlign = ContentAlignment.MiddleLeft;
        }
        else if (item is Center)
        {
          label1.TextAlign = ContentAlignment.MiddleCenter;
        }
        else if (item is Right)
        {
          label1.TextAlign = ContentAlignment.MiddleRight;
        }
      },
    };

    ItemGroupManager.Create<One>(new ToolStripItem[] { oneToolStripMenuItem, toolStripButton1, aToolStripMenuItem }, oneToolStripMenuItem.Image);
    ItemGroupManager.Create<Open>(new ToolStripItem[] { twoToolStripMenuItem, toolStripSplitButton2, bToolStripMenuItem });
    ItemGroupManager.Create<Enable>(new ToolStripItem[] { threeToolStripMenuItem, toolStripButton3, cToolStripMenuItem });
    ItemGroupManager.Create<HideShow>(new ToolStripItem[] { toolStripButton4, dToolStripMenuItem });

    CheckboxItemGroupManager.CreateHome<Left>(new ToolStripItem[] { tsb1, leftToolStripMenuItem }).Checked = true;
    CheckboxItemGroupManager.Create<Center>(new ToolStripItem[] { tsb2, centerToolStripMenuItem });
    CheckboxItemGroupManager.Create<Right>(new ToolStripItem[] { tsb3, rightToolStripMenuItem });
  }

  private bool IsLicensed(bool allowMessageBox, ToolStripItemGroup toolStripItemGroup)
  {
    if (!checkBox1.Checked && allowMessageBox)
    {
      MessageBox.Show(this, "This feature is not licensed", "Not licensed", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    return checkBox1.Checked;
  }

  private void label1_Click(object sender, EventArgs e)
  {
    var checkedItemGroup = CheckboxItemGroupManager.FirstOrDefault(i => i.Value.Checked == true).Value;
    if (checkedItemGroup != null)
    {
      checkedItemGroup.Checked = false;
    }
  }

  private void button1_Click(object sender, EventArgs e)
  {
    CheckboxItemGroupManager.Toggle();
  }

  private void checkBox1_CheckedChanged(object sender, EventArgs e)
  {
    if (sender is CheckBox checkBox)
    {
      ItemGroupManager.LicensedChanged(checkBox.Checked);
      CheckboxItemGroupManager.LicensedChanged(checkBox.Checked);
    }
  }
}
