using System.Diagnostics.CodeAnalysis;
using TNT.ToolStripItemManager;

namespace UnitTests.Groups;

[ExcludeFromCodeCoverage]
public class Two : ToolStripItemGroup
{
	public override string Text => "Two Text";

	public override string ToolTipText => "Two ToolTipText";
}
