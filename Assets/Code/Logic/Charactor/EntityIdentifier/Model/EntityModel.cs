using System;
using static GlobalEnums.CharacterEnums;
namespace Logic.Character.EntityIdentifier.Model
{
    public class EntityModel
    {
        public int id{ get; set; }
        public string uniqueID { get; set; }
        public Godot.Vector2 location{ get; set; }
        public CharacterTypes characterType{ get; set; }
        
    }
}