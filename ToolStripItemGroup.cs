using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace TNT.ToolStripItemManager
{
	/// <summary>
	/// Manages a group of <see cref="ToolStripItem"/>
	/// </summary>
	public abstract class ToolStripItemGroup : List<ToolStripItem>
	{
		/// <summary>
		/// <see cref="System.Windows.Forms.ToolStripStatusLabel"/> used to display the tool tip hint
		/// </summary>
		public ToolStripStatusLabel ToolStripStatusLabel { get; internal set; }

		/// <summary>
		/// Group manager used to access other <see cref="ToolStripItemGroup"/>
		/// </summary>
		public ToolStripItemGroupManager ToolStripItemGroupManager { get; internal set; }

		/// <summary>
		/// Mouse click event handler
		/// </summary>
		public EventHandler OnMouseClick { get; internal set; }

		/// <summary>
		/// <see cref="Image"/> used by all <see cref="ToolStripItem"/>
		/// </summary>
		public Image Image { get; internal set; }

		/// <summary>
		/// Text used by all <see cref="ToolStripItem"/>
		/// </summary>
		public abstract string Text { get; }

		/// <summary>
		/// ToolTipText used by all <see cref="ToolStripItem"/>
		/// </summary>
		public abstract string ToolTipText { get; }

		/// <summary>
		/// Indicates that all <see cref="ToolStripItem"/> should act as a check box or not
		/// </summary>
		public virtual bool CheckOnClick => false;

		/// <summary>
		/// Gets or sets a value indicating whether the group of <see cref="ToolStripItem"/> are checked or not checked
		/// </summary>
		public virtual bool Checked
		{
			get
			{
				if (base.Count > 0)
				{
					return base[0].GetChecked();
				}

				return false;
			}
			set
			{
				base.ForEach(t =>
				{
					ToolStripMenuItem toolStripMenuItem = t as ToolStripMenuItem;
					ToolStripButton toolStripButton = t as ToolStripButton;

					if (toolStripMenuItem != null)
					{
						toolStripMenuItem.CheckedChanged -= CheckedChanged;
						toolStripMenuItem.Checked = value;
						toolStripMenuItem.CheckedChanged += CheckedChanged;
					}
					else if (toolStripButton != null)
					{
						toolStripButton.CheckedChanged -= CheckedChanged;
						toolStripButton.Checked = value;
						toolStripButton.CheckedChanged += CheckedChanged;
					}
				});

			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the group of <see cref="ToolStripItem"/> are visible or not visible
		/// </summary>
		public bool Visible
		{
			get { return base[0].Visible; }
			set { base.ForEach(t => t.Visible = value); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the group of <see cref="ToolStripItem"/> are enabled or not enabled
		/// </summary>
		public bool Enabled
		{
			get { return base[0].Enabled; }
			set { base.ForEach(t => t.Enabled = value); }
		}

		/// <summary>
		/// Constructs a <see cref="ToolStripItemGroup"/>
		/// </summary>
		/// <param name="image">Image to set on the <see cref="ToolStripItem"/> in the group</param>
		public ToolStripItemGroup(Image image = null)
		{
			this.Image = image;
		}

		/// <summary>
		/// Implement to enable/disable <see cref="ToolStripItem"/>
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event args</param>
		public virtual void OnApplicationIdle(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// Adds a <see cref="ToolStripItem"/> to the <see cref="ToolStripItemGroup"/>
		/// </summary>
		/// <typeparam name="T">Type of <see cref="ToolStripItem"/></typeparam>
		/// <param name="toolStripItem"><see cref="ToolStripItem"/> to add</param>
		public void Add<T>(T toolStripItem) where T : ToolStripItem
		{
			if (toolStripItem is ToolStripButton)
			{
				ToolStripButton toolStripButton = toolStripItem as ToolStripButton;
				toolStripButton.CheckOnClick = this.CheckOnClick;
				toolStripButton.Checked = this.Checked;
				toolStripButton.CheckedChanged += CheckedChanged;
				toolStripItem.Click += this.MouseClick;
			}
			else if (toolStripItem is ToolStripMenuItem)
			{
				ToolStripMenuItem toolStripMenuItem = toolStripItem as ToolStripMenuItem;
				toolStripMenuItem.CheckOnClick = this.CheckOnClick;
				toolStripMenuItem.Checked = this.Checked;
				toolStripMenuItem.CheckedChanged += CheckedChanged;
				toolStripItem.Click += this.MouseClick;
			}
			else
			{
				if (toolStripItem is ToolStripSplitButton)
				{
					(toolStripItem as ToolStripSplitButton).ButtonClick += this.MouseClick;
				}
				else
				{
					toolStripItem.Click += this.MouseClick;
				}
			}

			toolStripItem.MouseEnter += this.MouseEnter;
			toolStripItem.MouseLeave += this.MouseLeave;
			toolStripItem.Image = this.Image;
			toolStripItem.Text = this.Text;

			base.Add(toolStripItem);
		}

		/// <summary>
		/// Gets an image associated with the <paramref name="resource"/> value within the calling assembly
		/// </summary>
		/// <param name="resource">Name of resource in the calling assembly</param>
		/// <returns>Image associated with the <paramref name="resource"/> value within the calling assembly</returns>
		public static Image ResourceToImage(string resource)
		{
			var assembly = Assembly.GetCallingAssembly();
			var resourceStream = assembly.GetManifestResourceStream(resource);
			return resourceStream == null ? null : new Bitmap(resourceStream);
		}

		private void MouseEnter(object sender, EventArgs e)
		{
			if (ToolStripStatusLabel != null)
			{
				ToolStripStatusLabel.Text = ToolTipText;
			}
		}

		private void MouseLeave(object sender, EventArgs e)
		{
			if (ToolStripStatusLabel != null)
			{
				ToolStripStatusLabel.Text = string.Empty;
			}
		}

		/// <summary>
		/// Calls the <see cref="OnMouseClick"/> event if assigned
		/// </summary>
		/// <param name="sender">Object that was clicked</param>
		/// <param name="e">Information about the event</param>
		public virtual void MouseClick(object sender, EventArgs e)
		{
			this.OnMouseClick?.Invoke(sender, e);
		}

		private void CheckedChanged(object sender, EventArgs e)
		{
			this.Checked = (sender as ToolStripItem).GetChecked();
		}
	}
}
