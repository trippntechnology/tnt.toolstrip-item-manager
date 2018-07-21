using TNT.ToolStripItemManager;

namespace Test.Groups
{
	public class Three : ToolStripItemGroup
	{
		public Three()
			: base(ResourceToImage("Test.Images.shape_align_left.png"))
		{
		}

		public override string Text => "Three";

		public override string ToolTipText => "Tool tip three";

		public override bool CheckOnClick => true;
	}
}
