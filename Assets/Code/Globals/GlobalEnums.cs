using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

public static class GameInfomation
{
    public static int tilePixelSizeX{
        get 
        { 
            return 8;
        }
    }
    public static int tilePixelSizeY{
        get 
        {
            return 4;
        }
    }
}

public static class GlobalEnums
{
    
    public static class CharacterEnums
    {
        public enum CharacterTypes
        {
            Human,
            Mutant,
            CrabBot,
            Animal,
            Plant,
            RogueAi
        }
        public enum TempamentTypes
        {
            None,
            Passive,
            Agressive,
            Peaceful
        }
        public enum HumanActionsTypes
        {
            Roam,
            TakeCover,
            TakeCoverAndShoot,
            MeleeAttack,
            Shoot,
            Defend,
            Heal
        }
        public enum Factions
        {
            Scabs,
            Scavs,
            Raiders,
            Huskers,
            Splices,
            TechnoFreaks,
            Enforcers
        }
        public enum BreadcrumTypes
        {
            Path,
            Cover,
            Item,
        }
    }
    public static class ItemEnums
    {
        public enum ItemTypes
        {
            None,
            Weopon,
            Health,
            Wearable
        }
        public enum WornOn
        {
            Head,
            Torso,
            Legs,
            Arms,
            Hands,
            Feet
        }
        public static class GunEnums
        {
            public enum GunType{
                Shotgun,
                Pistol,
                Rifle,
                Laser,
            }
            public enum FireType
            {
                SingleShot,
                SemiAutomatic,
                FullAtomatic,
                BurstFire
            }
        }
    }
    public static class PlayerEnums
    {
        [Flags]
	    public enum Direction_enum {
	    	[Display(Name = "Right")]
	    	Right,
	    	[Display(Name = "DownRight")]
	    	DownRight,
	    	[Display(Name = "Down")]
	    	Down,
	    	[Display(Name = "DownLeft")]
	    	DownLeft,
	    	[Display(Name = "Left")]
	    	Left,
	    	[Display(Name = "UpLeft")]
	    	UpLeft,
	    	[Display(Name = "Up")]
	    	Up,
	    	[Display(Name = "UpRight")]
	    	UpRight
	    }
    }
}