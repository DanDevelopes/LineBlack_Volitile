using System;

namespace Logic.GameAI.Actions
{
    abstract class ActionBase
    {
        public abstract void InterestObject<T>(T type, double radius) where T : struct, Enum;
        public abstract void SetAction<T>(T action) where T : struct, Enum;
        public abstract void SetLocation(Godot.Vector2 location);
        public abstract Godot.Vector2 GetObjectiveLocation();
        public abstract void NextObjective(Godot.Vector2 position, double radius);
        
    }
}