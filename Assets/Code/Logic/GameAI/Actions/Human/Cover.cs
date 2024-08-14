using System;
using Godot;
using Logic.GameAI.PathFinding.BreadCrums;
using static GlobalEnums.CharacterEnums;

namespace Logic.GameAI.Actions
{
    public class Cover
    {
        CharacterBody2D character;
        EntityBreadcrumLogic breadcrumLogic;
        public Cover(ref CharacterBody2D character)
        {
            breadcrumLogic = new EntityBreadcrumLogic();
            this.character = character;
        }
        
        public void LocateCover(Godot.Vector2 position)
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