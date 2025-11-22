using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace NUnitTests;

[TestFixture]
[ExcludeFromCodeCoverage]
public class ToolStripItemRadioGroupTests
{
    private class TestRadioGroup1() : ToolStripItemRadioGroup("Rest Radio Group 1");
    private class TestRadioGroup2() : ToolStripItemRadioGroup("Rest Radio Group 2");


    private ToolStripButton button1 = null!;
    private ToolStripButton button2 = null!;
    private ToolStrip toolStrip = null!;
    private Form form = null!;

    [SetUp]
    public void SetUp()
    {
        button1 = new ToolStripButton();
        button2 = new ToolStripButton();
        toolStrip = new ToolStrip();
        form = new Form();
        toolStrip.Items.Add(button1);
        toolStrip.Items.Add(button2);
        form.Controls.Add(toolStrip);
        form.Show();
    }

    [TearDown]
    public void TearDown()
    {
        button1?.Dispose();
        button2?.Dispose();
        toolStrip?.Dispose();
        form?.Dispose();
    }

    [Test]
    public void Add_Items_OnlyOneCanBeCheckedAtATime()
    {
        // Arrange
        var manager = new ToolStripItemRadioGroupManager();
        manager.Create<TestRadioGroup1>([button1]);
        manager.Create<TestRadioGroup2>([button2]);

        // Act
        button1.Checked = true;
        //group.CheckedChanged(button1, System.EventArgs.Empty);
        button2.Checked = true;
        //group.CheckedChanged(button2, System.EventArgs.Empty);

        // Assert
        Assert.That(button1.Checked, Is.False, "button1 should be unchecked after button2 is checked");
        Assert.That(button2.Checked, Is.True, "button2 should be checked");
    }

    [Test]
    public void CheckedChanged_InvokesOnCheckedChangedOnlyOnStateChange()
    {
        // Arrange
        var group = new ToolStripItemRadioGroup("RadioGroup");
        group.Add(button1);
        int callCount = 0;
        group.OnCheckedChanged = (_, isChecked) => callCount++;

        // Act
        button1.Checked = true;
        group.CheckedChanged(button1, System.EventArgs.Empty); // should call
        group.CheckedChanged(button1, System.EventArgs.Empty); // should NOT call again
        button1.Checked = false;
        group.CheckedChanged(button1, System.EventArgs.Empty); // should call

        // Assert
        Assert.That(callCount, Is.EqualTo(2));
    }

    [Test]
    public void CheckOnClick_IsAlwaysTrue()
    {
        // Arrange
        var group = new ToolStripItemRadioGroup("RadioGroup");
        var menuItem = new ToolStripMenuItem();
        // Act
        group.Add(button1);
        group.Add(menuItem);
        // Assert
        Assert.That(group.CheckOnClick, Is.True);
        Assert.That(button1.CheckOnClick, Is.True);
        Assert.That(menuItem.CheckOnClick, Is.True);
    }

    /// <summary>
    /// NOTE: This test really doens't test any functionality, but was added for test coverage
    /// </summary>
    [Test]
    public void CheckedChanged_SenderIsNull_DoesNothing()
    {
        var group = new ToolStripItemRadioGroup("Test");
        Assert.DoesNotThrow(() => group.CheckedChanged(null, EventArgs.Empty));
    }

    [Test]
    public void Manager_WhenGroupIsCreatedByManager_SetToManagerInstance()
    {
        // Arrange
        var manager = new ToolStripItemRadioGroupManager();
        var button = new ToolStripButton();

        // Act
        var group = manager.Create<TestRadioGroup1>([button]);

        // Assert
        Assert.That(group.Manager, Is.SameAs(manager), "Group's Manager should reference the creating manager instance");
    }
}
