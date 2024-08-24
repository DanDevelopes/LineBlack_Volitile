using Godot;
using System;

namespace Items.Mods
{
    public class DashDrive
    {
        float dashCooldownTimeSecs = 3;
	    int dashCount = 3;
	    int dashsUsed;
	    double dashCooldownCounter;
        CharacterBody2D body;
        public DashDrive(CharacterBody2D body)
        {
            this.body = body;
        }
        public void Update(double delta)
        {
        
		    dashCooldownCounter += delta;
            dashTimer();
        }

        void dashTimer()
	    {
            
		    if(dashsUsed >= dashCount)
		    {
		    	dashCooldownTimeSecs = 5;
		    	dashsUsed = 0;
		    	dashCooldownCounter = 0;
		    }
		    if(dashCooldownCounter > 2 && dashsUsed != 0)
		    {
		    	dashCooldownTimeSecs = 1;
		    	dashsUsed = 0;
		    	dashCooldownCounter = 0;
		    }
	    }
        public void Use(Godot.Vector2 location)
        {
            if(Input.IsMouseButtonPressed(MouseButton.Right)
			&& dashCooldownCounter >= dashCooldownTimeSecs
		    )
		    {
			    if(
			    	GameMath.GetDistance(body.Position, location) < 200
			    )
			    {
			    	body.Position = location;
			    	dashCooldownTimeSecs = 0.75f;
			    	dashCooldownCounter = 0;
			    	dashsUsed++;
			    }
		    }
        }

    }
}