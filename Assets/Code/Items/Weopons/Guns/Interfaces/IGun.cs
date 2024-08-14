using Items.Weopens.Projectiles.Guns.Model.Interfaces;
using Items.Weopons.Projectiles.Guns.Model;

namespace Items.Weopens.Guns.Interfaces
{
    public interface IGun: IGunModel
    {
        GunModel GunModel();
        Godot.Marker2D EndOfBarrel();
    }
}