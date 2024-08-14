using System;
using Godot;

namespace Items.Weopens.Projectiles.Guns.Model.Interfaces
{
    public interface IGunModel
    {
        int GetGunId();
        int Damage();
        float Range();
        float Speed();
        float Spread();
        int RoundPerSecond();
        int ReloadTime();
        int OwnerId();
        
    }
}