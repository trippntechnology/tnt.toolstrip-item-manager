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

		//private bool _IsLicensed = false;
		//public bool IsLicensed
		//{
		//	get { return _IsLicensed; }
		//	set
		//	{
		//		_IsLicensed = value;
		//		foreach (var tsig in Values)
		//		{
		//			tsig.IsLicensed = _IsLicensed;
		//		}
		//	}
		//}

		/// <summary>	
		/// Initializes a ToolStripItemGroupManager that manages a <see cref="Dictionary{TKey, TValue}"/> of <see cref="ToolStripItemGroup"/>
		/// </summary>
		/// <param name="statusLabel">Provide for tool tip hints</param>
		public ToolStripItemGroupManager(ToolStripStatusLabel statusLabel) //, bool isLicensed = true)
		{
			this.StatusLabel = statusLabel;
			//this.IsLicensed = isLicensed;
			Application.Idle += Application_Idle;
		}

		/// <summary>
		/// Called by <see cref="Application.Idle"/> which calls each <see cref="ToolStripItemGroup.OnApplicationIdle(object, EventArgs)"/>
		/// </summary>
		/// <param name="sender">Not used</param>
		/// <param name="e">Not used</param>
		protected void Application_Idle(object sender, EventArgs e)
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
		/// <param name="externalObject">External object that this <see cref="ToolStripItemGroup"/> needs access</param>
		/// <param name="onClick">Event that handles a mouse click</param>
		/// <param name="isLicensed"><see cref="Func{T, TResult}"/> that indicates whether the actions managed by the group are licensed</param>
		/// <returns>Newly create object <typeparamref name="T"/></returns>
		public virtual T Create<T>(ToolStripItem[] items, Image image = null, object externalObject = null, EventHandler onClick = null, Func<bool> isLicensed = null) where T : ToolStripItemGroup, new()
		{
			T t = new T
			{
				ToolStripStatusLabel = this.StatusLabel,
				ToolStripItemGroupManager = this,
				ExternalObject = externalObject,
				IsLicensed = isLicensed != null ? isLicensed : () => true
			};

			if (image != null)
			{
				t.Image = image;
			}

			if (onClick != null)
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
