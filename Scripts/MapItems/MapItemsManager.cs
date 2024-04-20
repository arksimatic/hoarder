using Godot;
using Hoarder.Scripts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

public partial class MapItemsManager : Node2D
{
	private List<MapItem> _items = new List<MapItem>();
	public override void _Ready()
	{
		foreach(var item in GetChildren())
		{
			if (item is MapItem)
			{
				_items.Add(item as MapItem);
			}
		}
		
		PrintItemsPosition();
	}

	public override void _Process(double delta)
	{
		
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

	private void EQ_Test()
	{
		//GD.Print("EQ_Test");
	}

	private void EQ_BreakItemTick(Vector2 equippablePosition, Int32 damage)
	{
		MapItem item = _items.Where(item => item.Position.X == equippablePosition.X && item.Position.Y == equippablePosition.Y).FirstOrDefault();
		if(item != null)
		{
			item.OnDamaged(damage);
			if(item.Health <= 0)
			{
				item.QueueFree();
				_items.Remove(item);
			}
		}
		GD.Print("BREAK ITEM TICK");
	}
}
