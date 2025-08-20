namespace TNT.ToolStripItemManager;

/// <summary>
/// <see cref="Dictionary{TKey, TValue}"/> of <see cref="ToolStripItemGroup"/>
/// </summary>
public class ToolStripItemGroupManager : Dictionary<string, ToolStripItemGroup>
{
	private readonly ToolStripStatusLabel StatusLabel;

	/// <summary>
	/// <see cref="ToolStripItemGroup"/> items represented by this <see cref="ToolStripItemGroupManager"/>
	/// </summary>
	protected List<ToolStripItemGroup> Items { get { return this.Values.ToList(); } }

	/// <summary>
	/// Callback called when <see cref="ToolStripItemGroup.MouseClicked"/> and <see cref="ToolStripItemGroup.CheckedChanged(object, EventArgs)"/>
	/// are called to control what happens
	/// </summary>
	public Func<bool, ToolStripItemGroup, bool> IsLicensed { get; set; } = (allowMessageBox, ToolStripItemGroup) => true;

  /// <summary>
  /// Callback called when a <see cref="ToolStripItem"/> is clicked.
  /// </summary>
  public Action<ToolStripItem> OnItemClicked { get; set; } = item => { };

	/// <summary>	
	/// Initializes a ToolStripItemGroupManager that manages a <see cref="Dictionary{TKey, TValue}"/> of <see cref="ToolStripItemGroup"/>
	/// </summary>
	/// <param name="statusLabel">Provide for tool tip hints</param>
	public ToolStripItemGroupManager(ToolStripStatusLabel statusLabel)
	{
		this.StatusLabel = statusLabel;
		Application.Idle += Application_Idle;
	}

  /// <summary>
  /// Call to notify each <see cref="ToolStripItemGroup"/> that the license changed.
  /// </summary>
  /// <param name="isLicensed">Indicates whether the app is licensed</param>
  public virtual void LicensedChanged(bool isLicensed) => Items.ForEach(i => i.OnLicenseChanged(isLicensed));

	/// <summary>
	/// Called by <see cref="Application.Idle"/> which calls each <see cref="ToolStripItemGroup.OnApplicationIdle(object, EventArgs)"/>
	/// </summary>
	/// <param name="sender">Not used</param>
	/// <param name="e">Not used</param>
	protected void Application_Idle(object? sender, EventArgs e) => Items.ForEach(i => i.OnApplicationIdle(sender, e));

	/// <summary>
	/// Creates a new <see cref="ToolStripItemGroup"/>
	/// </summary>
	/// <typeparam name="T"><see cref="ToolStripItemGroup"/> type</typeparam>
	/// <param name="items"><see cref="ToolStripItem"/> array that should be added to the <see cref="ToolStripItemGroup"/></param>
	/// <param name="image"><see cref="Image"/> that should be used</param>
	/// <param name="externalObject">External object that this <see cref="ToolStripItemGroup"/> needs access</param>
	/// <returns>Newly create object <typeparamref name="T"/></returns>
	public virtual T Create<T>(ToolStripItem[] items, Image? image = null, object? externalObject = null) where T : ToolStripItemGroup, new()
	{
		T t = new T
		{
			ToolStripStatusLabel = this.StatusLabel,
			ToolStripItemGroupManager = this,
			ExternalObject = externalObject,
			IsLicensed = this.IsLicensed,
			OnItemClicked = this.OnItemClicked,
		};

		if (image != null)
		{
			t.Image = image;
		}

		this[t.Text] = t;

		foreach (ToolStripItem item in items)
		{
			t.Add(item);
		}

		return t;
	}
}
