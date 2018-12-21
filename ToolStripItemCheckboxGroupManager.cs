using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TNT.ToolStripItemManager
{
	/// <summary>
	/// Manages a group of <see cref="ToolStripItemGroup"/> that are meant to work like radio buttons (only one checked at a time).
	/// </summary>
	public class ToolStripItemCheckboxGroupManager : ToolStripItemGroupManager
	{
		/// <summary>	
		/// Initializes a <see cref="ToolStripItemCheckboxGroupManager"/> that manages a <see cref="Dictionary{TKey, TValue}"/> of <see cref="ToolStripItemGroup"/>
		/// </summary>
		/// <param name="statusLabel">Provide for tool tip hints</param>
		public ToolStripItemCheckboxGroupManager(ToolStripStatusLabel statusLabel)
			: base(statusLabel)
		{

		}

		/// <summary>
		/// Creates a new <see cref="ToolStripItemGroup"/> which is also managed as like a radio button with other <see cref="ToolStripItemGroup"/>
		/// </summary>
		/// <typeparam name="T"><see cref="ToolStripItemGroup"/> type</typeparam>
		/// <param name="items"><see cref="ToolStripItem"/> array that should be added to the <see cref="ToolStripItemGroup"/></param>
		/// <param name="image"><see cref="Image"/> that should be used</param>
		/// <param name="externalObject">External object that this <see cref="ToolStripItemGroup"/> needs access</param>
		/// <param name="onClick">Event that handles a mouse click</param>
		/// <returns>Newly create object <typeparamref name="T"/></returns>
		public override T Create<T>(ToolStripItem[] items, Image image = null, object externalObject = null, EventHandler onClick = null)
		{
			T itemGroup = base.Create<T>(items, image, externalObject, onClick);
			itemGroup.OnMouseClick += this.MouseClick;
			return itemGroup;
		}

		/// <summary>
		/// Managers the <see cref="ToolStripItemGroup"/> items as a radio button group
		/// </summary>
		/// <param name="sender"><see cref="ToolStripItem"/> that was clicked</param>
		/// <param name="e">Not used</param>
		protected void MouseClick(object sender, EventArgs e)
		{
			var toolStripItem = sender as ToolStripItem;

			if (toolStripItem.GetChecked())
			{
				return;
			}

			if (TryGetValue(toolStripItem.Text, out ToolStripItemGroup toolStripItemGroup))
			{
				foreach (var item in this.Values)
				{
					item.Checked = item == toolStripItemGroup;
				}
			}
		}
	}
}
