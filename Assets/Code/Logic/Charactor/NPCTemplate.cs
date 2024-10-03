using System;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using Godot;

namespace Logic.Character
{
    public class NPCTemplate
    {
        public NPCTemplate()
        {

        }
        
        public DirectionInfo WalkDirection(Godot.Vector2 lastPosition, Godot.Vector2 targetVelocity)
        {
            double radians = GameMath.DirectionToRotation(lastPosition, lastPosition + targetVelocity) + Math.PI; // do not add to alter DirectionToRotation as this is an issue with < -Y meaning down
            // but is read as expected by godots rotation which can not be changed on my end!!! Leave + Math.PI!!!!!
            double increment = Math.PI * 2  / 8;
            return new DirectionInfo() {Direction = GameMath.GetDirection(increment * Math.Round(radians / increment)),
            DirectionFacing = (int) Math.Round(radians / increment)};//GameMath.GetDirection(increment * Math.Round(radians / increment));
        }

    }
    public struct DirectionInfo
    {
        public int DirectionFacing;
        public Godot.Vector2 Direction; 
    }
}