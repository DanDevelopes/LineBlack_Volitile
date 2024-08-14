using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Item
{
    static class ItemLogic
    {
        private static readonly object idFetch = new object();
        static List<int> ItemIds = new();
        static public int GiveNewId()
        {
            lock (idFetch)
            {
                int id;
                if (ItemIds.Count > 0)
                {
                   
                   id = ItemIds.Max( x => id = x) + 1;
                   
                }
                else
                    id = 1;
                ItemIds.Add(id);
                return id;
            }
        }
    }
}