using Godot;
using Items.Weopens.Projectiles.Guns.Model.Interfaces;
using Items.Weopens.Guns.Interfaces;
using System;
using Items.Weopons.Projectiles.Guns.Model;
using Logic.Item;
using Logic.Guns;
using static GlobalEnums.ItemEnums;
public partial class Test_Gun : Node2D, IGun
{
	GunModel gunInfo;
	int id = 0;
	GunLogic gunLogic;
    private int ownerId;

    // Called when the node enters the scene tree for the first time.
    public Test_Gun()
	{
		id = ItemLogic.GiveNewId();
	}

	public void SetOwnerId(int ownerId)
	{
		this.ownerId = ownerId;
		gunInfo.UpdateOwner(ownerId);
	}
	public int GetGunId(){
		return gunInfo.GetGunId();
	}
	public Marker2D EndOfBarrel()
	{
		return GetNode<Marker2D>("EndOfBarrel");
	}
	public int OwnerId()
	{
		return ownerId;
	} 
	public int Damage()
	{
		return gunInfo.Damage();
	}

	public float Range()
	{
		return gunInfo.Range();
	}

	public float Speed()
	{
		return gunInfo.Speed();
	}

	public float Spread()
	{
		return gunInfo.Spread();
	}
	public int RoundPerSecond(){
		return gunInfo.RoundPerSecond();
	}
	public int ReloadTime()
	{
		return gunInfo.ReloadTime();
	}
	public void Shoot(Godot.Vector2 position)
	{
		//if (gunLogic.ReadyToShoot())
		//{
			PackedScene scene = GD.Load<PackedScene>("res://Scenes/Projectiles/bullet.tscn");
			Bullet bullet = (Bullet)scene.Instantiate();
			AddChild(bullet);
			bullet.Initualize( gunLogic.GetAccuracy(EndOfBarrel().GlobalPosition ,position), EndOfBarrel().GlobalPosition, Rotation, this);
		//}
	}
	public GunModel GunModel()
	{
		return new GunModel("test_gun", ItemLogic.GiveNewId(), GunEnums.GunType.Rifle, GunEnums.FireType.FullAtomatic, 20, 800, 600, (float)(Math.PI / 5), 2, 3, this.ownerId);
	}

	public override void _Ready()
	{
		gunInfo = GunModel();
		gunLogic = new GunLogic(this);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

    internal void Initualise(int ownerId)
    {
        this.id = ItemLogic.GiveNewId();
		this.ownerId = ownerId;
		gunInfo = GunModel();

    }

}
