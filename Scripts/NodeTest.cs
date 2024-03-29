using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarder.Scripts
{
	public partial class NodeTest : Node2D
	{
		public override void _Process(Double delta)
		{
			//GD.Print("NodeTest Global Position: " + GlobalPosition);
			//GD.Print("Equippable Global Position: " + GetNode<Node2D>("Equippable").GlobalPosition);

			//Node parentNode = GetParent();
			//if (parentNode != null)
			//{
			//	GD.Print(Name + " is child of: " + parentNode.Name);
			//	GD.Print(Name + " global position: " + GlobalPosition);
			//}
			//else
			//{
			//	GD.Print("No parent");
			//}
		}
	}
}
