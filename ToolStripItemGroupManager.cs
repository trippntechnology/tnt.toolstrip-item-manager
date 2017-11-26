using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TNT.ToolStripItemManager
{
	/// <summary>
	/// <see cref="Dictionary{TKey, TValue}"/> of <see cref="ToolStripItemGroup"/>
	/// </summary>
	public class ToolStripItemGroupManager : Dictionary<string, ToolStripItemGroup>
	{
		private ToolStripStatusLabel StatusLabel;

		/// <summary>
		/// Initializes a ToolStripItemGroupManager that manages a <see cref="Dictionary{TKey, TValue}"/> of <see cref="ToolStripItemGroup"/>
		/// </summary>
		/// <param name="statusLabel">Provide for tool tip hints</param>
		public ToolStripItemGroupManager(ToolStripStatusLabel statusLabel)
		{
			this.StatusLabel = statusLabel;
			Application.Idle += Application_Idle;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			foreach (var toolStripItemGroup in this.Values)
			{
				toolStripItemGroup.OnApplicationIdle(sender, e);
			}
		}

		/// <summary>
		/// Creates a new <see cref="ToolStripItemGroup"/>
		/// </summary>
		/// <typeparam name="T"><see cref="ToolStripItemGroup"/> type</typeparam>
		/// <param name="items"><see cref="ToolStripItem"/> array that should be added to the <see cref="ToolStripItemGroup"/></param>
		/// <param name="image"><see cref="Image"/> that should be used</param>
		/// <param name="onClick">Event that handles a mouse click</param>
		/// <returns></returns>
		public T Create<T>(ToolStripItem[] items, Image image = null, EventHandler onClick = null) where T : ToolStripItemGroup, new()
		{
			T t = new T
			{
				ToolStripStatusLabel = this.StatusLabel,
				ToolStripItemGroupManager = this
			};

			if (image != null)
			{
				t.Image = image;
			}

			if (onClick !=null)
			{
				t.OnMouseClick = onClick;
			}

			this[t.Text] = t;

			foreach (ToolStripItem item in items)
			{
				t.Add(item);
			}

			return t;
		}
	}
}
