extends CharacterBody2D
@export var tileSizeXAxis: float = 51
@export var tileSizeYAxis: float = 26
var pointX: bool = true
var pointY: bool = false
var playerDirection
var speed = 50
var health = 100
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	#playerDirection = DirectionToRotation($"../Player".global_position, global_position)
	if pointX == true:
		velocity.y = 30
		if global_position.y > 600:
			pointX = false
			pointY = true
	if pointY == true:
		velocity.y = -30	
		if global_position.y < 10:
			pointY = false
			pointX = true
	#velocity = Vector2(cos(playerDirection) * tileSizeXAxis ,sin(playerDirection) * tileSizeYAxis) * delta * speed
	move_and_slide()
	$Label.text = "Health" + str(health)
	if health <= 0:
		global_position = Vector2(0, 0)
		health = 100

func DirectionToRotation(aimPosition: Vector2, globalPosition: Vector2 ):
	var x = (aimPosition - globalPosition).x
	var y = (aimPosition - globalPosition).y
	return atan2(y, x)


func _on_head_area_entered(area):
	health -= 100
	pass # Replace with function body.


func _on_torso_area_entered(area):
	health -= 5
	pass # Replace with function body.
