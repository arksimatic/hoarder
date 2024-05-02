using Godot;
using System;

public partial class MapObject : Node2D
{
	public virtual Int32 Health { get; set; }
    public virtual void OnDamaged(Int32 damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            QueueFree();
        }
    }
}
