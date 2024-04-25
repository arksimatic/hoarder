using Godot;
using Hoarder.Scripts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

public partial class MapObjectsManager : Node2D
{
	private List<MapObject> _items = new List<MapObject>();
	public override void _Ready()
	{
		foreach(var item in GetChildren())
		{
			if (item is MapObject)
			{
				_items.Add(item as MapObject);
			}
		}
		
		PrintItemsPosition();
	}

	private async void PrintItemsPosition()
	{
		while (true)
		{
			await ToSignal(GetTree().CreateTimer(5), "timeout");

			foreach (var item in _items)
			{
				GD.Print(item.Name + " position: " + item.GlobalPosition);
			}
		}
	}

	private void EQ_BreakItemTick(Vector2 equippablePosition, Int32 damage)
	{
		MapObject item = _items.Where(item => item.Position.X == equippablePosition.X && item.Position.Y == equippablePosition.Y).FirstOrDefault();
		if(item != null)
		{
			item.OnDamaged(damage);
			if(item.Health <= 0)
			{
				item.QueueFree();
				_items.Remove(item);
			}
		}
	}
}
