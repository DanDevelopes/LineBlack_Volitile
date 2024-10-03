using Godot;
using Logic.MapLogic;
using Logic.MapLogic.Interfaces;
using System;

public partial class Wall : StaticBody2D, IWall
{
	Wall()
	{

	}
	Area2D behindWall;
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
}
