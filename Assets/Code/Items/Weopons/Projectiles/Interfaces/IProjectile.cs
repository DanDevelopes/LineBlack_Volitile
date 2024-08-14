using System;
using Godot;
using Items.Weopens.Projectiles.Guns.Model.Interfaces;

namespace Items.Weopons.Projectiles.Interfaces
{
    public interface IProjectile
    {
        void Initualize(Godot.Vector2 aimPosition, Godot.Vector2 position, float rotation, IGunModel gunInfo);
    
        Sprite2D ProjectileImage();
        Area2D WallDetector();
	    public void Range(float range);
        public void Damage(int damage);
        public void Speed(float speed);
    }
    
}