using System;
using Godot;
using Logic.GameAI.PathFinding.BreadCrums;
using static GlobalEnums.CharacterEnums;
namespace Logic.GameAI.Actions
{
    public class Roam
    {
        EntityBreadcrumLogic breadcrumLogic;
        public Roam(ref CharacterBody2D character)
        {
            breadcrumLogic = new EntityBreadcrumLogic();
            
        }
        
        public void LocatePath(Godot.Vector2 position)
        {
            breadcrumLogic.FilterBreadcrumsByDistance(position ,BreadcrumTypes.Path);
        }
        public Godot.Vector2 Location()
        {
            return breadcrumLogic.GetBreadcrumPosition();
        }

        internal void NextPath(Godot.Vector2 position, double radius)
        {
            breadcrumLogic.SelectNextBreadcrum(position, radius);
        }
    }

}