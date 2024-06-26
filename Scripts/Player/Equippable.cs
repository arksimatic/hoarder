using Godot;
using Hoarder.Scripts.Player;
using System;

namespace Hoarder.Scripts
{
    public partial class Equippable : Node2D
	{
		[Signal]
		public delegate void BreakItemTickEventHandler();

		private Sprite2D _childSprite2D;
		private CharacterBody2D _parent;
		private AnimationPlayer _animationPlayer;
		private AnimationTree _animationTree;
		private AnimationNodeStateMachinePlayback _stateMachine;

		private Boolean _isRightDirection;
		private Single _imageShiftX = 7f;
		private Single _imageShiftY = -4f;
		private Single _buffer => _imageShiftX + 1f;
		private Boolean _isMining = false;
		private Single _miningTime = 0;

		private Single _itemRadius = 20f;
		public override void _Ready()
		{
			_childSprite2D = (Sprite2D)GetNode("Sprite2D");
			_parent = (CharacterBody2D)GetParent();
			_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
			_animationTree = GetNode<AnimationTree>("AnimationTree");
			_stateMachine = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");

			_isRightDirection = _parent.GlobalPosition.X < GlobalPosition.X;

			PrintItemsPosition();
		}
		public override void _Process(Double delta)
		{
			UpdateMining(delta);
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
			Vector2 destination = HMath.TrimToCircle(GetGlobalMousePosition(), _parent.GlobalPosition, _itemRadius);
			GlobalPosition = VisualShift(destination);
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

		private Vector2 GetAimedGridTilePosition()
		{
            Vector2 pos = HMath.TrimToCircle(GetGlobalMousePosition(), _parent.GlobalPosition, _itemRadius);

            return new Vector2(
				(Single)Math.Floor((pos.X + StaticSettings.GridSize / 2) / StaticSettings.GridSize) * StaticSettings.GridSize,
				(Single)Math.Floor((pos.Y + StaticSettings.GridSize / 2) / StaticSettings.GridSize) * StaticSettings.GridSize
			);
		}

		private async void PrintItemsPosition()
		{
			while (true)
			{
				await ToSignal(GetTree().CreateTimer(5), "timeout");
                GD.Print(Name + " parent: " + GetParent<Node2D>().GlobalPosition);
                GD.Print(Name + " position: " + GlobalPosition);
				GD.Print(Name + " weird point position: " + HMath.TrimToCircle(GetGlobalMousePosition(), _parent.GlobalPosition, _itemRadius));
				GD.Print(Name + " snappoint: " + GetAimedGridTilePosition());
			}
		}

		private Single _timeToMine = 0f;
		public void Mine()
		{
			if(_timeToMine <= 0)
			{

				_timeToMine = 1f;
				MineTimerDown();
			}
		}

		private async void MineTimerDown()
		{
			await ToSignal(GetTree().CreateTimer(1), "timeout");
			if(_timeToMine > 0)
				_timeToMine -= 1;
		}

		private void UpdateMining(Double delta)
		{
			Single miningUnitTime = 1f;

			if (Input.IsActionPressed(KeyCode.Action))
			{
				_isMining = true;
				_miningTime += Convert.ToSingle(delta);
				if (_miningTime >= miningUnitTime)
				{
					_miningTime -= miningUnitTime;
					EmitSignal("BreakItemTick", GetAimedGridTilePosition(), 100);
				}
			}
			else
			{
				_miningTime = 0;
				_isMining = false;
			}
		}

		private Vector2 VisualShift(Vector2 original)
		{
            Vector2 shift = _isRightDirection ? new Vector2(-_imageShiftX, _imageShiftY) : new Vector2(_imageShiftX, _imageShiftY);
			return new Vector2(original.X + shift.X, original.Y + shift.Y);
        }
    }
}
