using System.Diagnostics.CodeAnalysis;

namespace UnitTests.Helpers;

[ExcludeFromCodeCoverage]
class TestToolStripItemGroupAddOverride : TestToolStripItemGroup
{
  public TestToolStripItemGroupAddOverride(Image? image = null)
    : base(image)
  {

  }

  public TestToolStripItemGroupAddOverride() : base() { }

  public override void Add<T>(T toolStripItem)
  {
    var ttsmi = toolStripItem as TestToolStripItem;
    if (ttsmi == null) return;

    base.Add(ttsmi);
    ttsmi.MouseEnter += this.OnMouseEnter;
    ttsmi.MouseLeave += this.OnMouseLeave;
  }
}
