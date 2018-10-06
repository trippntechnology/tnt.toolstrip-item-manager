using System;

namespace TNT.ToolStripItemManager.Tests.Helpers
{
	public class ApplicationIdleToolStripItemGroup : ToolStripItemGroup
	{
		public bool ApplicationIdleCalled { get; set; } = false;

		public override string Text => "Application Idle ToolStripItemGroup Text";

		public override string ToolTipText => "Application Idle ToolStripItemGroup ToolTipText";

		public override void OnApplicationIdle(object sender, EventArgs e)
		{
			ApplicationIdleCalled = true;
			base.OnApplicationIdle(sender, e);
		}

		public class Group1 : ApplicationIdleToolStripItemGroup
		{
			public override string Text => "Group 1";
		}

		public class Group2 : ApplicationIdleToolStripItemGroup
		{
			public override string Text => "Group 2";
		}

		public class Group3 : ApplicationIdleToolStripItemGroup
		{
			public override string Text => "Group 3";
		}
	}
}
