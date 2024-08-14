using Godot;
using System;
using Player.Interfaces;
public partial class Aimer : Node2D, IAimer
{
	public Marker2D FireFromHere()
	{
		return new Marker2D();
	}
	public Marker2D fireFromHere;
	double delta;
	PackedScene bulletPath = GD.Load<PackedScene>("res://Scenes/Projectiles/bullet.tscn");
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fireFromHere = FireFromHere();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.delta = delta;
	}
}
