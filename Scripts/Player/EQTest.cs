using Godot;

namespace Hoarder.Scripts
{
	public partial class EQTest : Node2D
	{
        private CharacterBody2D _parent;

		public override void _Ready()
		{
            _parent = (CharacterBody2D)GetParent();
        }

		public override void _Process(double delta)
		{
            GlobalPosition = HMath.TrimToCircle(GetGlobalMousePosition(), _parent.GlobalPosition, 20);
		}
    }
}
