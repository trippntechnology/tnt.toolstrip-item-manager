using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TNT.ToolStripItemManager
{
	/// <summary>
	/// Manages a group of <see cref="ToolStripItemGroup"/> that are meant to work like radio buttons (only one checked at a time).
	/// </summary>
	public class ToolStripItemCheckboxGroupManager : ToolStripItemGroupManager
	{
		/// <summary>
		/// Indicates the home group
		/// </summary>
		public ToolStripItemGroup HomeGroup { get; protected set; } = null;

		/// <summary>
		/// Previously checked <see cref="ToolStripItemGroup"/> that was not the <see cref="HomeGroup"/>
		/// </summary>
		public ToolStripItemGroup PreviouslyCheckedGroup { get; protected set; } = null;

		/// <summary>	
		/// Initializes a <see cref="ToolStripItemCheckboxGroupManager"/> that manages a <see cref="Dictionary{TKey, TValue}"/> of <see cref="ToolStripItemGroup"/>
		/// </summary>
		/// <param name="statusLabel">Provide for tool tip hints</param>
		public ToolStripItemCheckboxGroupManager(ToolStripStatusLabel statusLabel)
			: base(statusLabel)
		{
		}

		/// <summary>
		/// Creates a new <see cref="ToolStripItemGroup"/> which is also managed as a radio button with other <see cref="ToolStripItemGroup"/>
		/// </summary>
		/// <typeparam name="T"><see cref="ToolStripItemGroup"/> type</typeparam>
		/// <param name="items"><see cref="ToolStripItem"/> array that should be added to the <see cref="ToolStripItemGroup"/></param>
		/// <param name="image"><see cref="Image"/> that should be used</param>
		/// <param name="externalObject">External object that this <see cref="ToolStripItemGroup"/> needs access</param>
		/// <returns>Newly create object <typeparamref name="T"/></returns>
		public override T Create<T>(ToolStripItem[] items, Image image = null, object externalObject = null)
		{
			T itemGroup = base.Create<T>(items, image, externalObject);
			itemGroup.MouseClicked += this.MouseClick;
			return itemGroup;
		}

		/// <summary>
		/// Creates a new <see cref="ToolStripItemGroup"/> which is also managed as a radio button with other <see cref="ToolStripItemGroup"/>. The 
		/// <typeparamref name="T"/> created will be considered the home item.
		/// </summary>
		/// <typeparam name="T"><see cref="ToolStripItemGroup"/> type</typeparam>
		/// <param name="items"><see cref="ToolStripItem"/> array that should be added to the <see cref="ToolStripItemGroup"/></param>
		/// <param name="image"><see cref="Image"/> that should be used</param>
		/// <param name="externalObject">External object that this <see cref="ToolStripItemGroup"/> needs access</param>
		/// <returns>Newly create object <typeparamref name="T"/></returns>
		public T CreateHome<T>(ToolStripItem[] items, Image image = null, object externalObject = null) where T : ToolStripItemGroup, new()
		{
			T itemGroup = this.Create<T>(items, image, externalObject);
			HomeGroup = itemGroup;
			return itemGroup;
		}

		/// <summary>
		/// Called when the license changes
		/// </summary>
		/// <param name="isLicensed">Indicates if the app is licensed</param>
		public override void LicensedChanged(bool isLicensed)
		{
			if (!isLicensed)
			{
				base.Items.ForEach(i => i.Checked = false);
			}
			base.LicensedChanged(isLicensed);
		}

		/// <summary>
		/// Returns the <see cref="ToolStripItemGroup"/> that is checked if exists
		/// </summary>
		/// <returns><see cref="ToolStripItemGroup"/> that is checked if exists, null otherwise</returns>
		public ToolStripItemGroup GetCheckedGroup()
		{
			return this.Values.FirstOrDefault(i => i.Checked == true);
		}

		/// <summary>
		/// Toggles check between <see cref="HomeGroup"/> and <see cref="PreviouslyCheckedGroup"/>
		/// </summary>
		public void Toggle()
		{
			if (HomeGroup == null || PreviouslyCheckedGroup == null) return;

			if (HomeGroup.Checked == true)
			{
				HomeGroup.Checked = false;
				PreviouslyCheckedGroup.Checked = true;
			}
			else
			{
				PreviouslyCheckedGroup.Checked = false;
				HomeGroup.Checked = true;
			}
		}

		/// <summary>
		/// Manages the <see cref="ToolStripItemGroup"/> items as a radio button group
		/// </summary>
		/// <param name="sender"><see cref="ToolStripItem"/> that was clicked</param>
		/// <param name="e">Not used</param>
		protected void MouseClick(object sender, EventArgs e)
		{
			var toolStripItem = sender as ToolStripItem;
			var currentlyChecked = GetCheckedGroup();

			// Keep track that the item was selected previously if not the HomeGroup
			if (currentlyChecked != HomeGroup) PreviouslyCheckedGroup = currentlyChecked;

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

				if (toolStripItemGroup != HomeGroup) PreviouslyCheckedGroup = toolStripItemGroup;
			}
		}
	}
}
