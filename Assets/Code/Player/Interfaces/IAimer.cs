using Godot;
using System;
using Items.Weopons.Projectiles.Guns;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Data.Common;
using Logic.player;

namespace Player.Interfaces
{
    interface IAimer
    {
        Marker2D FireFromHere();
    }
}