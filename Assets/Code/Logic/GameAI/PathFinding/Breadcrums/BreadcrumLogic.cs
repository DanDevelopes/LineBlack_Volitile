using System;
using Godot;

namespace Logic.GameAI.PathFinding.BreadCrums
{
    public class BreadcrumLogic
    {
        Godot.Vector2 breadcrumPosition;
        int breadcrumID;
        public int GetBreadcrumID()
        {
            return breadcrumID;
        }
        public BreadcrumLogic(Godot.Vector2 position, GlobalEnums.CharacterEnums.BreadcrumTypes type, bool isValid = true)
        {
            this.breadcrumPosition = position;
            breadcrumID = BreadCrum_Identifiers.AddIdentifier(breadcrumPosition, type, isValid);
        }

    }
}