using System;
using Godot;

namespace Logic.Charactor.Interfaces
{
    abstract class Charactor
    {
        public abstract int GetHealth();
        public abstract Godot.Vector2 GetPosition();
        
        
    }
}