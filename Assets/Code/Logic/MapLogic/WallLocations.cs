using System;
using System.Collections.Generic;

namespace Logic.MapLogic
{
    public static class WallLocations
    {
        private static readonly object WarLock = new object();
        static List<Godot.Vector2> wallLocations = new();
        static List<Godot.Vector2> GetWallLocations()
        {
            lock (WarLock)
            {
                return wallLocations;
            }
        }
        public static void AddWallLocation(Godot.Vector2 wallLocation)
        {
            wallLocations.Add(wallLocation);
        }
    }
}