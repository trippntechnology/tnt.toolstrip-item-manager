using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace NUnitTests;

[ExcludeFromCodeCoverage]
internal class ToolStripItemGroupTests
{
    /// <summary>
    /// Concrete implementation of ToolStripItemGroup for testing purposes.
    /// Exposes protected methods for unit testing.
    /// </summary>
    private class TestToolStripItemGroup : ToolStripItemGroup
    {
        public TestToolStripItemGroup(string text, string? toolTipText = null, bool checkOnClick = false, Image? image = null)
          : base(text, toolTipText, checkOnClick, image)
        {
        }

        /// <summary>
        /// Public wrapper for testing the protected OnMouseEnter method.
        /// </summary>
        public void TestOnMouseEnter(object? sender, EventArgs e) => OnMouseEnter(sender, e);

        /// <summary>
        /// Public wrapper for testing the protected OnMouseLeave method.
        /// </summary>
        public void TestOnMouseLeave(object? sender, EventArgs e) => OnMouseLeave(sender, e);

        /// <summary>
        /// Public wrapper for testing the private OnToolStripItemClick method.
        /// </summary>
        public void TestOnToolStripItemClick(object? sender, EventArgs e)
        {
            // Use reflection to invoke the private method
            var method = typeof(ToolStripItemGroup).GetMethod("OnToolStripItemClick",
                  System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method?.Invoke(this, new object?[] { sender, e });
        }
    }

    [Test]
    public void OnMouseEnter_WhenMouseEntersItem_InvokesOnToolTipChangeWithGroupTooltipText()
    {
        // Arrange
        string tooltipText = "Test Tooltip";
        var group = new TestToolStripItemGroup("Test", tooltipText);
        var capturedTooltip = string.Empty;
        group.OnToolTipChange = (text) => capturedTooltip = text;

        var toolStripButton = new ToolStripButton();
        group.Add(toolStripButton);

        // Act
        group.TestOnMouseEnter(toolStripButton, EventArgs.Empty);

        // Assert
        Assert.That(capturedTooltip, Is.EqualTo(tooltipText));
    }

    [Test]
    public void OnMouseLeave_WhenMouseLeavesItem_InvokesOnToolTipChangeWithEmptyString()
    {
        // Arrange
        var group = new TestToolStripItemGroup("Test", "Tooltip Text");
        var capturedTooltip = "not empty";
        group.OnToolTipChange = (text) => capturedTooltip = text;

        var toolStripButton = new ToolStripButton();
        group.Add(toolStripButton);

        // Act
        group.TestOnMouseLeave(toolStripButton, EventArgs.Empty);

        // Assert
        Assert.That(capturedTooltip, Is.Empty);
    }

    [Test]
    public void MouseEnterThenLeave_ProperlyTogglesToolTipText()
    {
        // Arrange
        string tooltipText = "Integration Test Tooltip";
        var group = new TestToolStripItemGroup("Test", tooltipText);
        var capturedTooltips = new List<string>();
        group.OnToolTipChange = (text) => capturedTooltips.Add(text);

        var toolStripButton = new ToolStripButton();
        group.Add(toolStripButton);

        // Act
        group.TestOnMouseEnter(toolStripButton, EventArgs.Empty);
        group.TestOnMouseLeave(toolStripButton, EventArgs.Empty);

        // Assert
        Assert.That(capturedTooltips.Count, Is.EqualTo(2));
        Assert.That(capturedTooltips[0], Is.EqualTo(tooltipText));
        Assert.That(capturedTooltips[1], Is.Empty);
    }

