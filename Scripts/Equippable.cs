using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarder.Scripts
{
	public partial class Equippable : CharacterBody2D
	{
		private Sprite2D _childSprite2D;
		private CharacterBody2D _parent;
		private AnimationPlayer _animationPlayer;
		public override void _Ready()
		{
			_childSprite2D = (Sprite2D)GetNode("Sprite2D");
			_parent = (CharacterBody2D)GetParent();
			_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		}
		public override void _Process(Double delta)
		{
			Move(delta);
			SwapEquippable();
			FlipEquippable();
		}

		private void Move(Double delta)
		{
			Vector2 mouse_position = GetGlobalMousePosition();
			
			Vector2 destination = TrimToPlayerCircle(mouse_position, _parent.GlobalPosition);

			Vector2 direction = destination - GlobalPosition;

			// This if prevents from smuggy/laggy effect of the sprite. It was probably caused by rapid movement of the sprite
			if (direction.Length() > 1)
			{
				Single speed = direction.Length() * 25_000;
				Vector2 directionDelta = direction.Normalized() * Convert.ToInt32(delta * speed);
				Vector2 newVelocity = new Vector2((Single)Math.Round(directionDelta.X), (Single)Math.Round(directionDelta.Y));

				Velocity = newVelocity;
				MoveAndSlide();
			}
		}

		private Vector2 TrimToPlayerCircle(Vector2 mousePosition, Vector2 parentPosition)
		{
			Vector2 destination = mousePosition;
			Vector2 direction = (destination - parentPosition).Normalized();
			Single distance = parentPosition.DistanceTo(destination);
			if (distance > 10)
			{
				destination = parentPosition + direction * 10;
			}
			return destination;
		}

		public void SwapEquippable()
		{
			if (Input.IsKeyPressed(Key.Q))
			{
				var resource = ResourceLoader.Load("res://Graphics/Equippable/weapon.png");
				var texture = (Texture2D)resource;
				_childSprite2D.Texture = texture;
			}
			if (Input.IsKeyPressed(Key.E))
			{
				var resource = ResourceLoader.Load("res://Graphics/Equippable/pickaxe.png");
				var texture = (Texture2D)resource;
				_childSprite2D.Texture = texture;
			}
		}

		public void FlipEquippable()
		{
			if (_parent.GlobalPosition.X < GlobalPosition.X)
				_childSprite2D.Scale = new Vector2(1, 1);
			else
				_childSprite2D.Scale = new Vector2(-1, -1);
		}
	}
}
