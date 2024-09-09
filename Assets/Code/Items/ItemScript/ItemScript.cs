using Godot;
using Logic.Item;
using System;
using static GlobalEnums.ItemEnums;

public partial class ItemScript : Node2D
{
	
	[Export] ItemTypes ItemType = ItemTypes.None;
	bool isInitialised = false;
	int id;
	ItemScript()
	{

	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!isInitialised)
		{
			return;
		}
	}
	public void Initialise(ItemTypes newItemType, string attributesJson = "")
	{
		if(this.ItemType == ItemTypes.None)
			this.id = ItemLogic.GiveNewId(newItemType);
		AddImage();
		ItemLogic.AddAttributes(attributesJson, this.id);
	}

    private void AddImage()
    {
        throw new NotImplementedException();
    }

}
