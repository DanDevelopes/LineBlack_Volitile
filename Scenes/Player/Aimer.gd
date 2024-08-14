extends Node2D

var bulletPath = load("res://Scenes/Projectiles/bullet.tscn")
var bullets = 2000
var mag = 25
var bulletsDisplay
var magDisplay
var reloadSpeed: float = 2
var inaccuratePosition
signal giveAmmoAmount(bullets: int, mag: int, reloadPercentage: int)

var level: int
var inaccuracy: int = 0
var maxInaccuracy: = 256

func SetLevel(levelSet:int):
	if level > 16:
		level = 16
	else:
		level = levelSet
	if level % 2 == 0:
		return int(maxInaccuracy / level**2)
	else: 
		return int(maxInaccuracy / (level + 1)**2)

func AddInaccuracy(vector2: Vector2):
	var miss = inaccuracy * GetDistance(vector2) * 0.0015
	print(str(vector2.x - miss) + str(vector2.x + miss) + " distance: " + str(GetDistance(vector2)) + " baldis " + str((GetDistance(vector2) * 0.00015)))

	return Vector2( randf_range(vector2.x  - miss, vector2.x  + miss) ,randf_range(vector2.y  - miss, vector2.y  + miss))


func _ready():
	pass # Replace with function body.


func _process(delta):
	inaccuratePosition = AddInaccuracy(get_global_mouse_position())
	rotation = DirectionToRotation(inaccuratePosition, global_position)
	#if IsGunEquipt:
	
	if(Input.is_key_pressed(KEY_1)):
		inaccuracy = SetLevel(1)
	if(Input.is_key_pressed(KEY_2)):
		inaccuracy = SetLevel(16)
	
	giveAmmoAmount.emit(bulletsDisplay, magDisplay, CalculatePercentageOf($"../ReloadTimer".time_left, reloadSpeed))
	if $"../ReloadTimer".time_left != 0:
		return
	bulletsDisplay = bullets
	magDisplay = mag
	if Input.is_mouse_button_pressed(MOUSE_BUTTON_LEFT):
		Shoot(0.5)
	if Input.is_key_pressed(KEY_R):
		Reload(50, reloadSpeed)
	pass
	
	
func DirectionToRotation(aimPosition: Vector2, globalPosition: Vector2 ):
	var x = (aimPosition - globalPosition).x
	var y = (aimPosition - globalPosition).y
	return atan2(y, x)
	
func Shoot(rps: float):
	if $"../RoundPerSecond".time_left == 0 && mag > 0:
		var bullet = bulletPath.instantiate()
		get_parent().add_child(bullet)
		print($FireFromHere.global_position)
		bullet.Construct(get_global_mouse_position(), 20, 1000, $FireFromHere.global_position, rotation, 600)	
		mag -= 1
		$"../RoundPerSecond".start(rps)
	pass

func Reload(magSize: int, reloadTime: float):
	if mag == magSize || bullets == 0:
			return
	
	if bullets > magSize:
		bullets -= (magSize - mag)
		mag += (magSize - mag)
	else:
		mag += bullets % (magSize - mag)
		bullets = 0
	$"../ReloadTimer".start(reloadTime)	
	
func CalculatePercentageOf(numerator, denominator):
	var value: float
	if numerator != 0: 
		value = (numerator / denominator) * 100
	else:
		value = 0
	return int(value)
	
func GetDistance(position: Vector2):
	var result = sqrt(abs(position.x** 2 - global_position.x** 2) + abs(position.y** 2 - global_position.y** 2))
	return result
