using System.Diagnostics.CodeAnalysis;

namespace UnitTests.Helpers;

[ExcludeFromCodeCoverage]
class TestToolStripItem : ToolStripItem
{
	public new virtual event EventHandler MouseEnter;
	public new virtual event EventHandler MouseLeave;

	public void PerformMouseEnter(EventArgs e)
	{
		MouseEnter?.Invoke(null, e);
	}

	public void PerformMouseLeave(EventArgs e)
	{
		MouseLeave?.Invoke(null, e);
	}
}
