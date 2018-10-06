using System;
using TNT.ToolStripItemManager.Tests.Helpers;

namespace TNT.ToolStripItemManager.Tests
{
	public static class Extensions
	{
		public static ApplicationIdleToolStripItemGroup Apply(this ApplicationIdleToolStripItemGroup manager, Action<ApplicationIdleToolStripItemGroup> block)
		{
			block(manager);
			return manager;
		}
	}
}
