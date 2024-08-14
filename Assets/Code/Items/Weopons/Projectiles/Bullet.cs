using System;
using Godot;
using Items.Weopons.Projectiles.Interfaces;
using Items.Weopens.Guns.Interfaces;
using Items.Weopens.Projectiles.Guns.Model.Interfaces;
public partial class Bullet : Area2D, IProjectile
{
	public Bullet()
	{

	}
	public Sprite2D ProjectileImage()
	{
		return GetNode<Sprite2D>("Bullet");
	}

	public Area2D WallDetector()
	{
		return GetNode<Area2D>("WallDetector");
	}
	public void Range(float range)
	{
		this.range = range;
	}
	public void SetOwner(int ownerId)
	{
		this.ownerId = ownerId;
	}
	public void Damage(int damage)
	{
		this.damage = damage;
	}

	public void Speed(float speed)
	{
		this.speed = speed;
	}
	int ownerId;
	float range;
	float speed;
	float rotation;
	double delta;
	int damage;
	int ownerID;
	Godot.Vector2 startPostion;
	Godot.Vector2 lastGlobalPosition;
	Godot.Vector2 aimPosition;
	public Sprite2D projectileImage;
	public delegate void BulletHitWall();
	public Area2D wallDetector;
	public bool isInitualized = false;
	// Called when the node enters the scene tree for the first time.
	bool hasHitSomething = false;

	public override void _Ready()
	{
		projectileImage = ProjectileImage();
		wallDetector = WallDetector();
		this.Monitorable = false;
		this.Monitoring = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.delta = delta;
		try{
		if (!this.isInitualized)
			throw new Exception("Bullet Not Initualized:\nmake sure to add gunInfo IGunModel before or after Instantiating");
		this.GlobalRotation = rotation;
		this.GlobalPosition = lastGlobalPosition;
		this.GlobalPosition += GameMath.GetDirection(this.rotation) * (float)(speed * delta);
		lastGlobalPosition = this.GlobalPosition;
		if (GameMath.GetDistance(startPostion, GlobalPosition) + 5 >=  GameMath.GetDistance(startPostion, aimPosition) ){
			this.Monitorable = true;
			this.Monitoring = true;
		}
		if(GameMath.GetDistance(startPostion, aimPosition) <= GameMath.GetDistance(startPostion, this.GlobalPosition))
			Free();
		if(hasHitSomething)
			Free();
		}
		catch(Exception ex)
		{
			GD.Print(ex);
		}
	}
	private void SendAttributes()
	{
		this.Name = this.Name + $"#Owner[{ownerId}]##Damage[{damage}]#";
	}
	private void _on_wall_detector_area_entered(Area2D area)
	{
		hasHitSomething = true;
	}
	private void _on_area_entered(Area2D area)
	{
		hasHitSomething = true;
	}
	public void Initualize(Godot.Vector2 destinations, Godot.Vector2 position, float rotation, IGunModel gunInfo)
	{
		Range(gunInfo.Range());
		Damage(gunInfo.Damage());
		Speed(gunInfo.Speed());
		SetOwner(gunInfo.OwnerId());
		SendAttributes();
		this.GlobalPosition = position;
		this.startPostion = position;
		this.lastGlobalPosition = position;
		this.aimPosition = destinations;
		this.rotation = GameMath.DirectionToRotation(destinations, position);
		isInitualized = true;
	}

}
