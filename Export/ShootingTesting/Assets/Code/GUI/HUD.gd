extends Node


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if $GridContainer/GeneralInformation.text == "Reloading: 100%":
		$GridContainer/GeneralInformation.text = ""
	pass


func _on_player_ammo_pass_through(bullets, mag, reloadPercentage):
	#create string
	var formattedString: String = "Ammo:" + str(mag) + "/" + str(bullets)
	#add to AmmoCount text Box
	$GridContainer/AmmoCount.text = formattedString
	$GridContainer/GeneralInformation.text = "Reloading: " + str(abs(reloadPercentage - 100)) + "%"
	pass # Replace with function body.
