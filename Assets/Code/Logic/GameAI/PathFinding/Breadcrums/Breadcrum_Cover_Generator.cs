using Godot;
using System;

public partial class Breadcrum_Cover_Generator : Node2D
{
	Breadcrum_Cover_Generator()
	{
	}
	PackedScene breadcrumImport = GD.Load<PackedScene>("res://Scenes/GameAI/Breadcrum_Cover.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var breadcrum = breadcrumImport.Instantiate();
		this.AddChild(breadcrum);
	}	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
