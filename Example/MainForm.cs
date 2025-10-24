using Example.Groups;
using System.Diagnostics;
using TNT.ToolStripItemManager;

namespace Example;

public partial class MainForm : Form
{
  private readonly ToolStripItemGroupManager _toolStripItemGroupManager;
  private readonly ToolStripItemRadioGroupManager _toolStripItemRadioGroupManager;

  public MainForm()
  {
    InitializeComponent();

    //Application.Idle += Application_Idle;

    _toolStripItemGroupManager = new ToolStripItemGroupManager()
    {
      OnClick = OnClick,
      OnToolTipChange = toolTipText => toolStripStatusLabel1.Text = toolTipText,
      OnIdle = Application_Idle
    };
    _toolStripItemGroupManager.Create<Settings>([settingsToolStripMenuItem, settingsToolStripButton, splitToolStripMenuItem1]);
    _toolStripItemGroupManager.Create<Check>([checkToolStripButton, checkToolStripMenuItem]);
    _toolStripItemGroupManager.Create<Handyman>([handymanToolStripSplitButton]);
    _toolStripItemGroupManager.Create<License>([licenseToolStripButton]);

    _toolStripItemRadioGroupManager = new()
    {
      OnClick = OnClick,
      OnToolTipChange = toolTipText => toolStripStatusLabel1.Text = toolTipText,
      OnIdle = Application_Idle
    };
    _toolStripItemRadioGroupManager.Create<AlignLeft>([alignLeftToolStripButton, radioGroupItem1ToolStripMenuItem]);
    _toolStripItemRadioGroupManager.Create<AlignCenter>([alignCenterToolStripButton, radioGroupItem2ToolStripMenuItem]);
    _toolStripItemRadioGroupManager.Create<AlignRight>([alignRightToolStripButton, radioGroupItem3ToolStripMenuItem]);
  }

  private void OnClick(ToolStripItemGroup toolStripItemGroup)
  {
    Debug.WriteLine($"{toolStripItemGroup} clicked");
    if (toolStripItemGroup is License license && !isLicensedCheckBox.Checked)
    {
      license.Checked = false;
    }
  }

  private void Application_Idle(object? sender, EventArgs e)
  {
    if (sender is Settings settings)
    {
      settings.Enabled = checkBox1.Checked;
    }
    else if (sender is Handyman handyman)
    {
      var alignCenter = _toolStripItemRadioGroupManager.Find(i => i is AlignCenter);
      handyman.Enabled = !alignCenter?.Checked ?? true;
    }
  }
}
