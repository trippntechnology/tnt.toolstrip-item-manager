using TNT.ToolStripItemManager.Extension;

namespace TNT.ToolStripItemManager;

/// <summary>
/// Represents a group of <see cref="ToolStripItem"/> controls that behave as radio buttons,
/// ensuring only one item is checked at a time within the group.
/// </summary>
/// <remarks>
/// This class extends <see cref="ToolStripItemGroup"/> to provide radio button behavior, automatically managing
/// mutual exclusivity of checked states. When an item is checked, all other items in the group are unchecked.
/// The <see cref="CheckOnClick"/> property is always set to <c>true</c> for this group.
/// </remarks>
/// <param name="text">The text to display for all <see cref="ToolStripItem"/> controls in the group.</param>
/// <param name="toolTipText">The tooltip text to display for all <see cref="ToolStripItem"/> controls in the group. Optional.</param>
public class ToolStripItemRadioGroup(string text, string? toolTipText = null) : ToolStripItemGroup(text, toolTipText, true)
{
    private bool? _previouslyChecked;

    /// <summary>
    /// Gets or sets the action invoked when the checked state of an item in the radio group changes.
    /// </summary>
    /// <remarks>
    /// The action receives the <see cref="ToolStripItemGroup"/> instance and a boolean value indicating whether an item is now checked.
    /// This event is only triggered when the checked state actually changes (not on redundant state changes).
    /// </remarks>
    public Action<ToolStripItemGroup, bool> OnCheckedChanged = (toolStripItemGroup, isChecked) => { };

    /// <summary>
    /// Handles the <see cref="ToolStripItem.CheckedChanged"/> event for items in the radio group.
    /// </summary>
    /// <remarks>
    /// This method prevents duplicate checked changed events by tracking the previously checked state.
    /// It only invokes <see cref="OnCheckedChanged"/> when the checked state actually changes.
    /// </remarks>
    /// <param name="sender">The <see cref="ToolStripItem"/> that triggered the event.</param>
    /// <param name="e">The event data.</param>
    public override void CheckedChanged(object? sender, EventArgs e)
    {
        var toolStripItem = sender as ToolStripItem;
        var isChecked = toolStripItem?.GetChecked();
        if (isChecked == null) return;

        if (_previouslyChecked != isChecked!)
        {
            _previouslyChecked = isChecked;
            base.CheckedChanged(sender, e);
            OnCheckedChanged(this, isChecked!.Value);
        }
    }
}
