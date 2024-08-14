extends CharacterBody2D

@export var tileSizeXAxis: float = 51
@export var tileSizeYAxis: float = 26
signal CharecterLocation(pos: Vector2)
signal AmmoPassThrough(bullets: int, mag: int, reloadPercentage: int)
var tileMoveX
var tileMoveY
var velMultiplier: Vector2
@export var tileDevision: int = 1
var this_delta
var tileDevisionMove
# Called when the node enters the scene tree for the first time.
func _ready():
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
		HideAllSprites()
		
		if Input.is_key_pressed(KEY_D) and Input.is_key_pressed(KEY_S):
			$DebugSpritePlayerStand45Rotation.show()
			velMultiplier += xAxisMove()
			velMultiplier += yAxisMove()
			
		elif Input.is_key_pressed(KEY_S) and Input.is_key_pressed(KEY_A):
			$DebugSpritePlayerStand135Rotation.show()
			velMultiplier += yAxisMove()
			velMultiplier -= xAxisMove()
			
		elif Input.is_key_pressed(KEY_A) and Input.is_key_pressed(KEY_W):
			$DebugSpritePlayerStand225Rotation.show()
			velMultiplier -= xAxisMove()
			velMultiplier -= yAxisMove()
			
		elif Input.is_key_pressed(KEY_W) and Input.is_key_pressed(KEY_D):
			$DebugSpritePlayerStand315Rotation.show()
			velMultiplier -= yAxisMove()
			velMultiplier += xAxisMove()
			
		elif Input.is_key_pressed(KEY_D):
			velMultiplier += xAxisMove()
			$DebugSpritePlayerStand0Rotation.show()
			
		elif Input.is_key_pressed(KEY_A):
			velMultiplier -= xAxisMove()
			$DebugSpritePlayerStand180Rotation.show()
			
		elif Input.is_key_pressed(KEY_W):
			velMultiplier -= yAxisMove()
			$DebugSpritePlayerStand270Rotation.show()
		
		elif Input.is_key_pressed(KEY_S):
			velMultiplier += yAxisMove()
			$DebugSpritePlayerStand90Rotation.show()
			
		velocity = velMultiplier
		move_and_slide()
	CharecterLocation.emit($x_y_sort.global_position)
	
	
func yAxisMove():
	return Vector2(0 ,(tileMoveY * scale.y))

func xAxisMove():
	return Vector2((tileMoveX * scale.x), 0)
	
func HideAllSprites():
	$DebugSpritePlayerStand0Rotation.hide() 
	$DebugSpritePlayerStand45Rotation.hide() 
	$DebugSpritePlayerStand90Rotation.hide() 
	$DebugSpritePlayerStand135Rotation.hide() 
	$DebugSpritePlayerStand180Rotation.hide() 
	$DebugSpritePlayerStand225Rotation.hide() 
	$DebugSpritePlayerStand270Rotation.hide() 
	$DebugSpritePlayerStand315Rotation.hide()



func _on_aimer_give_ammo_amount(bullets: int, mag: int, reloadPercentage: int):
	AmmoPassThrough.emit(bullets, mag, reloadPercentage)
	pass # Replace with function body.
