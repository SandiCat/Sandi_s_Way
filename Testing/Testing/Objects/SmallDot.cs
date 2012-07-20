using System;
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
    public class SmallDot : GameObject
    {
        public SmallDot(Vector2 position) : base(position)
        {
        }
        public SmallDot()
            : base()
        {
        }

        public override void KeyDown(List<Keys> keys)
        {
            //StepingAngle:
            if (keys.Contains(Keys.Left))
                StepAngle(Directions.Left, 2);
            if (keys.Contains(Keys.Right))
                StepAngle(Directions.Right, 2);
            if (keys.Contains(Keys.Up))
                StepAngle(Directions.Up, 2);
            if (keys.Contains(Keys.Down))
                StepAngle(Directions.Down, 2);

            ////MovingAngle:
            //if (keys.Contains(Keys.Left))
            //    MoveAngle(Directions.Left, 2);
            //if (keys.Contains(Keys.Right))
            //    MoveAngle(Directions.Right, 2);
            //if (keys.Contains(Keys.Up))
            //    MoveAngle(Directions.Up, 2);
            //if (keys.Contains(Keys.Down))
            //    MoveAngle(Directions.Down, 2);

            ////Steping towards:
            //Vector2 somePoint = new Vector2(300, 300);
            //if (keys.Contains(Keys.Space))
            //    StepTowards(somePoint, 10);

            ////Moving towards:
            //Vector2 someOtherPoint = new Vector2(400, 300);
            //if (keys.Contains(Keys.Space))
            //    MoveTowards(someOtherPoint, 2);
        }
        public override void Collision(List<GameObject> collisions)
        {
            foreach (var obj in collisions)
            {
                if (obj.GetType() == typeof(YellowObject))
                {
                    Testing.Console.UniqueLine("Small dot collided with yellow object");
                }

                if (obj.GetType() == typeof(RedObject))
                {
                    Testing.Console.UniqueLine("Small dot collided with red object");
                }

                if (obj.GetType() == typeof(RotatingCross))
                {
                    Testing.Console.UniqueLine("Small dot collided the spinnin' cross");
                }

                if (obj.GetType() == typeof(Cross))
                {
                    Testing.Console.UniqueLine("Small dot collided the cross");
                }
            }
        }
        public override void OutsideOfWindow()
        {
            Testing.Console.UniqueLine("Small dot is not in the window");
        }
        public override void IntersectBoundary()
        {
            Testing.Console.UniqueLine("Small dot is intersecting with the boundary");
            //CreateObject(typeof(YellowObject), Sprite.Position);
        }
    }
}