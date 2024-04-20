using Godot;
using Hoarder.Scripts.Player;
using System;

public partial class PlayerMovement : CharacterBody2D
{
	public Single MAX_SPEED = 500;
	public Single ACCELERATION = 1000;
	public Single FRICTION = 1000;
	private AnimationPlayer _animationPlayer;
	private AnimationTree _animationTree;
	private AnimationNodeStateMachinePlayback _stateMachine;

	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_animationTree = GetNode<AnimationTree>("AnimationTree");
		_stateMachine = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
	}
	public override void _Process(Double delta)
	{
		UpdateAnimation();
		UpdateMovement(delta);
	}
	public override void _PhysicsProcess(Double delta)
	{
	}
	public Vector2 GetInputVector()
	{
		Vector2 vector2 = Vector2.Zero;
		if (Input.IsActionPressed(KeyCode.Right))
			vector2 = new Vector2(vector2.X + 1, vector2.Y);
		if (Input.IsActionPressed(KeyCode.Left))
			vector2 = new Vector2(vector2.X - 1, vector2.Y);
		if (Input.IsActionPressed(KeyCode.Down))
			vector2 = new Vector2(vector2.X, vector2.Y + 1);
		if (Input.IsActionPressed(KeyCode.Up))
			vector2 = new Vector2(vector2.X, vector2.Y - 1);

		return vector2.Normalized();
	}
	private void UpdateAnimation()
	{
		// check whether mouse button is pressed
		//Boolean isAction = Input.IsActionPressed(KeyCode.Action);

		// setting vectors for the animation tree
		Vector2 inputVector = GetInputVector();
		if (inputVector != Vector2.Zero)
		{
			_animationTree.Set("parameters/Move/blend_position", inputVector);
			_animationTree.Set("parameters/Idle/blend_position", inputVector);
			//_animationTree.Set("parameters/Action/blend_position", inputVector);
		}

		//if (isAction)
		//{
		//	_stateMachine.Travel("Action");
		//}
		if (inputVector != Vector2.Zero)
		{
			_stateMachine.Travel("Move");
		}
		else
		{
			_stateMachine.Travel("Idle");
		}
	}
	public void UpdateMovement(Double delta)
	{
		Vector2 newVelocity = GetInputVector();
		if (newVelocity != Vector2.Zero)
		{
			Velocity = newVelocity.Normalized() * Convert.ToInt32(delta * 25_000);
			MoveAndSlide();
		}
	}
}
