using System;

public partial class CoalMapObject : MapObject
{
	public override Int32 Health { get; set; } = 100;
	public override void _Ready()
	{
		//_customSignals = GetNode<CustomSignals>("/root/Scripts/CustomSignals");
		//_customSignals.DamageObject += OnDamaged;
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
