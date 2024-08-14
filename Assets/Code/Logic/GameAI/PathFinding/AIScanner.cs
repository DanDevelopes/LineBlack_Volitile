using Godot;
using System;

public partial class AIScanner : Area2D
{
	bool PathComplete;
	bool PathDoable;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (PathComplete)
		{
			if (PathDoable)
			{

			}
			else
			{
				Free();
			}
		}
	}

}
