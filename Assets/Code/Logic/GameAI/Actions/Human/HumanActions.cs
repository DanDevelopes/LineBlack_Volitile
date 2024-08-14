using System;
using System.Collections;
using System.Numerics;
using System.Threading.Tasks;
using Godot;
using Logic.GameAI.PathFinding.BreadCrums;
using static GlobalEnums.CharacterEnums;
using static GlobalEnums.ItemEnums;

namespace Logic.GameAI.Actions
{
    class HumanActions : ActionBase
    {
        HumanActionsTypes setAction;
        Godot.Vector2 location;
        ItemTypes getItemByType;
        internal class Actions
        {
            public Actions(CharacterBody2D character)
            {
                cover = new Cover(ref character);
                roam = new Roam(ref character);
                coverAndAttack = new CoverAndAttack(ref character);
            }
            public static Cover cover;
            public static Roam roam;
            public static CoverAndAttack coverAndAttack;
        }
        Godot.Vector2 targetLocation;
        public HumanActions(CharacterBody2D character)
        {
            new Actions(character);
            getItemByType = new ItemTypes();
            setAction = new HumanActionsTypes();
        } 

        public void SelectTarget(Godot.Vector2 location)
        {
            targetLocation = location;
        }
        

        public override void SetAction<T>(T action)
        {
            try
            {
                if (typeof(T) == typeof(HumanActionsTypes))
                {
                    this.getItemByType = ItemTypes.None;
                    this.setAction = (HumanActionsTypes)(object) action;
                    switch(setAction)
                    {
                        case HumanActionsTypes.Roam:
                            Actions.roam.LocatePath(location);
                            break;
                        case HumanActionsTypes.TakeCover:
                            Actions.cover.LocateCover(location);
                            break;
                        case HumanActionsTypes.TakeCoverAndShoot:
                            Actions.coverAndAttack.LocateCover(location);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                Task.Run(async () => await Logger.LogException(ex));
            }
        }

        public override void InterestObject<T>(T type, double radius)
        {
            
            if (typeof(T) == typeof(ItemTypes))
            {
                this.getItemByType = (ItemTypes)(object) type;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override void SetLocation(Godot.Vector2 location)
        {
            this.location = location;
        }

        public override Godot.Vector2 GetObjectiveLocation()
        {
            if(getItemByType == ItemTypes.None)
            {
                switch(setAction)
                {
                    case HumanActionsTypes.Roam:
                        return Actions.roam.Location();
                    case HumanActionsTypes.TakeCover:
                        return Actions.cover.Location();
                    case HumanActionsTypes.TakeCoverAndShoot:
                        return Actions.coverAndAttack.Location(targetLocation);
                    default:
                        throw new NotImplementedException();
                }
            }
            return this.location;
            
        }

        public override void NextObjective(Godot.Vector2 position, double radius)
        {
            switch(setAction)
            {
                case HumanActionsTypes.Roam:
                    Actions.roam.NextPath(position, radius);
                    break;
                case HumanActionsTypes.TakeCover:
                    Actions.cover.CheckLocation(position, radius);
                    break;
                case HumanActionsTypes.TakeCoverAndShoot:
                    if(targetLocation == new Godot.Vector2() {X = 0, Y = 0})
                    {
                        throw new ArgumentException("No Target Selected");
                    }
                    Actions.coverAndAttack.CheckLocation(position, radius);
                    break;
                default:
                    throw new NotImplementedException();
            }
            
        }
    }
}