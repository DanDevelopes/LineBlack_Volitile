using System.Collections.Generic;
using System.Linq;
using static GlobalEnums.ItemEnums;

namespace Logic.Item
{
    static class ItemLogic
    {
        private static readonly object dataLock = new object();
        static List<ItemInfo> itemInfos = new();
        static public int GiveNewId(ItemTypes itemType)
        {
            ItemInfo itemInfo= new ItemInfo();
            lock (dataLock)
            {
                int id;
                if (itemInfos.Count > 0)
                {
                   
                   itemInfo.id = itemInfos.Max( x => id = x.id) + 1;
                   
                }
                else
                    id = 1;
                itemInfo.itemType = itemType;
                itemInfos.Add(itemInfo);
                return itemInfo.id;
            }
        }
        public static void pickUpItem(int itemId, int ownerId)
        {
            lock(dataLock)
            {
                itemInfos.Where(x => x.id == itemId).FirstOrDefault().ownerId = ownerId;
            }
        }
        public static void StoreItem(int itemId, int storageID)
        {
            lock(dataLock)
            {
                itemInfos.Where(x => x.id == itemId).FirstOrDefault().ownerId = storageID;
            }
        }
        public static void AddAttributes(string jsonString, int itemId)
        {
            lock(dataLock)
            {
                itemInfos.Where(x => x.id == itemId).FirstOrDefault().itemAttributesJson = jsonString;
            }
        }
    }

    class ItemInfo
    {
        public int id
        {
            get; set;
        }
        public ItemTypes itemType
        {
            get; set;
        }
        public int ownerId
        {
            get
            {
                if(isPickable != false)
                    return ownerId;
                return 0;
            } 
            set
            {
                ownerId = value;
            }

        }
        public bool isStored
        {
            get
            {
                return isStored;
            } 
            set
            {
                if(value == true)
                    isPickable = true;
                isStored = value;
            }
        }
        public bool isPickable
        {
            get; set;
        }
        public string itemAttributesJson
        {
            get; set;
        }
    }
}