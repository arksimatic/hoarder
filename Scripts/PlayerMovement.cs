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
        UpdateMovement(delta);
    }

    public Vector2 GetInputAxis()
    {
        Int32 left = Convert.ToInt32(Input.IsActionPressed("move_left"));
        Int32 right = Convert.ToInt32(Input.IsActionPressed("move_right"));
        Int32 up = Convert.ToInt32(Input.IsActionPressed("move_up"));
        Int32 down = Convert.ToInt32(Input.IsActionPressed("move_down"));
        return new Vector2(right - left, down - up);
    }

    public void UpdateMovement(Double delta)
    {
        Vector2 axis = GetInputAxis();
        Single deltaSingle = Convert.ToSingle(delta);

        if (axis.X == 0 && axis.Y == 0)
            Slide(deltaSingle);
        else
            Move(deltaSingle, axis);

        MoveAndSlide();
    }

    public void Slide(Single deltaSingle)
    {
        Single frictionDelta = FRICTION * deltaSingle;
        Single xSlowDown = Velocity.Normalized().X * frictionDelta;
        Single ySlowDown = Velocity.Normalized().Y * frictionDelta;
        if (Velocity.Length() > frictionDelta)
            Velocity = new Vector2(Velocity.X - xSlowDown, Velocity.Y - ySlowDown);
        else
            Velocity = Vector2.Zero;
    }

    public void Move(Single deltaSingle, Vector2 axis)
    {
        Vector2 axisAccelerationDelta = new Vector2(axis.X * ACCELERATION * deltaSingle, axis.Y * ACCELERATION * deltaSingle);
        Velocity += axisAccelerationDelta;
        Velocity = Velocity.LimitLength(MAX_SPEED);
    }
}
