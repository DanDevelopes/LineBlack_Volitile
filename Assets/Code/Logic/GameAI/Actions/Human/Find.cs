using System;
using Godot;
using Logic.GameAI.PathFinding.BreadCrums;
using static GlobalEnums.CharacterEnums;
using static GlobalEnums.ItemEnums;

namespace Logic.GameAI.Actions
{
    public class Find
    {
        EntityBreadcrumLogic breadcrumLogic;
        public Find()
        {
            breadcrumLogic = new EntityBreadcrumLogic();
            
        }
        
        public void LocateItem(Godot.Vector2 position, ItemTypes itemType)
        {
            breadcrumLogic.FilterBreadcrumsByDistance(position ,BreadcrumTypes.Cover);
        }
        public Godot.Vector2 Location()
        {
            return breadcrumLogic.GetBreadcrumPosition();
        }

        internal void CheckLocation(Godot.Vector2 position, double radius)
        {
            breadcrumLogic.SelectNextBreadcrum(position, radius);
        }
    }
}