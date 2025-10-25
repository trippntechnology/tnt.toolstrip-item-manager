using System.Diagnostics.CodeAnalysis;
using TNT.ToolStripItemManager;

namespace UnitTests.Helpers;

[ExcludeFromCodeCoverage]
class TestToolStripItemGroup() : ToolStripItemGroup("Test", "Tool Tip Test");