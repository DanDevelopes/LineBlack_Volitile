using Godot;
using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using Logic.player;
using Player.Interfaces;
using static GlobalEnums.PlayerEnums;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Logic.Character.EntityIdentifier;
using Items.Mods;
public partial class PlayerScript : CharacterBody2D, IPlayer
{   
	#region Get Subnodes
	public Timer ReloadTimer()
	{
		return GetNode<Timer>("ReloadTimer");
	}

	public Timer RoundsPerSecond()
	{
		return GetNode<Timer>("RoundPerSecond");
	}

	public Sprite2D DebugAreaSprite()
	{
		return GetNode<Sprite2D>("DebugTileArea");
	}

	public Sprite2D CharacterSprite()
	{
		return GetNode<Sprite2D>("CharacterSprite");
	}

	public Camera2D PlayerCam()
	{
		return GetNode<Camera2D>("PlayerCam");
	}

	public Marker2D XYsort()
	{
		return GetNode<Marker2D>("x_y_sort");
	}

	public Area2D FloorTileArea()
	{
		return GetNode<Area2D>("FloorTileArea");
	}

	public Node2D Aimer()
	{
		return GetNode<Node2D>("Aimer");
	}
	#endregion
	public Node2D ItemHolder()
	{
		return GetNode<Node2D>("ItemHolder");
	}
	public Area2D Head()
    {
		return GetNode<Area2D>("Head");
    }

    public Area2D Torso()
    {
        return GetNode<Area2D>("Torso");
    }
	public AnimatedSprite2D CharectorWalkSheet()
    {
        return GetNode<AnimatedSprite2D>("CharectorWalkSheet");
    }

	#region Godot subnode objects
	public Timer reloadTimer;
	public Node2D itemHolder;
	public Timer roundsPerSecond;

	public Sprite2D debugAreaSprite;

	public Sprite2D haracterSprite;

	public Camera2D playerCam;
	private AnimatedSprite2D charectorWalkSheet;
	public Marker2D x_y_sort;
	public int health = 100;
	public Area2D floorTileArea;
	public Area2D head;
	public Area2D torso; 
	public Node2D aimer;
	#endregion
	#region	Class globals
	Godot.Vector2 velMultiplier;
	double delta;
	float tileSizeXAxis = 4 * 30;
	float tileSizeYAxis = 2 * 30;
	Signal CharectorLocation = new();
	Signal AmmoPassThrough = new();
	Direction_enum playerDirection;
	Direction_enum lastPlayerDirection;
	private int walkCounter;
	private int incrementPerFrame = 2;
	Godot.Sprite2D characterSprite;
	Godot.Vector2 lastPosition;
	float tileMoveY;
	float tileMoveX;
	int id;
	Test_Gun gun;
	#endregion
	DashDrive dashDrive;
    private bool moveKeyNotPressed;

    public PlayerScript()
	{
	}
	public override void _Ready()
	{
		// initailise
		PlayerStatus.SetHealth(health);
		playerCam = PlayerCam();
		itemHolder = ItemHolder();
		head = Head();
		torso = Torso();
		charectorWalkSheet = CharectorWalkSheet();
		characterSprite = GetNode<Sprite2D>("CharacterSprite");
		// setup
		
		head.AreaEntered += on_head_area_entered;
		torso.AreaEntered += on_torso_area_entered;
		characterSprite.Texture = ResourceLoader.Load<Texture2D>("res://Assets/Sprites/Templates/PlayerDebugSprites/DebugSprite_playerStand_90_Rotation.png");
		id = EntityIdentifier.GetNewID();
		EntityIdentifier.SetUniqueIdentifier("Player", id);
		playerDirection = Direction_enum.Down;
		
		PackedScene gunResource = GD.Load<PackedScene>("res://Scenes/Items/Weopons/Guns/Test_Gun.tscn");
		gun = gunResource.Instantiate() as Test_Gun;
		gun.Initualise(id);
		itemHolder.AddChild(gun);
		gun.Position = new Vector2() {X = 11, Y = 0};
		dashDrive = new DashDrive(this);
		Task.Run(()=> Logger.LogMessage($"{this.GetType().Name} is Initialized"));
	}

	public override void _Process(double delta)
	{
		this.delta = delta;
		dashDrive.Update(delta);
		dashDrive.Use(GetGlobalMousePosition());
		PlayerStatus.SetHealth(health);
		if(Input.IsMouseButtonPressed(MouseButton.Left))
		{
			gun.Shoot(GetGlobalMousePosition());
		}
		PlayerItemHolderDirection();
		velMultiplier = new Godot.Vector2(0,0);
		if(Godot.Input.IsKeyPressed(Key.Minus))
			ZoomOut();
		
		if(Godot.Input.IsKeyPressed(Key.Equal))
			ZoomIn();
		
		if(Godot.Input.IsKeyPressed(Key.Shift))
		{
			tileMoveX = tileSizeXAxis * 2;
			tileMoveY = tileSizeYAxis * 2;
		}
		else
		{
			tileMoveX = tileSizeXAxis;
			tileMoveY = tileSizeYAxis;
		}
		
		if (Godot.Input.IsKeyPressed(Key.D) || Godot.Input.IsKeyPressed(Key.A) || Godot.Input.IsKeyPressed(Key.W) || Godot.Input.IsKeyPressed(Key.S))
		{
			
			characterSprite.Hide();
			charectorWalkSheet.Show();
			this.Velocity = GetPlayerMovement() / 4; // * new Godot.Vector2(24f,24f);
			PlayerWalk();
			this.MoveAndSlide();
		}
		else
		{
			characterSprite.Show();
			charectorWalkSheet.Hide();
			lastPosition = Position;
			SetSpriteStandStill();
			this.Velocity = new Godot.Vector2(0,0);
		}
		PlayerStatus.SetPlayerLocation(this.GlobalPosition);
		EntityIdentifier.SetLocation(this.GlobalPosition, id);
	}


