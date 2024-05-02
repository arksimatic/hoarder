using System;

public partial class CoalMapObject : MapObject
{
	public override Int32 Health { get; set; } = 100;
	public override void _Ready()
	{
		//_customSignals = GetNode<CustomSignals>("/root/Scripts/CustomSignals");
		//_customSignals.DamageObject += OnDamaged;
	}

}
