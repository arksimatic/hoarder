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
		private Single _imageShiftX = 7f;
		private Single _imageShiftY = -4f;
		private Single _buffer => _imageShiftX + 1f;
		public override void _Ready()
		{
			_childSprite2D = (Sprite2D)GetNode("Sprite2D");
			_parent = (CharacterBody2D)GetParent();
			_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
			_animationTree = GetNode<AnimationTree>("AnimationTree");
			_stateMachine = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");

			_isRightDirection = _parent.GlobalPosition.X < GlobalPosition.X;
		}
		public override void _Process(Double delta)
		{
			Move(delta);
			SwapEquippable();
			FlipEquippable();
			UpdateAnimation();
		}
		private void UpdateAnimation()
		{
			Boolean isAction = Input.IsActionPressed(KeyCode.Action);

			if (isAction)
			{
				_stateMachine.Travel("Mine");
			}
			else
			{
				_stateMachine.Travel("Idle");
			}
		}

		private void Move(Double delta)
		{
			Vector2 mouse_position = GetGlobalMousePosition();
			Vector2 destination = TrimToPlayerCircle(mouse_position, _parent.GlobalPosition);
			GlobalPosition = GlobalPosition.MoveToward(destination, Convert.ToSingle(delta * 25_000));
		}

		private Vector2 TrimToPlayerCircle(Vector2 mousePosition, Vector2 parentPosition)
		{
			Vector2 shift = _isRightDirection ? new Vector2(-_imageShiftX, _imageShiftY) : new Vector2(_imageShiftX, _imageShiftY);
			Vector2 destination = mousePosition + shift;
			Vector2 direction = (destination - parentPosition).Normalized();
			Single distance = parentPosition.DistanceTo(destination);
			if (distance > 20)
			{
				destination = parentPosition + direction * 20;
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
			if (_parent.GlobalPosition.X < GlobalPosition.X - _buffer && !_isRightDirection)
			{
				_isRightDirection = true;
				Scale = new Vector2(1, 1);
				return;
			}

			if(_parent.GlobalPosition.X > GlobalPosition.X + _buffer && _isRightDirection)
			{
				_isRightDirection = false;
				Scale = new Vector2(-1, 1);
				return;
			}
		}
	}
}
