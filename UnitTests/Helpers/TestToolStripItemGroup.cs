﻿using System.Diagnostics.CodeAnalysis;
using TNT.ToolStripItemManager;

namespace UnitTests.Helpers;

[ExcludeFromCodeCoverage]
class TestToolStripItemGroup : ToolStripItemGroup
{
	public EventHandler MyMouseClick { get; set; }

	public TestToolStripItemGroup(Image image = null)
		: base(image)
	{

	}

	public TestToolStripItemGroup() : base() { }

	public override string Text => "Test";

	public override string ToolTipText => "Tool Tip Test";

	public override void OnMouseClick(object sender, EventArgs e)
	{
		MyMouseClick?.Invoke(sender, e);
		base.OnMouseClick(sender, e);
	}
}