    [Test]
    public void MultipleEnterLeaveSequence_MaintainsCorrectTooltipState()
    {
        // Arrange
        string tooltipText = "Sequence Tooltip";
        var group = new TestToolStripItemGroup("Test", tooltipText);
        var capturedTooltips = new List<string>();
        group.OnToolTipChange = (text) => capturedTooltips.Add(text);

        var button1 = new ToolStripButton();
        var button2 = new ToolStripButton();
        group.Add(button1);
        group.Add(button2);

        // Act
        group.TestOnMouseEnter(button1, EventArgs.Empty);
        group.TestOnMouseLeave(button1, EventArgs.Empty);
        group.TestOnMouseEnter(button2, EventArgs.Empty);
        group.TestOnMouseLeave(button2, EventArgs.Empty);

        // Assert
        Assert.That(capturedTooltips.Count, Is.EqualTo(4));
        Assert.That(capturedTooltips[0], Is.EqualTo(tooltipText));
        Assert.That(capturedTooltips[1], Is.Empty);
        Assert.That(capturedTooltips[2], Is.EqualTo(tooltipText));
        Assert.That(capturedTooltips[3], Is.Empty);
    }

    [Test]
    public void OnMouseEnter_WithDifferentToolStripItemTypes_WorksCorrectly()
    {
        // Arrange
        string tooltipText = "Multi-Type Tooltip";
        var group = new TestToolStripItemGroup("Test", tooltipText);
        var capturedTooltips = new List<string>();
        group.OnToolTipChange = (text) => capturedTooltips.Add(text);

        var button = new ToolStripButton();
        var menuItem = new ToolStripMenuItem();
        var label = new ToolStripLabel();
        var textBox = new ToolStripTextBox();

        group.Add(button);
        group.Add(menuItem);
        group.Add(label);
        group.Add(textBox);

        // Act
        group.TestOnMouseEnter(button, EventArgs.Empty);
        group.TestOnMouseEnter(menuItem, EventArgs.Empty);
        group.TestOnMouseEnter(label, EventArgs.Empty);
        group.TestOnMouseEnter(textBox, EventArgs.Empty);

        // Assert
        Assert.That(capturedTooltips.Count, Is.EqualTo(4));
        Assert.That(capturedTooltips, Is.All.EqualTo(tooltipText));
    }

    [Test]
    public void OnMouseLeave_WithDifferentToolStripItemTypes_WorksCorrectly()
    {
        // Arrange
        var group = new TestToolStripItemGroup("Test", "Tooltip");
        var capturedTooltips = new List<string>();
        group.OnToolTipChange = (text) => capturedTooltips.Add(text);

        var button = new ToolStripButton();
        var menuItem = new ToolStripMenuItem();
        var label = new ToolStripLabel();
        var textBox = new ToolStripTextBox();

        group.Add(button);
        group.Add(menuItem);
        group.Add(label);
        group.Add(textBox);

        // Act
        group.TestOnMouseLeave(button, EventArgs.Empty);
        group.TestOnMouseLeave(menuItem, EventArgs.Empty);
        group.TestOnMouseLeave(label, EventArgs.Empty);
        group.TestOnMouseLeave(textBox, EventArgs.Empty);

        // Assert
        Assert.That(capturedTooltips.Count, Is.EqualTo(4));
        Assert.That(capturedTooltips, Is.All.Empty);
    }

    [Test]
    public void OnToolStripItemClick_WhenItemIsClicked_InvokesOnClickAction()
    {
        // Arrange
        var group = new TestToolStripItemGroup("Test Button", "Click me");
        var clickedGroups = new List<ToolStripItemGroup>();
        group.OnClick = (clickedGroup) => clickedGroups.Add(clickedGroup);

        var toolStripButton = new ToolStripButton();
        group.Add(toolStripButton);

        // Act
        group.TestOnToolStripItemClick(toolStripButton, EventArgs.Empty);

        // Assert
        Assert.That(clickedGroups.Count, Is.EqualTo(1));
        Assert.That(clickedGroups[0], Is.SameAs(group));
    }

    [Test]
    public void CheckedChanged_WhenButtonIsChecked_UpdatesGroupCheckedState()
    {
        // Arrange
        var group = new TestToolStripItemGroup("Test", checkOnClick: true);
        var button = new ToolStripButton();
        group.Add(button);

        // Verify initial state
        Assert.That(group.Checked, Is.False);
        Assert.That(button.Checked, Is.False);

        // Act - simulate button being checked
        button.Checked = true;
        group.CheckedChanged(button, EventArgs.Empty);

        // Assert - group's checked state should match button's checked state
        Assert.That(group.Checked, Is.True);
    }
}
