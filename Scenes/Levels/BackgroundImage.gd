extends Sprite2D

@export var tileSizeXAxis: float = 51
@export var tileSizeYAxis: float = 26

var tileMoveX
var tileMoveY

@export var tileDevision: int = 1
var this_delta
var tileDevisionMove
# Called when the node enters the scene tree for the first time.
func _ready():
	# gaurd clause
	#if tileDevision % 2 != 0:
	#	tileDevisionMove = tileDevision + tileDevision % 2
	#else:
	tileDevisionMove = tileDevision
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	this_delta = delta
	
	if(Input.is_key_pressed(KEY_SHIFT)):
		tileMoveY = tileSizeYAxis * 2
		tileMoveX = tileSizeXAxis * 2
	else:
		tileMoveY = tileSizeYAxis
		tileMoveX = tileSizeXAxis		
	# Movement logic
	if Input.is_key_pressed(KEY_D) or Input.is_key_pressed(KEY_A) or Input.is_key_pressed(KEY_W) or Input.is_key_pressed(KEY_S):
		if Input.is_key_pressed(KEY_D):
			position -= xAxisMove()
		if Input.is_key_pressed(KEY_A):
			position += xAxisMove()
		if Input.is_key_pressed(KEY_W):
			position += yAxisMove()
		if Input.is_key_pressed(KEY_S):
			position -= yAxisMove()
	

func yAxisMove():
	return Vector2(0 ,(tileMoveY * scale.y) / tileDevisionMove) * this_delta

func xAxisMove():
	return Vector2((tileMoveX * scale.x) / tileDevisionMove, 0) * this_delta
