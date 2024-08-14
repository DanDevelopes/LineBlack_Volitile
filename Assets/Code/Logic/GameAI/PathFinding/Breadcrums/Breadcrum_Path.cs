using Godot;
using System;
using Logic.GameAI.PathFinding.BreadCrums;
using static GlobalEnums;
using static GlobalEnums.CharacterEnums;
public partial class Breadcrum_Path : Area2D
{
	Breadcrum_Path()
	{
	}
	BreadcrumLogic breadcrumLogic;
	int breadcrumID;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		breadcrumLogic = new BreadcrumLogic(this.GlobalPosition, BreadcrumTypes.Path);
		breadcrumID = breadcrumLogic.GetBreadcrumID();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		int i = breadcrumID;
	}
}
