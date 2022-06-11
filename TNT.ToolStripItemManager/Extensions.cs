
namespace TNT.ToolStripItemManager;

/// <summary>
/// Extension methods
/// </summary>
public static class Extensions
{
	/// <summary>
	/// Sets the Checked property value if it exists.
	/// </summary>
	/// <param name="toolStripItem">Object with Checked property</param>
	/// <param name="isChecked">Value to set Checked property</param>
	public static void SetChecked(this ToolStripItem toolStripItem, bool isChecked)
	{
		if (toolStripItem is ToolStripMenuItem toolStripMenuItem)
		{
			toolStripMenuItem.Checked = isChecked;
		}
		else if (toolStripItem is ToolStripButton toolStripButton)
		{
			toolStripButton.Checked = isChecked;
		}
	}

	/// <summary>
	/// Gets the Checked property value if it exists
	/// </summary>
	/// <param name="toolStripItem">Object with Checked property</param>
	/// <returns>Checked property value if it exists, false otherwise</returns>
	public static bool GetChecked(this ToolStripItem toolStripItem)
	{
		bool isChecked = false;

		if (toolStripItem is ToolStripMenuItem toolStripMenuItem)
		{
			isChecked = toolStripMenuItem.Checked;
		}
		else if (toolStripItem is ToolStripButton toolStripButton)
		{
			isChecked = toolStripButton.Checked;
		}

		return isChecked;
	}
}
