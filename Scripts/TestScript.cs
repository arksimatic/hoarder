using Godot;
using System;

public partial class TestScript : Sprite2D
{
    public void _on_timer_timeout()
    {
        this.Visible = false;
        GD.Print("Timer timeout");
    }
}
