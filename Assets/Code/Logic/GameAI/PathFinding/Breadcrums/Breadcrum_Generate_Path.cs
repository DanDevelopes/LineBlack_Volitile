using Godot;
using System;

public partial class Breadcrum_Generate_Path : Node2D
{
	Breadcrum_Generate_Path()
	{
	}
	PackedScene breadcrumImport = GD.Load<PackedScene>("res://Scenes/GameAI/Breadcrum_Path.tscn");

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
