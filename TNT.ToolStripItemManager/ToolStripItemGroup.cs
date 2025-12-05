using TNT.ToolStripItemManager.Extension;

namespace TNT.ToolStripItemManager;

/// <summary>
/// Represents a group of <see cref="ToolStripItem"/> controls that can be managed collectively, providing unified management for properties such as text, tooltip, image, checked state, and licensing.
/// </summary>
/// <remarks>
/// Use this class to manage a set of <see cref="ToolStripItem"/> instances together, allowing for consistent configuration and event handling across all items in the group.
/// </remarks>
/// <param name="text">The text to display for all <see cref="ToolStripItem"/> controls in the group.</param>
/// <param name="toolTipText">The tooltip text to display for all <see cref="ToolStripItem"/> controls in the group. Optional.</param>
/// <param name="checkOnClick">A value indicating whether <see cref="ToolStripButton"/> and <see cref="ToolStripMenuItem"/> items in the group should be checked when clicked.</param>
/// <param name="image">The image to set on each <see cref="ToolStripItem"/> in the group. Optional.</param>
public abstract class ToolStripItemGroup(string text, string? toolTipText = null, bool checkOnClick = false, Image? image = null) : List<ToolStripItem>
{
    // Fields

    /// <summary>
    /// Gets the image associated with all <see cref="ToolStripItem"/> controls in the group.
    /// </summary>
    public readonly Image? Image = image;

    /// <summary>
    /// Gets the text displayed for all <see cref="ToolStripItem"/> controls in the group.
    /// </summary>
    public readonly string Text = text;

    /// <summary>
    /// Gets the tooltip text displayed for all <see cref="ToolStripItem"/> controls in the group.
    /// </summary>
    public readonly string ToolTipText = toolTipText ?? String.Empty;

    /// <summary>
    /// Gets a value indicating whether <see cref="ToolStripButton"/> and <see cref="ToolStripMenuItem"/> items in the group should be checked when clicked.
    /// </summary>
    public readonly bool CheckOnClick = checkOnClick;

    // Properties

    /// <summary>
    /// Gets or sets a value indicating whether all <see cref="ToolStripItem"/> controls in the group are checked.
    /// When getting, returns the checked state of the first item in the group. When setting, applies the checked state to all items.
    /// </summary>
    public virtual bool Checked
    {
        get { return (Count > 0) ? base.TrueForAll(item => item.GetChecked()) : false; }
        set { base.ForEach(t => t.SetChecked(value)); }
    }

    /// <summary>
    /// Gets or sets a value indicating whether all <see cref="ToolStripItem"/> controls in the group are enabled.
    /// </summary>
    public bool Enabled
    {
        get { return (Count > 0) ? base.TrueForAll(i => i.Enabled) : false; }
        set { base.ForEach(t => t.Enabled = value); }
    }

    /// <summary>
    /// Gets or sets an object that contains data about the <see cref="ToolStripItemGroup"/>.
    /// </summary>
    public virtual object? Tag { get; set; }

    /// <summary>
    /// Gets or sets the action invoked when a <see cref="ToolStripItem"/> in the group is clicked.
    /// </summary>
    /// <remarks>
    /// The action receives the <see cref="ToolStripItemGroup"/> instance that was clicked.
    /// </remarks>
    public Action<ToolStripItemGroup> OnClick = toolStripItemGroup => { };

    public Action<ToolStripItemGroup, bool> OnCheckChanged = (toolStripItemGroup, isChecked) => { };

    /// <summary>
    /// Gets or sets the action invoked when the mouse enters or leaves a <see cref="ToolStripItem"/> in the group.
    /// </summary>
    /// <remarks>
    /// The action receives the tooltip text to display. An empty string is passed when the mouse leaves.
    /// </remarks>
    public Action<string> OnToolTipChange = toolTipText => { };

