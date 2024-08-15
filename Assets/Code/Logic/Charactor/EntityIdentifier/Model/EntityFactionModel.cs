using System.Collections.Generic;
using static GlobalEnums.CharacterEnums;

namespace Logic.Character.EntityIdentifier.Model
{
    public class EntityFactionModel
    {  
        public string FactionName;
        public int FactionID;
        public Factions Faction
        { 
            get; 
            set; 
        }
        public List<int> EntityMembers
        {
            get;
            set;
        }
        public int Leader
        {
            get; 
            set;
        }
    }
}