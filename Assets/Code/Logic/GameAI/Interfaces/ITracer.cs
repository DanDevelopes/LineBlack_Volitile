using System;
using Godot;
namespace Logic.GameAI.Interfaces
{
    public interface ITracer
    {
        Area2D ReferenceTracer();
        CollisionObject2D CollisionDetector();
    }
}
