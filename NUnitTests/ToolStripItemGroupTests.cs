using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using TNT.ToolStripItemManager;

namespace NUnitTests;

[ExcludeFromCodeCoverage]
internal class ToolStripItemGroupTests
{
    private ToolStripButton button1 = null!;
    private ToolStripButton button2 = null!;
    private ToolStripMenuItem menuItem1 = null!;
    private ToolStripLabel label = null!;
    private ToolStripTextBox textBox = null!;
    private ToolStrip toolStrip = null!;
    private MenuStrip menuStrip = null!;
    private Form form = null!;

    [SetUp]
    public void SetUp()
    {
        button1 = new ToolStripButton("Button 1");
        button2 = new ToolStripButton("Button 2");
        menuItem1 = new ToolStripMenuItem("Menu Item 1");
        label = new ToolStripLabel("Label");
        textBox = new ToolStripTextBox();
        toolStrip = new ToolStrip();
        menuStrip = new MenuStrip();
        form = new Form();
        toolStrip.Items.Add(button1);
        toolStrip.Items.Add(button2);
        menuStrip.Items.Add(menuItem1);
        form.Controls.Add(toolStrip);
        form.Controls.Add(menuStrip);
        form.Show();
    }

    [TearDown]
    public void TearDown()
    {
        button1?.Dispose();
        button2?.Dispose();
        menuItem1?.Dispose();
        label?.Dispose();
        textBox?.Dispose();
        toolStrip?.Dispose();
        menuStrip?.Dispose();
        form?.Dispose();
    }

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
        group.Add(button1);
        // Act
        group.TestOnMouseEnter(button1, EventArgs.Empty);
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
        group.Add(button1);
        // Act
        group.TestOnMouseLeave(button1, EventArgs.Empty);
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
        group.Add(button1);
        // Act
        group.TestOnMouseEnter(button1, EventArgs.Empty);
        group.TestOnMouseLeave(button1, EventArgs.Empty);
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
        group.Add(button1);
        group.Add(menuItem1);
        group.Add(label);
        group.Add(textBox);
        // Act
        group.TestOnMouseEnter(button1, EventArgs.Empty);
        group.TestOnMouseEnter(menuItem1, EventArgs.Empty);
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
        group.Add(button1);
        group.Add(menuItem1);
        group.Add(label);
        group.Add(textBox);
        // Act
        group.TestOnMouseLeave(button1, EventArgs.Empty);
        group.TestOnMouseLeave(menuItem1, EventArgs.Empty);
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
        group.Add(button1);
        // Act
        group.TestOnToolStripItemClick(button1, EventArgs.Empty);
        // Assert
        Assert.That(clickedGroups.Count, Is.EqualTo(1));
        Assert.That(clickedGroups[0], Is.SameAs(group));
    }

    [Test]
    public void CheckedChanged_WhenButtonIsChecked_UpdatesGroupCheckedState()
    {
        // Arrange
        var group = new TestToolStripItemGroup("Test", checkOnClick: true);
        group.Add(button1);
        // Verify initial state
        Assert.That(group.Checked, Is.False);
        Assert.That(button1.Checked, Is.False);
        // Act - simulate button being checked
        button1.Checked = true;
        group.CheckedChanged(button1, EventArgs.Empty);
        // Assert - group's checked state should match button's checked state
        Assert.That(group.Checked, Is.True);
    }

    [Test]
    public void Checked_WhenSetToTrue_ChecksAllItemsInGroup()
    {
        // Arrange
        var group = new TestToolStripItemGroup("Test", checkOnClick: true);
        group.Add(button1);
        group.Add(button2);
        group.Add(menuItem1);
        Assert.That(group.Checked, Is.False);
        // Verify initial state
        Assert.That(group.Checked, Is.False);
        Assert.That(button1.Checked, Is.False);
        Assert.That(button2.Checked, Is.False);
        Assert.That(menuItem1.Checked, Is.False);
        // Act
        group.Checked = true;
        // Assert - all items in the group should be checked
        Assert.That(group.Checked, Is.True);
        Assert.That(button1.Checked, Is.True);
        Assert.That(button2.Checked, Is.True);
        Assert.That(menuItem1.Checked, Is.True);
    }

    [Test]
    public void Enabled_WhenSetToFalse_DisablesAllItemsInGroup()
    {
        // Arrange
        var group = new TestToolStripItemGroup("Test");
        group.Add(button1);
        group.Add(button2);
        group.Add(menuItem1);
        group.Add(label);
        // Verify initial state - all items should be enabled by default
        Assert.That(group.Enabled, Is.True);
        Assert.That(button1.Enabled, Is.True);
        Assert.That(button2.Enabled, Is.True);
        Assert.That(menuItem1.Enabled, Is.True);
        Assert.That(label.Enabled, Is.True);
        // Act - disable all items in the group
        group.Enabled = false;
        // Assert - all items should be disabled
        Assert.That(group.Enabled, Is.False);
        Assert.That(button1.Enabled, Is.False);
        Assert.That(button2.Enabled, Is.False);
        Assert.That(menuItem1.Enabled, Is.False);
        Assert.That(label.Enabled, Is.False);
    }

    [Test]
    public void Tag_WhenSetToVariousObjectTypes_StoresAndRetrievesCorrectly()
    {
        // Arrange
        var group = new TestToolStripItemGroup("Test");
        // Act & Assert - test with null
        group.Tag = null;
        Assert.That(group.Tag, Is.Null);
        // Act & Assert - test with string
        group.Tag = "test string";
        Assert.That(group.Tag, Is.EqualTo("test string"));
        // Act & Assert - test with integer
        group.Tag = 42;
        Assert.That(group.Tag, Is.EqualTo(42));
        // Act & Assert - test with custom object
        var customObject = new { Id = 1, Name = "Test" };
        group.Tag = customObject;
        Assert.That(group.Tag, Is.SameAs(customObject));
        // Act & Assert - test with complex object
        var list = new List<string> { "item1", "item2" };
        group.Tag = list;
        Assert.That(group.Tag, Is.SameAs(list));
        Assert.That(((List<string>)group.Tag).Count, Is.EqualTo(2));
    }

    [Test, Apartment(ApartmentState.STA)]
    public void Visible_WhenSetToFalse_HidesAllItemsInGroup()
    {
        // Arrange
        var group = new TestToolStripItemGroup("Test");
        Assert.That(button1.Visible, Is.True);
        Assert.That(button2.Visible, Is.True);
        Assert.That(menuItem1.Visible, Is.True);
        group.Add(button1);
        group.Add(button2);
        group.Add(menuItem1);
        // Verify initial state - all items should be visible by default
        Assert.That(group.Visible, Is.True);
        Assert.That(button1.Visible, Is.True);
        Assert.That(button2.Visible, Is.True);
        Assert.That(menuItem1.Visible, Is.True);
        // Act - hide all items in the group
        group.Visible = false;
        // Assert - all items should be hidden
        Assert.That(group.Visible, Is.False);
        Assert.That(button1.Visible, Is.False);
        Assert.That(button2.Visible, Is.False);
        Assert.That(menuItem1.Visible, Is.False);
    }
}
