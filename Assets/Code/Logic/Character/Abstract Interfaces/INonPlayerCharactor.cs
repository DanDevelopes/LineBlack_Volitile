using System;
using Godot;

namespace Logic.Charactor.Interfaces
{
    public interface INonPlayerCharactor
    {
        NavigationAgent2D PathFinder();
        Marker2D XYsort();
        Area2D Head();
        Area2D Torso();
        Label Health();
    }
}