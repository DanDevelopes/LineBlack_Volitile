extends CharacterBody2D

@export var tileSizeXAxis: float = 2
@export var tileSizeYAxis: float = 4
signal CharecterLocation(pos: Vector2)
signal AmmoPassThrough(bullets: int, mag: int, reloadPercentage: int)
var tileMoveX
var tileMoveY
var velMultiplier: Vector2
@export var tileDevision: int = 1
var this_delta
var tileDevisionMove
var directionIs: direction_enum = direction_enum.Right
var textureReference = load("res://Assets/Sprites/Templates/PlayerDebugSprites/DebugSprite_playerStand_90_Rotation.png")
var walkCounter: int = 1
enum direction_enum{
	Right,
	DownRight,
	Down,
	DownLeft,
	Left,
	UpLeft,
	Up,
	UpRight,
}
const direction_enum_state_name := ["Right",
	"DownRight",
	"Down",
	"DownLeft",
	"Left",
	"UpLeft",
	"Up",
	"UpRight"]
# Called when the node enters the scene tree for the first time.
func _ready():
	$CharacterSprite.texture = textureReference
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if Input.is_key_pressed(KEY_MINUS):
		if $Camera2D.zoom.x > 0.5:
			$Camera2D.zoom -= Vector2(0.1, 0.1)
	if Input.is_key_pressed(KEY_EQUAL) || Input.is_key_pressed(KEY_PLUS):
		if $Camera2D.zoom.x < 2:
			$Camera2D.zoom += Vector2(0.1, 0.1)
	this_delta = delta
	velMultiplier = Vector2(0, 0)
	if(Input.is_key_pressed(KEY_SHIFT)):
		tileMoveY = tileSizeYAxis * 2
		tileMoveX = tileSizeXAxis * 2
	else:
		tileMoveY = tileSizeYAxis
		tileMoveX = tileSizeXAxis		
	# Movement logic
	if Input.is_key_pressed(KEY_D) or Input.is_key_pressed(KEY_A) or Input.is_key_pressed(KEY_W) or Input.is_key_pressed(KEY_S):
		if Input.is_key_pressed(KEY_D) and Input.is_key_pressed(KEY_S):
			velMultiplier += xAxisMove()
			velMultiplier += yAxisMove()
			directionIs = direction_enum.DownRight
		elif Input.is_key_pressed(KEY_S) and Input.is_key_pressed(KEY_A):
			velMultiplier += yAxisMove()
			velMultiplier -= xAxisMove()
			directionIs = direction_enum.DownLeft
			
		elif Input.is_key_pressed(KEY_A) and Input.is_key_pressed(KEY_W):
			velMultiplier -= xAxisMove()
			velMultiplier -= yAxisMove()
			directionIs = direction_enum.UpLeft
			
		elif Input.is_key_pressed(KEY_W) and Input.is_key_pressed(KEY_D):
			velMultiplier -= yAxisMove()
			velMultiplier += xAxisMove()
			directionIs = direction_enum.UpRight
			
		elif Input.is_key_pressed(KEY_D):
			velMultiplier += xAxisMove()
			directionIs = direction_enum.Right
			
		elif Input.is_key_pressed(KEY_A):
			velMultiplier -= xAxisMove()
			directionIs = direction_enum.Left
			
		elif Input.is_key_pressed(KEY_W):
			velMultiplier -= yAxisMove()
			directionIs = direction_enum.Up
		
		elif Input.is_key_pressed(KEY_S):
			velMultiplier += yAxisMove()
			directionIs = direction_enum.Down
		PlayerWalk()
		velocity = velMultiplier
		move_and_slide()
	else:
		SetSpriteStandStill(directionIs)
	CharecterLocation.emit($x_y_sort.global_position)


func PlayerWalk():
	var incrementPerFrame = 2
	if walkCounter > 8 * incrementPerFrame || walkCounter < 1 * incrementPerFrame:
		walkCounter = 1
	var value = int(int(walkCounter) / 2)
	if value < 1:
		value = 1
	if direction_enum_state_name[directionIs] == "Right" || direction_enum_state_name[directionIs] == "Left":
		textureReference =  load("res://Assets/Sprites/Templates/PlayerDebugSprites/walk" + direction_enum_state_name[directionIs] + " #"+str(value)+".png")
		$CharacterSprite.texture = textureReference
	else:
		SetSpriteStandStill(directionIs)
	walkCounter += 1


func yAxisMove():
	return Vector2(0 ,(tileMoveY * scale.y))


func xAxisMove():
	return Vector2((tileMoveX * scale.x), 0)


func SetSpriteStandStill(direction: direction_enum):
	textureReference =  load("res://Assets/Sprites/Templates/PlayerDebugSprites/DebugSprite_playerStand_"+ str(45 * direction) +"_Rotation.png")
	$CharacterSprite.texture = textureReference


func _on_aimer_give_ammo_amount(bullets: int, mag: int, reloadPercentage: int):
	AmmoPassThrough.emit(bullets, mag, reloadPercentage)
	pass # Replace with function body.
