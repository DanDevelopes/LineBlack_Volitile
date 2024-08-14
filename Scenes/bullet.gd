extends Area2D

#region Variable
var rangeSet: int
var damageSet: int
var rotationSet: float
var speedSet: int
var startPostion: Vector2
var lastGlobalPosition: Vector2
var hitEntity: bool
var hitWall: bool
var mousePositionSet: Vector2
#endregion
# Called when the node enters the scene tree for the first time.

func _ready():
	$".".monitorable = false
	$".".monitoring = false
	pass # Replace with function body.

func _process(delta):
 # X pi sin math here, pi sin math here
	global_position = lastGlobalPosition
	global_position += Vector2(cos(rotation) ,sin(rotation)) * int(speedSet * delta)
	lastGlobalPosition = global_position

	if GetDistance(global_position) > GetDistance(mousePositionSet):
		$".".monitorable = true
		$".".monitoring = true 
	if sqrt(abs(global_position.x**2 - startPostion.x** 2) + abs(global_position.y**2 - startPostion.y**2)) > rangeSet:
		print("freed", self)
		free()
	if hitEntity:
		free()
	if hitWall:
		free() 

func _on_area_entered(area):
	print("hit enemy: " + str(area))
	hitEntity = true

func _on_has_hit_wall_area_entered(area):
	print("hit wall: " + str(area))
	hitWall = true
	pass # Replace with function body.

func Construct(mousePosition, damage: int, setRange: int, setPosition, setRotation, speed = 100):
	rangeSet = setRange
	damageSet = damage
	global_position = setPosition + Vector2(randf_range(0,5),randf_range(0,5))
	startPostion = setPosition
	lastGlobalPosition = startPostion
	rotation = setRotation
	speedSet = speed
	mousePositionSet = mousePosition
	pass

func GetDistance(position: Vector2):
	return sqrt(abs(position.x** 2 - startPostion.x** 2) + abs(position.y** 2 - startPostion.y** 2))
