using Example.Groups;
using TNT.ToolStripItemManager;

namespace Example;

public partial class Form1 : Form
{
	private One _One;
	private Open _Two;
	private Enable _Three;
	private HideShow _Four;

	ToolStripItemGroupManager ItemGroupManager;
	ToolStripItemCheckboxGroupManager CheckboxItemGroupManager;

	public Form1()
	{
		InitializeComponent();

		ItemGroupManager = new ToolStripItemGroupManager(toolStripStatusLabel1)
		{
			IsLicensed = IsLicensed,
			OnItemGroupClicked = item =>
			{
				System.Diagnostics.Debug.WriteLine($"ItemGroupManager: {item}");
			}
		};
		CheckboxItemGroupManager = new ToolStripItemCheckboxGroupManager(toolStripStatusLabel1)
		{
			IsLicensed = IsLicensed,
      OnItemGroupClicked = item =>
      {
        System.Diagnostics.Debug.WriteLine($"CheckboxItemGroupManager: {item}");
      }
    };

		_One = ItemGroupManager.Create<One>(new ToolStripItem[] { oneToolStripMenuItem, toolStripButton1, aToolStripMenuItem }, oneToolStripMenuItem.Image, this);
		_Two = ItemGroupManager.Create<Open>(new ToolStripItem[] { twoToolStripMenuItem, toolStripSplitButton2, bToolStripMenuItem });
		_Three = ItemGroupManager.Create<Enable>(new ToolStripItem[] { threeToolStripMenuItem, toolStripButton3, cToolStripMenuItem });
		_Four = ItemGroupManager.Create<HideShow>(new ToolStripItem[] { toolStripButton4, dToolStripMenuItem });

		CheckboxItemGroupManager.CreateHome<Left>(new ToolStripItem[] { tsb1, leftToolStripMenuItem }, externalObject: label1).Checked = true;
		CheckboxItemGroupManager.Create<Center>(new ToolStripItem[] { tsb2, centerToolStripMenuItem }, externalObject: label1);
		CheckboxItemGroupManager.Create<Right>(new ToolStripItem[] { tsb3, rightToolStripMenuItem }, externalObject: label1);
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
