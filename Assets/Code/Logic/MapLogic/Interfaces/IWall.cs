using System;
using Godot;

namespace Logic.MapLogic.Interfaces
{
    public interface IWall
    {
        Area2D BehindWall();
        void AddWallLocation();
    }

}