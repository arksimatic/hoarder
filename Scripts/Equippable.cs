using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarder.Scripts
{
	public partial class Equippable : Node2D
	{
		private Sprite2D _childSprite2D;
		private CharacterBody2D _parent;
		private AnimationPlayer _animationPlayer;
		private AnimationTree _animationTree;
		private AnimationNodeStateMachinePlayback _stateMachine;

		private Boolean _isRightDirection;
		public override void _Ready()
		{
			_childSprite2D = (Sprite2D)GetNode("Sprite2D");
			_parent = (CharacterBody2D)GetParent();
			_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
			_animationTree = GetNode<AnimationTree>("AnimationTree");
			_stateMachine = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");

			_isRightDirection = _parent.GlobalPosition.X < GlobalPosition.X;
		}

		public Vector2 GetVector2()
		{
			return _isRightDirection ? new Vector2(1, 0) : new Vector2(-1, 0);
		}
		public override void _Process(Double delta)
		{
			Move(delta);
			SwapEquippable();
			FlipEquippable();
			UpdateAnimation();
			//GD.Print(Scale);
		}
		private void UpdateAnimation()
		{
			Boolean isAction = Input.IsActionPressed(KeyCode.Action);

			_animationTree.Set("parameters/Mine/blend_position", GetVector2());

			if (isAction)
			{
				_stateMachine.Travel("Mine");
			}
			else
			{
				_stateMachine.Travel("Idle");
			}
		}
		//private void Move(Double delta)
		//{
		//	Vector2 mouse_position = GetGlobalMousePosition();
			
		//	Vector2 destination = TrimToPlayerCircle(mouse_position, _parent.GlobalPosition);

		//	Vector2 direction = destination - GlobalPosition;

		//	// This if prevents from smuggy/laggy effect of the sprite. It was probably caused by rapid movement of the sprite
		//	if (direction.Length() > 1)
		//	{
		//		Single speed = direction.Length() * 25_000;
		//		Vector2 directionDelta = direction.Normalized() * Convert.ToInt32(delta * speed);
		//		Vector2 newVelocity = new Vector2((Single)Math.Round(directionDelta.X), (Single)Math.Round(directionDelta.Y));

		//		Velocity = newVelocity;
		//		MoveAndSlide();
		//	}
		//}

		private void Move(Double delta)
		{
			Vector2 mouse_position = GetGlobalMousePosition();
			Vector2 destination = TrimToPlayerCircle(mouse_position, _parent.GlobalPosition);
			//GD.Print(mouse_position);
			//GD.Print(_parent.GlobalPosition);
			//GD.Print("Dest " + destination);
			GlobalPosition = GlobalPosition.MoveToward(destination, Convert.ToSingle(delta * 25_000));
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
			if (_parent.GlobalPosition.X < GlobalPosition.X && !_isRightDirection)
			{
				//GD.Print("Swap to right");
				_isRightDirection = true;
				Scale = new Vector2(1, 1);
				//_childSprite2D.Scale = new Vector2(1, 1);
				return;
			}

			if(_parent.GlobalPosition.X > GlobalPosition.X && _isRightDirection)
			{
				//GD.Print("Swap to left");
				_isRightDirection = false;
				Scale = new Vector2(-1, -1);
				//_childSprite2D.Scale = new Vector2(-1, -1);
				return;
			}
		}
	}
}
