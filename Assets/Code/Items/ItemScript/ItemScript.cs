using Godot;
using Logic.Item;
using System;
using System.IO;
using static GlobalEnums.ItemEnums;
[Tool]
public partial class ItemScript : Node2D, IImageScript
{
	public enum ItemSelection {
		LargeHealthPack = 0, 
		SmallHealthPack = 1, 
		MeatFood = 2,
		JarEmpty = 3,
		JarWater = 4,
		SmallBottle = 5,
		SmallWaterBottle = 6,
		LargeBottle = 7,
		LargeWaterBottle = 8,
		FoodPaste = 9,
		EnergyAmmo = 10,
		ShotgunAmmo = 11,
		HeavyAmmo = 12,
		RifleAmmo = 13,
		LightAmmo = 14
		};
		
	string[] ItemImageList = new string[] 
	{ 
		"HealthPack_Large", 
		"HealthPack_Small", 
		"Food_Meat", 
		"Jar", 
		"Jar_Water",
		"Bottle_Small",
		"Bottle_Small_Water",
		"Bottle_Large",
		"Bottle_Large_Water",
		"Food_Paste",
		"Ammo_Energy",
		"Ammo_Shotgun",
		"Ammo_Heavy",
		"Ammo_Rifle",
		"Ammo_Light"
	};
	Sprite2D itemImage;
	private Sprite2D deteriorationBlend;
	Sprite2D cookedBlend;
	ItemTypes ItemType = ItemTypes.None;
	bool isInitialised = false;
	int id;
    [Export] float decay;
	[Export] float cookedAmount;
    [Export] ItemSelection itemSelection;
    ItemSelection lastItemSelected;
	ItemScript()
	{

	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		itemImage = ItemImage();
		deteriorationBlend = DeteriorationBlend();
		cookedBlend = CookedBlend();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		try{
			if(lastItemSelected == itemSelection && itemImage.Texture != null)
			{
				
				deteriorationBlend.Modulate = new Godot.Color() {R = 1f, G = 1f, B = 1f, A = decay};
				return;
			}
			else{
				Initialise(itemSelection);
			}
		}
		catch(Exception ex)
		{
			Logger.LogException(ex);
		}
	}
	public void Initialise (ItemSelection itemSelection, string attributesJson, int i = 0)
	{
		this.itemSelection = itemSelection;

	}
	private void Initialise(ItemSelection newItemSelect = ItemSelection.EnergyAmmo, string attributesJson = "")
	{
		if(this.ItemType == ItemTypes.None)
			//this.id = ItemLogic.GiveNewId(newItemType);
		AddImage(newItemSelect);
		ItemLogic.AddAttributes(attributesJson, this.id);
	}

    private void AddImage(ItemSelection newItemSelect)
    {
		var pathForDeterioration = $"res://Assets/Sprites/Items/{ItemImageList[(int)itemSelection]}_Deterioration.png";
		var pathForImage = $"res://Assets/Sprites/Items/{ItemImageList[(int)itemSelection]}.png";
		if(Directory.Exists(pathForDeterioration))
			deteriorationBlend.Texture = ResourceLoader.Load<Texture2D>(pathForDeterioration);
		itemImage.Texture = ResourceLoader.Load<Texture2D>(pathForImage);
		
    }

    public Sprite2D ItemImage()
    {
        return GetNode<Sprite2D>("ItemImage");
    }

    public Sprite2D DeteriorationBlend()
    {
        return GetNode<Sprite2D>("DeteriorationBlend");
    }

    public Sprite2D CookedBlend()
    {
        return GetNode<Sprite2D>("CookedBlend");
    }
}
