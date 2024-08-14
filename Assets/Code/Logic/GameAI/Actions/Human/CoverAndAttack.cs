using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Logic.GameAI.PathFinding.BreadCrums;
using Logic.InitializeGameLogic;
using static GlobalEnums.CharacterEnums;

namespace Logic.GameAI.Actions
{
    public class CoverAndAttack
    {
        EntityBreadcrumLogic breadcrumLogic;
        Godot.Vector2 position;
        CharacterBody2D character;
        enum Mode{
            Attack,
            Cover,
            Peak
        }
        Mode mode;
        public CoverAndAttack(ref CharacterBody2D character)
        {
            breadcrumLogic = new EntityBreadcrumLogic();
            mode = Mode.Cover;
            this.character = character;
        }
        
        public void LocateCover(Godot.Vector2 position)
        {
            this.position = position;
            breadcrumLogic.FilterBreadcrumsByDistance(position ,BreadcrumTypes.Cover);
            
        }
        public Godot.Vector2 Location(Godot.Vector2 targetLocation)
        {
            switch(mode)
            {
                case Mode.Cover:
                    mode = Mode.Peak;
                    return breadcrumLogic.GetBreadcrumPosition();
                case Mode.Peak:
                    mode = Mode.Cover;
                    return FindPeakPoint(position, targetLocation);
                default:
                    return breadcrumLogic.GetBreadcrumPosition();
            }
        }
        
        public void CheckLocation(Godot.Vector2 position, double radius)
        {
            breadcrumLogic.SelectNextBreadcrum(position, radius);
            this.position = position;
        }

        public Godot.Vector2 FindPeakPoint(Godot.Vector2 location, Godot.Vector2 targetLocation)
        {
            var targetDirectionInPI = GameMath.DirectionToRotation(new Godot.Vector2() {X = 1, Y = 1}, targetLocation);
            double nextIterationDirection;
            List<RayCast2D> rayCasts = new();
            RayCast2D rayCastTemplate;
            try{
            for(int i = 0; i < 100; i++ )
            {
                if(i == i % 2)
                {
                    nextIterationDirection = Math.Sin(targetDirectionInPI - Math.PI / 2);
                }
                else
                {
                    nextIterationDirection = Math.Sin(targetDirectionInPI + Math.PI / 2);
                }
                rayCastTemplate = new RayCast2D()
                {
                    GlobalPosition = location + GameMath.GetDirection(nextIterationDirection) * (5 * i),
                    TargetPosition = targetLocation,
                    CollideWithAreas = true,
                    CollideWithBodies = true,
                    HitFromInside = true
                };
                for(int y = 1; y < 33; y++)
                {
                    if(y == 2)
                    {
                        rayCastTemplate.SetCollisionMaskValue(y, true);
                    }
                    else
                    {
                        rayCastTemplate.SetCollisionMaskValue(y, false);
                    }
                }
                this.character.AddChild(rayCastTemplate);
                rayCastTemplate.ForceRaycastUpdate();
                
                rayCasts.Add(rayCastTemplate);
            }
            bool test;
            foreach(var rayCast in rayCasts)
                 test = rayCast.IsColliding();
            if(rayCasts.Where(x => !x.IsColliding()).Any())
            {
                var copy = rayCasts.Where(x => !x.IsColliding()).FirstOrDefault().GlobalPosition;
                Godot.Vector2 result = new Godot.Vector2() {X = copy.X, Y = copy.Y};
                foreach(RayCast2D rayCast in rayCasts)
                {
                    rayCast.QueueFree();
                }
                return result;
            }
                foreach(RayCast2D rayCast in rayCasts)
                {
                    rayCast.QueueFree();
                }
                mode = Mode.Cover;
                return Location(targetLocation);
            }
            catch(ArithmeticException ex)
            {
                foreach(RayCast2D rayCast in rayCasts)
                {
                    rayCast.QueueFree();
                }
                throw ex;
            }
            
        }
    }
}