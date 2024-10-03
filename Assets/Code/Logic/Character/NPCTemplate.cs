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
        
        public NCPDirectionInformation WalkDirection(Godot.Vector2 lastPosition, Godot.Vector2 targetVelocity)
        {
            double radians = GameMath.DirectionToRotation(lastPosition, lastPosition + targetVelocity) + Math.PI; // do not add to alter DirectionToRotation as this is an issue with < -Y meaning down
            // but is read as expected by godots rotation which can not be changed on my end!!! Leave + Math.PI!!!!!
            double increment = Math.PI * 2  / 8;
            return new NCPDirectionInformation() {Direction = GameMath.GetDirection(increment * Math.Round(radians / increment)),
            DirectionFacing = (int) Math.Round(radians / increment)};
        }

    }
    public struct NCPDirectionInformation
    {
        public int DirectionFacing;
        public Godot.Vector2 Direction; 
    }
}