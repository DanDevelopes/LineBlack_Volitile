using Godot;
using Items.Weopens.Projectiles.Guns.Model.Interfaces;
using System;
using static GlobalEnums.ItemEnums.GunEnums;

namespace Items.Weopons.Projectiles.Guns.Model
{
    public class GunModel: IGunModel
    {
        public GunModel(string name, int id, GunType type, FireType fireType, int damage, float range, float speed, float spread, int roundPerSecond, int reloadTime, int ownerId)
        {
            this.name = name;
            this.id = id;
            this.type = type;
            this.damage = damage;
            this.range = range;
            this.speed = speed;
            this.spread = spread;
            this.ownerId = ownerId;
            this.roundPerSecond = roundPerSecond;
            this.reloadTime = reloadTime;
        }
        
        public int Damage()
        {
            return damage;
        }

        public float Range()
        {
            return range;
        }

        public float Speed()
        {
            return speed;
        }
        public float Spread()
        {
            return spread;
        }
        public int GetGunId()
        {
            return id;
        }
        public int RoundPerSecond(){
            return roundPerSecond;
        }
        public int ReloadTime(){
            return reloadTime;
        }
        public int OwnerId()
        {
            return ownerId;
        }
        public void UpdateOwner(int ownerId)
        {
            this.ownerId = ownerId;
        }
        public string name;
        public int id;
        public int damage;
        public float speed;
        public float range;
        public float spread;
        public int roundPerSecond;
        public int reloadTime;
        public int ownerId;
        public GunType type;
        public FireType fireType;
        public bool CanBlowUpOnYou;

    }
}