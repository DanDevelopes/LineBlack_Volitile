using Godot;
using Logic.MapLogic;
using Logic.MapLogic.Interfaces;
using System;

public partial class rightWall : StaticBody2D, IWall
{
	rightWall(){

	}
	Area2D behindWall;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		behindWall = BehindWall();
		behindWall.AreaEntered += BehindWallEntered;
		behindWall.AreaExited += BehindWallExited;
		AddWallLocation();
		this.YSortEnabled = false;
		this.ZIndex = 0;
		Godot.Color color = new Godot.Color() {R = 1, G = 1, B = 1, A = 1};
		this.Modulate = color;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void BehindWallEntered(Area2D area2D)
	{

		this.YSortEnabled = true;
		this.ZIndex = 2;
		Godot.Color color = new Godot.Color() {R = 1, G = 1, B = 1, A = 0.733f};
		this.Modulate = color;
		
	}
	private void BehindWallExited(Area2D area2D)
	{
		this.YSortEnabled = false;
		this.ZIndex = 0;
		Godot.Color color = new Godot.Color() {R = 1, G = 1, B = 1, A = 1};
		this.Modulate = color;
	}

	public Area2D BehindWall()
	{
		return GetNode<Area2D>("BehindWall");
	}

	public void AddWallLocation()
	{
		WallLocations.AddWallLocation(this.GlobalPosition);
	}
	//	extends StaticBody2D

	//signal charactorPosition
	//# Called when the node enters the scene tree for the first time.
	//func _ready():
	//	pass # Replace with function body.


	//# Called every frame. 'delta' is the elapsed time since the previous frame.
	//func _process(delta):

	//	pass

	//func _on_area_2d_area_entered(area):
	//	y_sort_enabled = true
	//	z_index = 1
	//	modulate = "ffffffbb"

	//func _on_area_2d_area_exited(area):
	//	y_sort_enabled = false
	//	z_index = 0
	//	modulate = "ffffffff"
	//	pass # Replace with function body.



}
