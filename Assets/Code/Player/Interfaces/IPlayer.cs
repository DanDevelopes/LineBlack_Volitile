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
    interface IPlayer
    {
        AnimatedSprite2D CharectorWalkSheet();
        Timer ReloadTimer();
        Timer RoundsPerSecond();
        Area2D Head();
        Area2D Torso();
        Sprite2D DebugAreaSprite();
        Sprite2D CharacterSprite();
        Camera2D PlayerCam();
        Marker2D XYsort();
        Area2D FloorTileArea();
        Node2D ItemHolder();
    }
}