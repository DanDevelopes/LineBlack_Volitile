using Godot;
using System;
using Logic.GameAI.PathFinding.BreadCrums;
using Logic.Charactor.Interfaces;
using System.Threading.Tasks;
using Logic.GameAI.Actions;
using static GlobalEnums.CharacterEnums;
using System.Threading;
using Logic.Character.interfaces;
using System.Text.RegularExpressions;
using Logic.Character.EntityIdentifier;


public partial class EnemyTest : CharacterBody2D, INonPlayerCharactor, ITestEnemy
{
	#region class globals
	int id;
	[Export] float speed = 50;
	object initLock = new object();
	double delta;
	Test_Gun gun;
	int health;
	Area2D head;
	Area2D torso;
	RayCast2D enemySight;
	Label healthLable;
	Godot.Vector2 lastGlobalPosition;
	Node2D pathFinderPositionNode;
	[Export] NavigationAgent2D pathFinder;
	bool isInitualized = false;
	bool isFindingCover = false;
	bool isTakingCover;
	int targetId;
	Node2D itemHolder;
	HumanActions actions;
	private Marker2D x_y_sort;
	private EntityBreadcrumLogic breadcrumLogic = new EntityBreadcrumLogic();
	#endregion
	#region class SubNodes
	public NavigationAgent2D PathFinder()
	{
		pathFinderPositionNode = GetNode<Node2D>("PathFinderPosition");
		return pathFinderPositionNode.GetNode<NavigationAgent2D>("PathFinder");
	}

	public Marker2D XYsort()
	{
		return GetNode<Marker2D>("x_y_sort");
	}

	public Area2D Head()
	{
		return GetNode<Area2D>("Head");
	}

	public Area2D Torso()
	{
		return GetNode<Area2D>("Torso");
	}
	
	public Label Health(){
		return GetNode<Label>("Health");
	}
	
	public Node2D ItemHolder()
	{
		return GetNode<Node2D>("ItemHolder");
	}

	public Node2D TestGun()
	{
		return GetNode<Node2D>("ItemHolder/TestGun");
	}

	public RayCast2D EnemySight()
	{
		return GetNode<RayCast2D>("ItemHolder/EnemySight");
	}
	#endregion
	#region SetUp
	EnemyTest()
	{
		actions = new HumanActions(this);
	}
	
	public override void _Ready()
	{
		id = EntityIdentifier.GetNewID();
		x_y_sort = XYsort();
		health = 100;
		head = Head();
		torso = Torso();
		healthLable = Health();
		head.AreaEntered += on_head_area_entered;
		torso.AreaEntered += on_torso_area_entered;
		pathFinder = PathFinder();
		enemySight = EnemySight();
		itemHolder = ItemHolder();

		pathFinder.AvoidanceEnabled = true;
		pathFinder.AvoidanceMask = 1;
		

		PackedScene gunResource = GD.Load<PackedScene>("res://Scenes/Items/Weopons/Guns/Test_Gun.tscn");
		gun = gunResource.Instantiate() as Test_Gun;
		gun.Initualise(id);
		itemHolder.AddChild(gun);
		gun.Position = new Vector2() {X = 11, Y = 0};


		Thread setUpThread = new Thread(async () => await AsyncSetup());
		setUpThread.Start();
		setUpThread.Priority = ThreadPriority.AboveNormal;
		
		
		Task.Run(async () => await Logger.LogMessage($"{this.GetType().Name} is Initialized"));
	}


	private async Task AsyncSetup()
	{
		await ToSignal(GetTree(), "physics_frame");
		lock(initLock)
		{
			while(!isInitualized)
			{
				try
				{
					if (breadcrumLogic.GetBreadcrumAmount() > 0)
					{
						actions.SetLocation(GlobalPosition);
						actions.SetAction(HumanActionsTypes.Roam);
						actions.NextObjective(GlobalPosition, 2000);
						pathFinder.TargetPosition = actions.GetObjectiveLocation();
						isInitualized = true;
					}
				}
				catch(Exception ex)
				{
					Task.Run(async () => await Logger.LogException(ex));
				}
			}
		}		
	}
	#endregion

	public override void _PhysicsProcess(double delta)
	{
		lock(initLock)
		{
		try{
			EntityIdentifier.SetLocation(this.GlobalPosition, id);
			if(health <= 0)
			{
				BecomeLoot();
			}
			healthLable.Text = $"Health {health}";
			Vector2 nextPathPoint = pathFinder.GetNextPathPosition() - this.pathFinderPositionNode.GlobalPosition;
			if(!isInitualized)
				return;
			actions.SetLocation(GlobalPosition);
			if(targetId > 0 )
			{
				AttackTarget();
			}
			if (this.health == 100)
			{
				this.actions.SetAction(HumanActionsTypes.Roam);
			}
			else
			{
				this.actions.SetAction(HumanActionsTypes.TakeCoverAndShoot);
			}
			if (pathFinder.IsNavigationFinished())
			{
				actions.NextObjective(this.GlobalPosition, 2000);
				
			}
			pathFinder.TargetPosition = actions.GetObjectiveLocation();
			this.Velocity = nextPathPoint.Normalized() * speed;
			pathFinder.Velocity = this.Velocity;
			MoveAndSlide();
		}
		catch(Exception ex)
		{
			Task.Run(async () => await Logger.LogException(ex));
		}
		}
	}


    private void BecomeLoot()
    {
        // loot needs to be implemnted here. 
		this.QueueFree();
    }


    private void ItemHolderDirection(Godot.Vector2 Position)
	{
		itemHolder.Rotation = GameMath.DirectionToRotation(GetGlobalMousePosition(),itemHolder.GlobalPosition);
	}


	Godot.Vector2 GetFootPostition()
	{
		return x_y_sort.GlobalPosition;
	}


	private void AttackTarget()
	{
		var targetLocation = EntityIdentifier.GetLocation(targetId);
		enemySight.TargetPosition = targetLocation;
		actions.SelectTarget(targetLocation);
		ItemHolderDirection(targetLocation);
		if(!enemySight.IsColliding())
		{
			gun.Shoot(targetLocation);

		}
	}


	public void on_head_area_entered(Area2D area){
		string testString = area.Name;
		targetId = int.Parse(Regex.Match(Regex.Match(testString, @"#Owner+\[+\d+\]#").Value, @"\d+").Value); // Bullet#Owner[1]##Damage[20]#
		int damage = int.Parse(Regex.Match(Regex.Match(testString, @"#Damage+\[+\d+\]#").Value, @"\d+").Value);
		health -= damage * 2;
	}


	public void on_torso_area_entered(Area2D area){
		string testString = area.Name;
		targetId = int.Parse(Regex.Match(Regex.Match(testString, @"#Owner+\[+\d+\]#").Value, @"\d+").Value); // Bullet#Owner[1]##Damage[20]#
		int damage = int.Parse(Regex.Match(Regex.Match(testString, @"#Damage+\[+\d+\]#").Value, @"\d+").Value);
		health -= damage;
	}
}
