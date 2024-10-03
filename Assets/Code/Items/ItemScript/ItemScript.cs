using Godot;
using Logic.Item;
using System;
using static GlobalEnums.ItemEnums;
[Tool]
public partial class ItemScript : Node2D, IImageScript
{
	public enum ItemSelection {
		LargeHealthPack = 0, 
		SmallHealthPack = 1, 
		MeatFood = 2,
		JarEmpty = 3,
		SmallBottle = 4,
		LargeBottle = 5,
		FoodPaste = 6,
		EnergyAmmo = 7,
		ShotgunAmmo = 8,
		HeavyAmmo = 9,
		RifleAmmo = 10,
		LightAmmo = 11
		};
		
	string[] ItemImageList = new string[] 
	{ 
		"HealthPack_Large", 
		"HealthPack_Small", 
		"Food_Meat", 
		"Jar", 
		"Bottle_Small",
		"Bottle_Large",
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
    private string pathForDeterioration;
    private string pathForCooked;
    private string pathForImage;

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
				if(deteriorationBlend.Texture != null)
					deteriorationBlend.Modulate = new Godot.Color() {R = 1f, G = 1f, B = 1f, A = decay};
				if(cookedBlend.Texture != null)
					deteriorationBlend.Modulate = new Godot.Color() {R = 1f, G = 1f, B = 1f, A = cookedAmount};
			}
			else{
				Initialise(itemSelection);
				lastItemSelected = itemSelection;
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
		pathForDeterioration = $"res://Assets/Sprites/Items/{ItemImageList[(int)itemSelection]}_Deterioration.png";
		pathForCooked = $"res://Assets/Sprites/Items/{ItemImageList[(int)itemSelection]}_Cooked.png";
		pathForImage = $"res://Assets/Sprites/Items/{ItemImageList[(int)itemSelection]}.png";
		
		if(ResourceLoader.Exists(pathForDeterioration))
			deteriorationBlend.Texture = ResourceLoader.Load<Texture2D>(pathForDeterioration);
		else
			deteriorationBlend.Texture = null;
		
		if(ResourceLoader.Exists(pathForCooked))
			cookedBlend.Texture = ResourceLoader.Load<Texture2D>(pathForCooked);
		else
			cookedBlend.Texture = null;
		
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
