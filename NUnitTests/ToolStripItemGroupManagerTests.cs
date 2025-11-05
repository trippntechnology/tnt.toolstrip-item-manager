using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace NUnitTests;

[ExcludeFromCodeCoverage]
internal class ToolStripItemGroupManagerTests
{
    private class TestToolStripItemGroup : ToolStripItemGroup
    {
        public TestToolStripItemGroup() : base("TestGroup") { }
    }

    [Test]
    public void Create_AddsGroupAndItemsToManager()
    {
        // Arrange
        var manager = new ToolStripItemGroupManager();
        var button = new ToolStripButton();
        var menuItem = new ToolStripMenuItem();
        // Act
        var group = manager.Create<TestToolStripItemGroup>(new ToolStripItem[] { button, menuItem });
        // Assert
        Assert.That(manager.Contains(group), Is.True);
        Assert.That(group.Contains(button), Is.True);
        Assert.That(group.Contains(menuItem), Is.True);
    }

    [Test]
    public void Create_PropagatesOnClickAndOnToolTipChange()
    {
        // Arrange
        var manager = new ToolStripItemGroupManager();
        bool clickCalled = false;
        bool tooltipCalled = false;
        manager.OnClick = _ => clickCalled = true;
        manager.OnToolTipChange = _ => tooltipCalled = true;
        var group = manager.Create<TestToolStripItemGroup>(new ToolStripItem[] { new ToolStripButton() });
        // Act
        group.OnClick(group);
        group.OnToolTipChange("");
        // Assert
        Assert.That(clickCalled, Is.True);
        Assert.That(tooltipCalled, Is.True);
    }

    [Test]
    public void ApplicationIdle_InvokesOnIdleForEachGroup()
    {
        // Arrange
        var manager = new ToolStripItemGroupManager();
        var group1 = manager.Create<TestToolStripItemGroup>(new ToolStripItem[] { new ToolStripButton() });
        var group2 = manager.Create<TestToolStripItemGroup>(new ToolStripItem[] { new ToolStripButton() });
        var called = new List<object?>();
        manager.OnIdle = (sender, e) => called.Add(sender);
        // Act
        // Simulate Application.Idle event
        typeof(ToolStripItemGroupManager)
            .GetMethod("Application_Idle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(manager, new object?[] { null, EventArgs.Empty });
        // Assert
        Assert.That(called, Does.Contain(group1));
        Assert.That(called, Does.Contain(group2));
    }
}
