using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static GlobalEnums.ItemEnums;
namespace Logic.GameAI.PathFinding.BreadCrums
{
    public static class BreadCrum_Identifiers
    {
        public static object breadcrumLock = new object();
        private static List<BreadCrum_Identifier> identifiers = new List<BreadCrum_Identifier>();
        
        public static int AddIdentifier(Godot.Vector2 breadcrumLocation, GlobalEnums.CharacterEnums.BreadcrumTypes breadcrumType, bool isValid)
        {  
            lock (breadcrumLock)
            {
                BreadCrum_Identifier identifier = new BreadCrum_Identifier();
                if(!identifiers.Any())
                {
                    identifier.id = 1;
                    identifier.breadcrumLocation = breadcrumLocation;
                    identifier.breadcrumType = breadcrumType;
                    identifier.isValid = isValid;
                }
                else 
                {
                    identifier.id = identifiers.Max(x => x.id) + 1;
                    identifier.breadcrumLocation = breadcrumLocation;
                    identifier.breadcrumType = breadcrumType;
                    identifier.isValid = isValid;
                }
                identifiers.Add(identifier);
                return identifier.id;
            }

        }
        public static List<BreadCrum_Identifier> GetBreadcrums()
        {
            lock (breadcrumLock)
            {
                NullCheck();
                return identifiers;
            }
        }
        private static void NullCheck()
        {
            if(!identifiers.Any())
            {
                throw new Exception("identifiers does not contain any breadcrums");
            }
        }

    } 
    public class BreadCrum_Identifier
    {
        public BreadCrum_Identifier()
        {
            itemTypes = ItemTypes.None;
        }
        public int id
        {
            get;
            set;
        }
        public Godot.Vector2 breadcrumLocation
        {
            get;
            set;
        }
        public GlobalEnums.CharacterEnums.BreadcrumTypes breadcrumType
        {
            get;
            set;
        }
        public bool isValid
        {
            get;
            set;
        }
        public ItemTypes itemTypes
        {
            get;
            set;
        }
    }
}