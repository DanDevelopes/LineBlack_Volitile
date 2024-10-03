using System;
using Godot;
namespace Logic.Character.interfaces
{
    interface INonPlayerCharacter{
        AnimatedSprite2D CharectorWalkSheet();
        Node2D ItemHolder();
        Node2D TestGun();
        RayCast2D EnemySight();
    }
}