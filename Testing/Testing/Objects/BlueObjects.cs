﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Sandi_s_Way;

namespace Testing
{
    public class BlueObject : GameObject
    {
        public BlueObject(Vector2 direction, float speed) : base(direction, speed)
        {
        }

        public override void KeyDown(List<Keys> keys)
        {
            //StepingAngle:
            if (keys.Contains(Keys.Left))
                Actions.StepAngle(this, Directions.Left, 3);
            if (keys.Contains(Keys.Right))
                Actions.StepAngle(this, Directions.Right, 3);
            if (keys.Contains(Keys.Up))
                Actions.StepAngle(this, Directions.Up, 3);
            if (keys.Contains(Keys.Down))
                Actions.StepAngle(this, Directions.Down, 3);

            ////MovingAngle:
            //if (keys.Contains(Keys.Left))
            //    Actions.MoveAngle(this, Directions.Left, 3);
            //if (keys.Contains(Keys.Right))
            //    Actions.MoveAngle(this, Directions.Right, 3);
            //if (keys.Contains(Keys.Up))
            //    Actions.MoveAngle(this, Directions.Up, 3);
            //if (keys.Contains(Keys.Down))
            //    Actions.MoveAngle(this, Directions.Down, 3);

            ////Steping towards:
            //Vector2 somePoint = new Vector2(300, 300);
            //if (keys.Contains(Keys.Space))
            //    Actions.StepTowards(this, somePoint, 10);

            ////Moving towards:
            //Vector2 someOtherPoint = new Vector2(400, 300);
            //if (keys.Contains(Keys.Space))
            //    Actions.MoveTowards(this, someOtherPoint, 3);
        }
    }
}