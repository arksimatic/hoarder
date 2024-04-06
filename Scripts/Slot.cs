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
        private PackedScene ItemClass = (PackedScene)ResourceLoader.Load("res://Scenes//Item.tscn");
        private Node2D _item;
        private GridContainer _inventoryGrid;

        public override void _Ready()
        {
            _inventoryGrid = GetNode<GridContainer>("Inventory");
            _item = ItemClass.Instantiate() as Node2D;
            AddChild(_item);
            Node2D child = GetNode<Node2D>("Item");
            child.Position = new Vector2(5, 5);
        }

        private void PickFromSlot()
        {
            RemoveChild(_item);
            _inventoryGrid.AddChild(_item);
            _item = null;
        }

        private void PutIntoSlot(Node2D newItem)
        {
            _item = newItem;
            _item.Position = new Vector2(0, 0);
            _inventoryGrid.RemoveChild(_item);
            AddChild(_item);
        }
    }
}
