using Godot;
using System;

public partial class MapItem : Node2D
{
	public virtual Int32 Health { get; set; }
	public virtual void OnDamaged(Int32 damage) { }
}
