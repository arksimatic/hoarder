using Godot;
using Hoarder.Scripts;
using System;
using System.Collections.Generic;
using System.Dynamic;

public partial class MapItemsManager : Node2D
{
	[Signal]
	public delegate void TestEventHandler();


	private List<Node2D> _items = new List<Node2D>();
	public override void _Ready()
	{
		foreach(var item in GetChildren())
		{
			if (item is Node2D)
			{
				_items.Add(item as Node2D);
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
		GD.Print("EQ_Test");
	}
}