    /// <summary>
    /// Gets or sets a value indicating whether all <see cref="ToolStripItem"/> controls in the group are visible.
    /// </summary>
    public bool Visible
    {
        get { return base.TrueForAll(i => i.Visible); }
        set { base.ForEach(t => t.Visible = value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="ToolStripItemGroupManager"/> that manages this group.
    /// </summary>
    /// <remarks>
    /// This property can be used to access the parent manager instance and its shared event handlers or settings.
    /// </remarks>
    public ToolStripItemGroupManager? Manager { get; set; }

    // Methods

    /// <summary>
    /// Adds a <see cref="ToolStripItem"/> to the <see cref="ToolStripItemGroup"/> and configures its properties and event handlers.
    /// </summary>
    /// <remarks>
    /// The item's text, tooltip text, and image are set to match the group's properties. Event handlers for click and mouse events are automatically attached.
    /// For <see cref="ToolStripButton"/> and <see cref="ToolStripMenuItem"/>, the <see cref="ToolStripItemGroup.CheckOnClick"/> property is applied.
    /// </remarks>
    /// <typeparam name="T">The type of <see cref="ToolStripItem"/> to add.</typeparam>
    /// <param name="toolStripItem">The <see cref="ToolStripItem"/> to add.</param>
    public virtual void Add<T>(T toolStripItem) where T : ToolStripItem
    {
        if (toolStripItem is ToolStripButton toolStripButton)
        {
            toolStripButton.CheckOnClick = this.CheckOnClick;
            toolStripButton.CheckedChanged += CheckedChanged;
            toolStripItem.Click += this.OnToolStripItemClick;
        }
        else if (toolStripItem is ToolStripMenuItem toolStripMenuItem)
        {
            toolStripMenuItem.CheckOnClick = this.CheckOnClick;
            toolStripMenuItem.CheckedChanged += CheckedChanged;
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
    /// Handles the <see cref="ToolStripItem.CheckedChanged"/> event for <see cref="ToolStripButton"/> and <see cref="ToolStripMenuItem"/> items in the group.
    /// </summary>
    /// <remarks>
    /// Updates the group's checked state based on the checked state of the item that triggered the event.
    /// </remarks>
    /// <param name="sender">The <see cref="ToolStripItem"/> that triggered the event.</param>
    /// <param name="e">The event data.</param>
    public virtual void CheckedChanged(object? sender, EventArgs e)
    {
        var toolStripItem = (sender as ToolStripItem);

        if (toolStripItem == null) return;

        var currentCheckedState = false;
        var newCheckedState = false;


        if (Count > 1)
        {
            currentCheckedState = TrueForAll(item => item == toolStripItem || item.GetChecked());
            newCheckedState = toolStripItem.GetChecked();

            this.FindAll(item => item != toolStripItem).ForEach(item => item.SetChecked(newCheckedState));
        }
        else
        {
            currentCheckedState = !toolStripItem.GetChecked();
            newCheckedState = toolStripItem.GetChecked();
        }

        TNTLogger.Info($"toolStripItem: {toolStripItem} currentCheckedState: {currentCheckedState} newCheckedState: {newCheckedState}");

        if (currentCheckedState != newCheckedState) OnCheckChanged(this, toolStripItem.GetChecked());
    }

    /// <summary>
    /// Handles the click event for a <see cref="ToolStripItem"/> in the group, invoking the <see cref="OnClick"/> action.
    /// </summary>
    /// <param name="sender">The <see cref="ToolStripItem"/> that was clicked.</param>
    /// <param name="e">The event data.</param>
    protected void OnToolStripItemClick(object? sender, EventArgs e) => OnClick(this);

    /// <summary>
    /// Handles the mouse enter event for a <see cref="ToolStripItem"/> in the group, invoking <see cref="OnToolTipChange"/> with the group's tooltip text.
    /// </summary>
    /// <param name="sender">The <see cref="ToolStripItem"/> that the mouse entered.</param>
    /// <param name="e">The event data.</param>
    protected virtual void OnMouseEnter(object? sender, EventArgs e) => OnToolTipChange(ToolTipText);

    /// <summary>
    /// Handles the mouse leave event for a <see cref="ToolStripItem"/> in the group, invoking <see cref="OnToolTipChange"/> with an empty string.
    /// </summary>
    /// <param name="sender">The <see cref="ToolStripItem"/> that the mouse left.</param>
    /// <param name="e">The event data.</param>
    protected virtual void OnMouseLeave(object? sender, EventArgs e) => OnToolTipChange(String.Empty);
}
