using Godot;
using System;
using System.Collections;

public partial class Coal : MapItem
{
	public override Int32 Health { get; set; } = 100;
	private CustomSignals _customSignals;
	public override void _Ready()
	{
		//_customSignals = GetNode<CustomSignals>("/root/Scripts/CustomSignals");
		//_customSignals.DamageObject += OnDamaged;
	}
	public override void _Process(double delta)
	{
		
	}
	public override void OnDamaged(Int32 damage)
	{
		Health -= damage;
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
