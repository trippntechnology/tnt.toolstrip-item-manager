using TNT.ToolStripItemManager;

namespace Test.Groups
{
	public class Four : ToolStripItemGroup
	{
		public Four()
			: base(ResourceToImage("Test.Images.shape_align_middle.png"))
		{

		}

		public override string Text => "Four";

		public override string ToolTipText => "Tool tip four";

		public override bool CheckOnClick => true;
	}
}
