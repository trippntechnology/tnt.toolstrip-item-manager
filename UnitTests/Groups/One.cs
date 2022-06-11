using System.Diagnostics.CodeAnalysis;
using TNT.ToolStripItemManager;

namespace UnitTests.Groups;

[ExcludeFromCodeCoverage]
public class One : ToolStripItemGroup
{
	public override string Text => "One Text";

	public override string ToolTipText => "One ToolTipText";
}
