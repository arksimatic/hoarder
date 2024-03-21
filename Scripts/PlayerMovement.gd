extends CharacterBody2D

var MAX_SPEED = 500
var ACCELERATION = 1000
var FRICTION = 1000
var _animationPlayer

func _ready():
	_animationPlayer = get_node("AnimatedSprite2D")

func _physics_process(delta):
	update_movement(delta)

func get_input_axis():
	_animationPlayer.play("new_animation")
	var left = int(Input.is_action_pressed("move_left"))
	var right = int(Input.is_action_pressed("move_right"))
	var up = int(Input.is_action_pressed("move_up"))
	var down = int(Input.is_action_pressed("move_down"))
	return Vector2(right - left, down - up)

func update_movement(delta):
	var axis = get_input_axis()

	if axis.x == 0 and axis.y == 0:
		slide(delta)
	else:
		move(delta, axis)

	move_and_slide()

func slide(delta):
	var friction_delta = FRICTION * delta
	var x_slow_down = velocity.normalized().x * friction_delta
	var y_slow_down = velocity.normalized().y * friction_delta
	if velocity.length() > friction_delta:
		velocity = Vector2(velocity.x - x_slow_down, velocity.y - y_slow_down)
	else:
		velocity = Vector2.ZERO

func move(delta, axis):
	var axis_acceleration_delta = Vector2(axis.x * ACCELERATION * delta, axis.y * ACCELERATION * delta)
	velocity += axis_acceleration_delta
	velocity = velocity.limit_length(MAX_SPEED)
