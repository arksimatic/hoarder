using Godot;
using Hoarder.Scripts;
using System;
using System.Collections.Generic;

public partial class MapItemsManager : Node2D
{
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

		//var equippable = GetParent().GetNode<Node2D>("Player/EQ");
		//equippable.Connect("Signal", this, "OnGetSignal");
		//GD.Print(equippable.Name);
		Equippable equippable = new Equippable();
        	//equippable.Connect("Signal", this, "OnGetSignal");

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

	public void OnGetSignal()
	{
		GD.Print("Signal received");
	}
}
