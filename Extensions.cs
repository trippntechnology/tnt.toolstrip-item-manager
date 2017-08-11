using System.Windows.Forms;

namespace TNT.ToolStripItemManager
{
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
			ToolStripMenuItem toolStripMenuItem = toolStripItem as ToolStripMenuItem;
			ToolStripButton toolStripButton = toolStripItem as ToolStripButton;

			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Checked = isChecked;
			}
			else if (toolStripButton != null)
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

			ToolStripMenuItem toolStripMenuItem = toolStripItem as ToolStripMenuItem;
			ToolStripButton toolStripButton = toolStripItem as ToolStripButton;

			if (toolStripMenuItem != null)
			{
				isChecked = toolStripMenuItem.Checked;
			}
			else if (toolStripButton != null)
			{
				isChecked = toolStripButton.Checked;
			}

			return isChecked;
		}
	}
}
