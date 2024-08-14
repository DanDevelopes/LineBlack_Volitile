using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Timers;
using Godot;
using Items.Weopens.Guns.Interfaces;
using static GameInfomation;
using static GameMath;
namespace Logic.Guns
{
    public class GunLogic
    {   
        private static readonly object timerLock = new object();
        float tileSizeX;
        float tileSizeY;
        int gunID;
        IGun gun;
        double delta;
        double ReloadTimeIncrement;
        public GunLogic(IGun gun)
        {
            this.tileSizeX = tilePixelSizeX;
            this.tileSizeY = tilePixelSizeY;
            Initualize(gun);
        }
        void updateDelta(double delta){
            this.delta = delta;
        }
        public Godot.Vector2 GetAccuracy(Godot.Vector2 startPostion, Godot.Vector2 endPostion)
        {   
            Random r = new Random();
            double distance = GetDistance(startPostion, endPostion);
            int range = (int)(gun.Spread() * distance / 5);
            double randVal = r.Next(-range,range);
            GD.Print(randVal);
            return new Godot.Vector2((float)(endPostion.X + randVal), (float)(endPostion.Y + randVal));
        }

        public bool ReadyToShoot(){
                return true;
        }

        void Initualize(IGun gun){
            this.gun = gun;
            this.gunID = gun.GetGunId();
        }
        int GetBulletsAvialable(){
            return 2000;
        }
        void ReloadTimer_GetIncrement(){
            if(delta != 0){
                ReloadTimeIncrement = delta / gun.ReloadTime();
            }
        }
    }
    
}