	private Godot.Vector2 GetPlayerMovement()
	{
		try
		{
			if (Godot.Input.IsKeyPressed(Key.D) && Godot.Input.IsKeyPressed(Key.W))
			{
				playerDirection = Direction_enum.UpRight;
				moveKeyNotPressed = false;
			}
			else if(Godot.Input.IsKeyPressed(Key.D) && Godot.Input.IsKeyPressed(Key.S))
			{
				playerDirection = Direction_enum.DownRight;
				moveKeyNotPressed = false;
			}
			else if (Godot.Input.IsKeyPressed(Key.A) && Godot.Input.IsKeyPressed(Key.W)) 
			{
				playerDirection = Direction_enum.UpLeft;
				moveKeyNotPressed = false;
			}
			else if(Godot.Input.IsKeyPressed(Key.A) && Godot.Input.IsKeyPressed(Key.S))
			{
				playerDirection = Direction_enum.DownLeft;
				moveKeyNotPressed = false;
			}
			else if(Godot.Input.IsKeyPressed(Key.D))
			{
				playerDirection = Direction_enum.Right;
				moveKeyNotPressed = false;
			}
			else if(Godot.Input.IsKeyPressed(Key.S))
			{
				playerDirection = Direction_enum.Down;
				moveKeyNotPressed = false;
			}
			else if(Godot.Input.IsKeyPressed(Key.A))
			{
				playerDirection = Direction_enum.Left;
				moveKeyNotPressed = false;
			}
			else if(Godot.Input.IsKeyPressed(Key.W))
			{
				playerDirection = Direction_enum.Up;
				moveKeyNotPressed = false;
			}
			else
			    moveKeyNotPressed = true;
			
			switch(playerDirection)
			{
				case Direction_enum.UpRight:
					velMultiplier -= YAxisMove() / 2;
					velMultiplier += XAxisMove() / 2;
					break;
				case Direction_enum.Right:
					velMultiplier += XAxisMove();
					break;
				case Direction_enum.DownRight:
					velMultiplier += YAxisMove() / 2;
					velMultiplier += XAxisMove() / 2;
					break;
				case Direction_enum.Down:
					velMultiplier += YAxisMove();
					break;
				case Direction_enum.DownLeft:
					velMultiplier -= XAxisMove() / 2;
					velMultiplier += YAxisMove() / 2;
					break;
				case Direction_enum.Left:
					velMultiplier -= XAxisMove();
					break;
				case Direction_enum.UpLeft:
					velMultiplier -= XAxisMove() / 2;
					velMultiplier -= YAxisMove() / 2;
					break;
				case Direction_enum.Up:
					velMultiplier -= YAxisMove();
					break;
				default:
					throw new Exception("Error: No Buttons pressed while trying to move");
			}
		
		return velMultiplier;
		}
		catch (Exception ex)
		{
			GD.Print(ex);
			return new Godot.Vector2(0f,0f);
		}
		
	}


	private Godot.Vector2 YAxisMove()
	{
		return new Godot.Vector2(0 ,tileMoveY);
	}


	private Godot.Vector2 XAxisMove()
	{
		return new Godot.Vector2(tileMoveX ,0);
	}


	private void ZoomOut()
	{
		if(playerCam.Zoom.X > 0.5)
			playerCam.Zoom -= new Godot.Vector2(0.1f,0.1f);
	}


	private void ZoomIn()
	{
		if(playerCam.Zoom.X < 2)
			playerCam.Zoom += new Godot.Vector2(0.1f,0.1f);
	}


	private void PlayerWalk()
	{
		if(lastPlayerDirection != playerDirection)
		{
			lastPosition = Position;
			lastPlayerDirection = playerDirection;
		}


		characterSprite.Hide();
		charectorWalkSheet.Frame = (int)GameMath.GetDistance(lastPosition, Position) % 32 + (32 * (int)playerDirection);
		GD.Print((int)GameMath.GetDistance(lastPosition, Position) % 31);
		
	}


	private void SetSpriteStandStill()
	{
		characterSprite.Texture = ResourceLoader.Load<Texture2D>($"res://Assets/Sprites/Templates/PlayerDebugSprites/DebugSprite_playerStand_{45 * (int)playerDirection}_Rotation.png");
	}
	
	private void PlayerItemHolderDirection()
	{
		itemHolder.Rotation = DirectionToRotation(GetGlobalMousePosition(),itemHolder.GlobalPosition);
	}
	float DirectionToRotation(Godot.Vector2 aimPosition, Godot.Vector2 globalPosition )
	{
		float x = aimPosition.X - globalPosition.X;
		float y = aimPosition.Y - globalPosition.Y;
		return (float)Math.Atan2(y, x);
	}
	public void on_head_area_entered(Area2D area)
	{
		string testString = area.Name;
		int damage = int.Parse(Regex.Match(Regex.Match(testString, @"#Damage+\[+\d+\]#").Value, @"\d+").Value);
		health -= damage * 2;
	}
	public void on_torso_area_entered(Area2D area)
	{
		string testString = area.Name;
		int damage = int.Parse(Regex.Match(Regex.Match(testString, @"#Damage+\[+\d+\]#").Value, @"\d+").Value);
		health -= damage;
	}
}
