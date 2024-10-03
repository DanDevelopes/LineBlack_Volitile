using System.Collections.Generic;
using System.Linq;
using Logic.Character.EntityIdentifier.Model;
using static GlobalEnums.CharacterEnums;

namespace Logic.Character.EntityIdentifier
{
    public static class EntityFactions
    {
        public static readonly object factionsLock = new object();
        private static List<EntityFactionModel> factions;


        static EntityFactions()
        {
            factions = new List<EntityFactionModel>();
        }
        public static int CreateFaction(Factions faction, string factionName, int leader)
        {
            lock (factionsLock)
            {
                EntityFactionModel entityFactionModel= new EntityFactionModel()
                {
                Faction = faction,
                FactionName = factionName,
                Leader = leader
                };
                if(factions.Any())
                {
                    entityFactionModel.FactionID = 1;

                }
                else
                {
                    entityFactionModel.FactionID = factions.Max(faction => faction.FactionID) + 1;
                }
                factions.Add(entityFactionModel);
                return entityFactionModel.FactionID;
            }
        }


        public static EntityFactionModel GetFactionByEntityID(int id)
        {
            lock(factionsLock)
            {
                return factions.Where(faction => faction.EntityMembers.Contains(id) || faction.Leader == id).FirstOrDefault();
            }
        }
        public static void SetToFaction(int EntityId, int factionId)
        {
            lock (factionsLock)
            {
                factions.Where(faction => faction.FactionID == factionId).Select(faction => faction).FirstOrDefault().EntityMembers.Add(EntityId);
            }
        }
    }
}