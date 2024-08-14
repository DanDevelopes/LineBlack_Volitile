using Godot;
using System;
using Logic.GameAI.PathFinding.BreadCrums;
using static GlobalEnums;
using System.Linq;
using Logic.player;
public partial class Breadcrum_Cover : Node2D
{
	Breadcrum_Cover()
	{
	}
	BreadcrumLogic breadcrumLogic;
	int breadcrumID;
	Godot.RayCast2D seePlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		breadcrumLogic = new BreadcrumLogic(this.GlobalPosition, CharacterEnums.BreadcrumTypes.Cover);
		breadcrumID = breadcrumLogic.GetBreadcrumID();
		seePlayer = GetNode<RayCast2D>("SeePlayer");
		
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		seePlayer.TargetPosition = PlayerStatus.PlayerLocation();
		
		var test = seePlayer.IsColliding();
		
		BreadCrum_Identifiers.GetBreadcrums().Where(x => x.id == breadcrumID).First().isValid = seePlayer.IsColliding();
	}
}
