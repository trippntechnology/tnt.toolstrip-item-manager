using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using TNT.ToolStripItemManager.Extension; // Ensure NUnit attributes and Assert are available

namespace NUnitTests.Extension;

[ExcludeFromCodeCoverage]
internal class ToolStripItemExtTests
{
  static List<ToolStripItem> toolStripItems = new List<ToolStripItem>();

  [OneTimeSetUp]
  public static void InitializeClass()
  {
    toolStripItems.Add(new ToolStripMenuItem("Menu"));
    toolStripItems.Add(new ToolStripButton("Button"));
  }

  [Test]
  public void Extensions_GetSetCheckedTest()
  {
    toolStripItems.ForEach(it => Assert.That(it.GetChecked(), Is.False));
    toolStripItems.ForEach(it => it.SetChecked(true));
    toolStripItems.ForEach(it => Assert.That(it.GetChecked(), Is.True));
    toolStripItems.ForEach(it => it.SetChecked(false));
    toolStripItems.ForEach(it => Assert.That(it.GetChecked(), Is.False));
  }
}
