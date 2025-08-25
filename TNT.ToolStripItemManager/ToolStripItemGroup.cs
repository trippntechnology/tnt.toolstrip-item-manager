using System.Reflection;

namespace TNT.ToolStripItemManager;

/// <summary>
/// Represents a group of <see cref="ToolStripItem"/> controls that can be managed collectively.
/// </summary>
/// <remarks>
/// Provides common functionality for managing a set of <see cref="ToolStripItem"/> instances, such as enabling/disabling, visibility, checked state, and licensing.
/// </remarks>
/// <param name="image">The image to set on each <see cref="ToolStripItem"/> in the group.</param>
public abstract class ToolStripItemGroup(Image? image = null) : List<ToolStripItem>
{
  // Fields

  /// <summary>
  /// Occurs when a <see cref="ToolStripItem"/> in the group is clicked.
  /// </summary>
  internal EventHandler? MouseClicked { get; set; }

  // Properties

  /// <summary>
  /// Gets or sets a value indicating whether the group of <see cref="ToolStripItem"/> controls are checked.
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
        if (t is ToolStripMenuItem toolStripMenuItem)
        {
          toolStripMenuItem.CheckState = value ? CheckState.Checked : CheckState.Unchecked;
        }
        else if (t is ToolStripButton toolStripButton)
        {
          toolStripButton.CheckState = value ? CheckState.Checked : CheckState.Unchecked;
        }
      });
    }
  }

  /// <summary>
  /// Gets a value indicating whether all <see cref="ToolStripItem"/> controls in the group should act as check boxes.
  /// </summary>
  public virtual bool CheckOnClick => false;

  /// <summary>
  /// Gets or sets a value indicating whether all <see cref="ToolStripItem"/> controls in the group are enabled.
  /// </summary>
  public bool Enabled
  {
    get
    {
      var isEnabled = true;
      base.ForEach(i => isEnabled = (i.Enabled && isEnabled));
      return isEnabled;
    }
    set { base.ForEach(t => t.Enabled = value); }
  }

  /// <summary>
  /// Gets or sets the <see cref="Image"/> used by all <see cref="ToolStripItem"/> controls in the group.
  /// </summary>
  public Image? Image { get; internal set; } = image;

  /// <summary>
  /// Gets or sets a delegate that determines whether the actions managed by this group are licensed.
  /// </summary>
  /// <remarks>
  /// The delegate receives a boolean indicating the context (e.g., click or check) and the group itself.
  /// </remarks>
  public virtual Func<bool, ToolStripItemGroup, bool>? IsLicensed { get; set; }

  /// <summary>
  /// Gets or sets an object that contains data about the <see cref="ToolStripItemGroup"/>.
  /// </summary>
  public virtual object? Tag { get; set; }

  /// <summary>
  /// Gets the text used by all <see cref="ToolStripItem"/> controls in the group.
  /// </summary>
  public abstract string Text { get; }

  /// <summary>
  /// Gets or sets the <see cref="ToolStripItemGroupManager"/> used to access other <see cref="ToolStripItemGroup"/> instances.
  /// </summary>
  public ToolStripItemGroupManager? ToolStripItemGroupManager { get; internal set; }

  /// <summary>
  /// Gets or sets the <see cref="System.Windows.Forms.ToolStripStatusLabel"/> used to display the tool tip hint.
  /// </summary>
  public ToolStripStatusLabel? ToolStripStatusLabel { get; internal set; }

  /// <summary>
  /// Gets the tool tip text used by all <see cref="ToolStripItem"/> controls in the group.
  /// </summary>
  public abstract string ToolTipText { get; }

  /// <summary>
  /// Gets or sets the action invoked when a <see cref="ToolStripItem"/> in the group is clicked. This can be used to perform custom logic when any <see cref="ToolStripItem"/> in the group is clicked.
  /// </summary>
  public Action<ToolStripItemGroup> OnClick = item => { };

  /// <summary>
  /// Gets or sets a value indicating whether all <see cref="ToolStripItem"/> controls in the group are visible.
  /// </summary>
  public bool Visible
  {
    get
    {
      var isVisible = true;
      base.ForEach(i => isVisible = (i.Available && isVisible));
      return isVisible;
    }
    set { base.ForEach(t => t.Visible = value); }
  }

  // Methods

  /// <summary>
  /// Adds a <see cref="ToolStripItem"/> to the <see cref="ToolStripItemGroup"/> and configures its properties and event handlers.
  /// </summary>
  /// <typeparam name="T">The type of <see cref="ToolStripItem"/> to add.</typeparam>
  /// <param name="toolStripItem">The <see cref="ToolStripItem"/> to add.</param>
  public virtual void Add<T>(T toolStripItem) where T : ToolStripItem
  {
    if (toolStripItem is ToolStripButton toolStripButton)
    {
      toolStripButton.CheckOnClick = this.CheckOnClick;
      toolStripButton.Checked = this.Checked;
      toolStripButton.CheckedChanged += OnCheckedChanged;
      toolStripItem.Click += this.OnToolStripItemClick;
    }
    else if (toolStripItem is ToolStripMenuItem toolStripMenuItem)
    {
      toolStripMenuItem.CheckOnClick = this.CheckOnClick;
      toolStripMenuItem.Checked = this.Checked;
      toolStripMenuItem.CheckedChanged += OnCheckedChanged;
      toolStripItem.Click += this.OnToolStripItemClick;
    }
    else if (toolStripItem is ToolStripSplitButton toolStripSplitButton)
    {
      // Attach the OnClick event handler to the ButtonClick. Click fires when the drop down arrow is also clicked.
      toolStripSplitButton.ButtonClick += this.OnToolStripItemClick;
    }
    else
    {
      toolStripItem.Click += this.OnToolStripItemClick;
    }

    toolStripItem.MouseEnter += this.OnMouseEnter;
    toolStripItem.MouseLeave += this.OnMouseLeave;
    toolStripItem.Image = this.Image;
    toolStripItem.Text = this.Text;
    toolStripItem.ToolTipText = this.ToolTipText;

    base.Add(toolStripItem);
  }

  /// <summary>
  /// Handles the <see cref="OnCheckedChanged"/> event for a <see cref="ToolStripItem"/> in the group.
  /// </summary>
  /// <param name="sender">The <see cref="object"/> that triggered the event.</param>
  /// <param name="e">The event data.</param>
  public virtual void OnCheckedChanged(object? sender, EventArgs e)
  {
    if (IsLicensed?.Invoke(false, this) == true)
    {
      this.Checked = (sender as ToolStripItem)?.GetChecked() ?? false;
    }
  }

  /// <summary>
  /// Handles the mouse click event for a <see cref="ToolStripItem"/> in the group, invoking <see cref="OnMouseClick"/> and the <see cref="MouseClicked"/> event if licensed.
  /// </summary>
  /// <param name="sender">The object that was clicked.</param>
  /// <param name="e">The event data.</param>
  private void OnToolStripItemClick(object? sender, EventArgs e)
  {
    if (IsLicensed?.Invoke(true, this) == true)
    {
      this.MouseClicked?.Invoke(sender, e);
      OnClick(this);
    }
    else
    {
      (sender as ToolStripItem)?.SetChecked(false);
    }
  }

  /// <summary>
  /// Handles the <see cref="OnMouseEnter"/> event for a <see cref="ToolStripItem"/> in the group, updating the status label with the tool tip text.
  /// </summary>
  /// <param name="sender">The <see cref="object"/> that triggered the event.</param>
  /// <param name="e">The event data.</param>
  protected virtual void OnMouseEnter(object? sender, EventArgs e)
  {
    if (ToolStripStatusLabel != null)
    {
      ToolStripStatusLabel.Text = ToolTipText;
    }
  }

  /// <summary>
  /// Handles the <see cref="OnMouseLeave"/> event for a <see cref="ToolStripItem"/> in the group, clearing the status label.
  /// </summary>
  /// <param name="sender">The <see cref="object"/> that triggered the event.</param>
  /// <param name="e">The event data.</param>
  protected virtual void OnMouseLeave(object? sender, EventArgs e)
  {
    if (ToolStripStatusLabel != null)
    {
      ToolStripStatusLabel.Text = string.Empty;
    }
  }

  /// <summary>
  /// Called by <see cref="Application.Idle"/> to enable or disable <see cref="ToolStripItem"/> controls in the group as needed.
  /// </summary>
  /// <param name="sender">The sender of the event.</param>
  /// <param name="e">The event data.</param>
  public virtual void OnApplicationIdle(object? sender, EventArgs e) { }

  /// <summary>
  /// Called when the license state changes, allowing the group to update its state accordingly.
  /// </summary>
  /// <param name="isLicensed">Indicates whether the application is licensed.</param>
  public virtual void OnLicenseChanged(bool isLicensed) { }

  /// <summary>
  /// Gets an <see cref="Image"/> associated with the specified resource name from the calling assembly.
  /// </summary>
  /// <param name="resource">The name of the resource in the calling assembly.</param>
  /// <returns>The <see cref="Image"/> associated with the specified resource, or <c>null</c> if not found.</returns>
  public static Image? ResourceToImage(string resource)
  {
    var assembly = Assembly.GetCallingAssembly();
    var resourceStream = assembly.GetManifestResourceStream(resource);
    return resourceStream == null ? null : new Bitmap(resourceStream);
  }
}
