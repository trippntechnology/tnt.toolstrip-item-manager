using TNT.ToolStripItemManager;

namespace Test.Groups
{
	public class Two : ToolStripItemGroup
	{
		public Two()
			:base (ResourceToImage("Test.Images.shape_align_center.png"))
		{

		}

		public override string Text => "Two";

		public override string ToolTipText => "Tool tip two";
	}
}
