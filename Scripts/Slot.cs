using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarder.Scripts
{
    public partial class Slot : Panel
    {
        PackedScene ItemClass = (PackedScene)ResourceLoader.Load("res://Scenes//Item.tscn");
        Node item;
        public override void _Ready()
        {
             item = ItemClass.Instantiate();
            AddChild(item);
            Node2D child = GetNode<Node2D>("Item");
            child.Position = new Vector2(5, 5);
        }
    }
}
