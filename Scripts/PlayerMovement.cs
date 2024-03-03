using Godot;
using System;
using System.Net;

public partial class PlayerMovement : CharacterBody2D
{
    public Single MAX_SPEED = 500;
    public Single ACCELERATION = 1000;
    public Single FRICTION = 1000;

    public override void _PhysicsProcess(Double delta)
    {
        Move(delta);
    }

    public Vector2 GetInputAxis()
    {
        Int32 left = Convert.ToInt32(Input.IsActionPressed("move_left"));
        Int32 right = Convert.ToInt32(Input.IsActionPressed("move_right"));
        Int32 up = Convert.ToInt32(Input.IsActionPressed("move_up"));
        Int32 down = Convert.ToInt32(Input.IsActionPressed("move_down"));

        return new Vector2(right - left, down - up);
    }

    public void Move(Double delta)
    {
        Vector2 axis = GetInputAxis();
        if(axis.X == 0 && axis.Y == 0)
        {
            Single frictionDelta = FRICTION * Convert.ToSingle(delta);
            Single xSlowDown = Velocity.Normalized().X * frictionDelta;
            Single ySlowDown = Velocity.Normalized().Y * frictionDelta;
            if (Velocity.Length() > frictionDelta)
                Velocity = new Vector2(Velocity.X - xSlowDown, Velocity.Y - ySlowDown);
            else
                Velocity = Vector2.Zero;
        }
        else
        {
            Vector2 axisAccelerationDelta = new Vector2(axis.X * ACCELERATION * Convert.ToSingle(delta), axis.Y * ACCELERATION * Convert.ToSingle(delta));
            Velocity += axisAccelerationDelta;
            Velocity = Velocity.LimitLength(MAX_SPEED);
        }

        MoveAndSlide();
    }
}
