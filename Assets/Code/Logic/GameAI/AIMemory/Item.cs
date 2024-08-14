using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Godot;
public enum ItemType{
    Health,
    Weopon,
    Armour
}
public class ItemToGet
{
    public Godot.Vector2 itemLocation
    {
        get; set;
    }
    public float distanceToItem;
}

public class RememberItem
{
    public ItemType itemType
    { get; set; }
    public Godot.Vector2 itemLocation
    {get; set;}
}
public class ItemMemory
{
    List<RememberItem> rememberItems = new List<RememberItem>();
    public ItemToGet fetchClosestItem(Godot.Vector2 entityLocation, ItemType itemType)
    {
        ItemToGet itemToGet = new ItemToGet();
        var rememberItemType = rememberItems.Select(x => x).Where(x => x.itemType == itemType);
        if (!rememberItemType.Any())
        {
            itemToGet.itemLocation = entityLocation;    
            itemToGet.distanceToItem = 999;
        }
        else
        {
            float targetValue = entityLocation.Y + entityLocation.X;
            itemToGet.itemLocation = rememberItemType.OrderBy(x => Math.Abs(targetValue - (x.itemLocation.X + x.itemLocation.Y))).FirstOrDefault().itemLocation;
        }
        return itemToGet;
    }
    public void ItemSeen(Godot.Vector2 itemLocation, ItemType itemType)
    {
        RememberItem rememberItem = new RememberItem();
        rememberItem.itemType = itemType;
        rememberItem.itemLocation = itemLocation;
        rememberItems.Add(rememberItem);
    }
    
}