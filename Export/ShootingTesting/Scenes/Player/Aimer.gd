extends Node2D

var bulletPath = load("res://Scenes/Projectiles/bullet.tscn")
var bullets = 2000
var mag = 25
var bulletsDisplay
var magDisplay
var reloadSpeed: float = 2
signal giveAmmoAmount(bullets: int, mag: int, reloadPercentage: int)
# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	rotation = DirectionToRotation(get_global_mouse_position(), global_position)
	#if IsGunEquipt:
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
