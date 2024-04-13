using Godot;
using System;
using System.Collections;

public partial class Coal : Node2D
{
	public Int32 Health = 100;
	public override void _Ready()
	{

	}
	public override void _Process(double delta)
	{
		
	}
	public void OnDamaged()
	{
		Health -= 40;
		if (Health <= 0)
		{
			DestroyCoal();
		}
	}
	public void DestroyCoal()
	{
		QueueFree();
	}
}
