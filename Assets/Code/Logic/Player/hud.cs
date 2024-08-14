using Godot;
using Logic.player;
using System;

public partial class hud : CanvasLayer
{
	hud()
	{

	}
	Label health;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		health = GetNode<Label>("GridContainer/Health");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		health.Text = $"Health: {PlayerStatus.GetHealth()}";
	}
}
