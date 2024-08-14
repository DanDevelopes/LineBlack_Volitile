using System;
using Godot;


public static class GameMath
{
    public static float Squared(float value)
    {
        return value * value;
    }
    public static double GetDistance(Godot.Vector2 destination,Godot.Vector2 startPosition)
    {
        return Math.Abs(Math.Sqrt(Squared(destination.X - startPosition.X) + Squared(destination.Y - startPosition.Y)));
    }
    public static Godot.Vector2 GetDirection(double rotation)
	{
		var positionIncrement = new Godot.Vector2((float)Math.Cos(rotation) ,(float)Math.Sin(rotation));
		return positionIncrement;
	}
    public static float DirectionToRotation(Godot.Vector2 destination, Godot.Vector2 globalPosition )
	{
		float x = destination.X - globalPosition.X;
		float y = destination.Y - globalPosition.Y;
		return (float)Math.Atan2(y, x);
	}
}

