using Godot;
using System;

namespace Hoarder.Scripts
{
    public partial class Equippable : Sprite2D
    {
        public override void _Process(Double delta)
        {
            SwapEquippable();
        }

        public void SwapEquippable()
        {
            if (Input.IsKeyPressed(Key.Q))
            {
                var resource = ResourceLoader.Load("res://Graphics/Equippable/weapon.png");
                var texture = (Texture2D)resource;
                this.Texture = texture;
            }
            if (Input.IsKeyPressed(Key.E))
            {
                var resource = ResourceLoader.Load("res://Graphics/Equippable/pickaxe.png");
                var texture = (Texture2D)resource;
                this.Texture = texture;
            }
        }
    }
}
