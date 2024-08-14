using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Newtonsoft.Json;
using static GlobalEnums.CharacterEnums;
using static GlobalEnums.ItemEnums;

namespace Logic.GameAI.PathFinding.BreadCrums
{
    public class EntityBreadcrumLogic
    {
        List<int> idsBeenTo = new List<int>();
        private BreadcrumTypes type;
        private BreadCrum_Identifier breadCrum_Identifier;
        private int currentBreadCrumID;
        private List<BreadCrum_Identifier> breadCrumsTemplate = new List<BreadCrum_Identifier>();
        private List<BreadCrum_Identifier> breadCrum_Identifiers = new List<BreadCrum_Identifier>();
        public EntityBreadcrumLogic()
        {
            breadCrum_Identifier = new BreadCrum_Identifier();
        }
        public int GetBreadcrumAmount()
        {
            return BreadCrum_Identifiers.GetBreadcrums().Count;
        }
        public void FilterBreadcrumsByAccesibility(Godot.Vector2 position)
        {
            breadCrum_Identifiers = BreadCrum_Identifiers.GetBreadcrums();
        }
        public void FilterBreadcrumsByDistance(Godot.Vector2 position, BreadcrumTypes type, ItemTypes itemTypes = ItemTypes.None)
        {
            this.type = type;
            try
            {
                if(!breadCrum_Identifiers.Any() || type != breadCrum_Identifiers.First().breadcrumType)
                {
                    breadCrum_Identifiers = new();
                    breadCrumsTemplate = new List<BreadCrum_Identifier>();
                    breadCrumsTemplate = DeepCopy(BreadCrum_Identifiers.GetBreadcrums());
                    breadCrum_Identifiers = breadCrumsTemplate
                        .OrderBy(x => GameMath.GetDistance(x.breadcrumLocation, position))
                        .Where(x => x.breadcrumType == type && x.itemTypes == itemTypes)
                        .Select(x => x).ToList();
                }
            }
            catch (Exception ex)
            {
                GD.Print(ex);
            }
        }

        public bool IsInitualized()
        {
            return BreadCrum_Identifiers.GetBreadcrums().Count() > 1;
        }
        public Godot.Vector2 GetBreadcrumPosition()
        {
            if( breadCrum_Identifiers.Count() - idsBeenTo.Count() <= 1 && type == BreadcrumTypes.Path)
                idsBeenTo.Clear();
            if(breadCrum_Identifiers.Any())
                return breadCrum_Identifiers.FirstOrDefault(x => x.id == currentBreadCrumID).breadcrumLocation;
            else
                throw new Exception("Breadcrum Not Available. Ensure breadcrums are available");
        }
        public void SelectNextBreadcrum(Godot.Vector2 position, double radius)
        {
            foreach (var breadCrum in breadCrum_Identifiers.Where(x => GameMath.GetDistance(x.breadcrumLocation, position) < radius).Select(x => x))
            {
                if (!idsBeenTo.Intersect(breadCrum_Identifiers.Where(x => x.id == breadCrum.id).Select(x => x.id)).Any() && breadCrum.breadcrumType == BreadcrumTypes.Path)
                {
                    idsBeenTo.Add(breadCrum.id);
                    currentBreadCrumID = breadCrum.id;
                    break;
                }
                if(type == BreadcrumTypes.Cover && IsBreadcrumValid(breadCrum.id))
                {
                    currentBreadCrumID = breadCrum.id;
                    break;
                }
            }
        }
        public int GetBreadcrumID()
        {
            return currentBreadCrumID;
        }
        public bool IsBreadcrumType(BreadcrumTypes type)
        {
            var result = breadCrum_Identifiers.Any(x => x.id == currentBreadCrumID && x.breadcrumType == type);
            return result;
        }
        public bool IsBreadcrumValid(int id = -1)
        {
            if(id == -1)
            {
            
                return BreadCrum_Identifiers.GetBreadcrums().Where((x) => x.id == currentBreadCrumID).Select(x=> x).FirstOrDefault().isValid;
            }
            bool result =  BreadCrum_Identifiers.GetBreadcrums().Where((x) => x.id == id).Select(x=> x).FirstOrDefault().isValid;
            return result;
        }
        
        public static List<T> DeepCopy<T>(List<T> list)
        {
            var json = JsonConvert.SerializeObject(list);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}