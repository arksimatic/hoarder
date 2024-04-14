using Godot;
using System;
using System.Collections;

public partial class Coal : Node2D
{
	public Int32 Health = 100;
	private CustomSignals _customSignals;
	public override void _Ready()
	{
		//_customSignals = GetNode<CustomSignals>("/root/Scripts/CustomSignals");
		//_customSignals.DamageObject += OnDamaged;
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
