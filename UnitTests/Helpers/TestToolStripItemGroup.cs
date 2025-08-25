using System.Diagnostics.CodeAnalysis;
using TNT.ToolStripItemManager;

namespace UnitTests.Helpers;

[ExcludeFromCodeCoverage]
class TestToolStripItemGroup : ToolStripItemGroup
{
	public TestToolStripItemGroup(Image? image = null)
		: base(image)
	{

	}

	public TestToolStripItemGroup() : base() { }

	public override string Text => "Test";

	public override string ToolTipText => "Tool Tip Test";
}
