namespace TNT.ToolStripItemManager;

/// <summary>
/// Manages a group of <see cref="ToolStripItemGroup"/> instances that function as mutually exclusive radio buttons,
/// ensuring only one group can be checked at a time.
/// </summary>
public class ToolStripItemRadioGroupManager() : ToolStripItemGroupManager()
{
  private ToolStripItemGroup? _activeToolStripItemGroup;

  /// <summary>
  /// Gets or sets the home group, which serves as the default checked group.
  /// </summary>
  public ToolStripItemGroup? HomeGroup { get; protected set; } = null;

  /// <summary>
  /// Gets or sets the previously checked <see cref="ToolStripItemGroup"/> that was not the <see cref="HomeGroup"/>.
  /// Used to track state for toggle operations.
  /// </summary>
  public ToolStripItemGroup? PreviouslyCheckedGroup { get; protected set; } = null;


  /// <summary>
  /// Creates a new <typeparamref name="T"/> instance of <see cref="ToolStripItemRadioGroup"/> with the specified <see cref="ToolStripItem"/> array
  /// and adds it to this manager's group collection.
  /// </summary>
  /// <param name="items">Array of <see cref="ToolStripItem"/> instances to add to the group.</param>
  /// <typeparam name="T">Type of <see cref="ToolStripItemRadioGroup"/> to create and manage.</typeparam>
  /// <returns>The newly created <typeparamref name="T"/> instance. If this is the first group, it will be automatically checked.</returns>
  public new T Create<T>(ToolStripItem[] items) where T : ToolStripItemRadioGroup, new()
  {
    var toolStripItemGroup = base.Create<T>(items);
    toolStripItemGroup.OnCheckedChanged += OnCheckedChanged;
    if (this.Count == 1) toolStripItemGroup.Checked = true;
    return toolStripItemGroup;
  }

  /// <summary>
  /// Handles the checked state change event for <see cref="ToolStripItemGroup"/> instances.
  /// Ensures radio button behavior by unchecking all other groups when one is checked,
  /// and preventing a group from being unchecked if it's the currently active group.
  /// </summary>
  /// <param name="toolStripItemGroup">The <see cref="ToolStripItemGroup"/> whose checked state changed.</param>
  /// <param name="isChecked">True if the group is now checked; otherwise, false.</param>
  private void OnCheckedChanged(ToolStripItemGroup toolStripItemGroup, bool isChecked)
  {
    if (isChecked)
    {
      _activeToolStripItemGroup = toolStripItemGroup;
      this.FindAll(group => group != _activeToolStripItemGroup).ForEach(group => group.Checked = false);
    }
    else if (_activeToolStripItemGroup == toolStripItemGroup)
    {
      toolStripItemGroup.Checked = true;
    }
  }
}
