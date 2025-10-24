using TNT.ToolStripItemManager;
using TNT.ToolStripItemManager.Extension;

namespace Example.Groups;

internal class License() : ToolStripItemGroup("License", "Select to check if licensed", true, "Example.Images.license_24dp.png".ToImage());