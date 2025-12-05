namespace TNT.ToolStripItemManager;

/// <summary>
/// Manages a collection of <see cref="ToolStripItemGroup"/> instances, providing unified control and event handling across multiple groups.
/// </summary>
/// <remarks>
/// This manager acts as a centralized collection and event dispatcher for toolbar item groups.
/// It inherits from <see cref="List{T}"/> to provide standard collection functionality while adding cross-group event handling.
/// </remarks>
public class ToolStripItemGroupManager : List<ToolStripItemGroup>
{
    /// <summary>
    /// Gets or sets the action invoked when a <see cref="ToolStripItem"/> in any managed group is clicked.
    /// </summary>
    /// <remarks>
    /// The action receives the <see cref="ToolStripItemGroup"/> instance that was clicked.
    /// This action is propagated to all groups created through the <see cref="Create{T}"/> method,
    /// allowing centralized handling of toolbar item clicks across all managed groups.
    /// </remarks>
    public Action<ToolStripItemGroup> OnClick { get; set; } = item => { };

    /// <summary>
    /// Gets or sets the action invoked when the checked state of any managed <see cref="ToolStripItemGroup"/> changes.
    /// </summary>
    /// <remarks>
    /// The action receives the <see cref="ToolStripItemGroup"/> instance and a <see cref="bool"/> indicating the new checked state.
    /// This action is propagated to all groups created through the <see cref="Create{T}"/> method,
    /// allowing centralized handling of checked state changes across all managed groups.
    /// </remarks>
    public Action<ToolStripItemGroup, bool> OnCheckChanged { get; set; } = (toolStripItemGroup, isChecked) => { };

    /// <summary>
    /// Gets or sets the action invoked when the tooltip text of any managed <see cref="ToolStripItem"/> changes.
    /// </summary>
    /// <remarks>
    /// The action receives the new tooltip text as a <see cref="string"/> parameter.
    /// This action is propagated to all groups created through the <see cref="Create{T}"/> method
    /// and can be used to handle tooltip updates for items in all managed groups.
    /// </remarks>
    public Action<string> OnToolTipChange = toolTipText => { };

    /// <summary>
    /// Gets or sets the action invoked when the application becomes idle.
    /// </summary>
    /// <remarks>
    /// This event is raised for each managed group during the application's idle state.
    /// It can be used for periodic updates or deferred operations that should occur when the UI thread is not processing user input.
    /// </remarks>
    public Action<ToolStripItemGroup, EventArgs> OnIdle = (sender, args) => { };

    /// <summary>
    /// Initializes a new instance of the <see cref="ToolStripItemGroupManager"/> class.
    /// </summary>
    /// <remarks>
    /// The constructor registers an event handler for the <see cref="Application.Idle"/> event
    /// to dispatch idle notifications to all managed groups.
    /// </remarks>
    public ToolStripItemGroupManager()
    {
        Application.Idle += Application_Idle;
    }

    /// <summary>
    /// Handles the <see cref="Application.Idle"/> event and propagates it to all managed groups.
    /// </summary>
    /// <param name="sender">The event sender (typically the Application).</param>
    /// <param name="e">The event arguments.</param>
    private void Application_Idle(object? sender, EventArgs e)
    {
        this.ForEach(item => OnIdle(item, e));
    }

    /// <summary>
    /// Creates a new <see cref="ToolStripItemGroup"/> of the specified type with the given <see cref="ToolStripItem"/> controls.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="ToolStripItemGroup"/> to create. Must have a parameterless constructor.</typeparam>
    /// <param name="items">The <see cref="ToolStripItem"/> array that should be added to the new group.</param>
    /// <returns>The newly created <see cref="ToolStripItemGroup"/> instance of type <typeparamref name="T"/>.</returns>
    /// <remarks>
    /// The created group is automatically added to this manager's collection and inherits the following from this manager instance:
    /// <list type="bullet">
    /// <item><description><see cref="OnClick"/> action</description></item>
    /// <item><description><see cref="OnToolTipChange"/> action</description></item>
    /// </list>
    /// </remarks>
    public virtual T Create<T>(ToolStripItem[] items) where T : ToolStripItemGroup, new()
    {
        T t = new T
        {
            Manager = this,
            OnCheckChanged = this.OnCheckChanged,
            OnClick = this.OnClick,
            OnToolTipChange = this.OnToolTipChange,
        };

        Add(t);

        foreach (ToolStripItem item in items)
        {
            t.Add(item);
        }

        return t;
    }
}
