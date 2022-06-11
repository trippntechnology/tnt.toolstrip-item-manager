using System.Diagnostics.CodeAnalysis;
using UnitTests.Helpers;

namespace UnitTests;

[ExcludeFromCodeCoverage]
public static class Extensions
{
	public static ApplicationIdleToolStripItemGroup Apply(this ApplicationIdleToolStripItemGroup manager, Action<ApplicationIdleToolStripItemGroup> block)
	{
		block(manager);
		return manager;
	}
}
