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
    public class BlueObject : GameObject
    {
        public BlueObject(Vector2 direction, float speed) : base(direction, speed)
        {
        }

        public override void KeyDown(List<Keys> keys)
        {
            //StepingAngle:
            if (keys.Contains(Keys.A))
                Actions.StepAngle(this, Directions.Left, 3);
            if (keys.Contains(Keys.D))
                Actions.StepAngle(this, Directions.Right, 3);
            if (keys.Contains(Keys.W))
                Actions.StepAngle(this, Directions.Up, 3);
            if (keys.Contains(Keys.S))
                Actions.StepAngle(this, Directions.Down, 3);

            ////MovingAngle:
            //if (keys.Contains(Keys.A))
            //    Actions.MoveAngle(this, Directions.Left, 3);
            //if (keys.Contains(Keys.D))
            //    Actions.MoveAngle(this, Directions.Right, 3);
            //if (keys.Contains(Keys.W))
            //    Actions.MoveAngle(this, Directions.Up, 3);
            //if (keys.Contains(Keys.S))
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
        public override void Collision(List<GameObject> collisions)
        {
            if (collisions.Contains(Testing.Yellow))
            {
                Testing.Console.UniqueLine("Blue object collided with yellow object");
            }

            if (collisions.Contains(Testing.Red))
            {
                Testing.Console.UniqueLine("Blue object collided with red object");
            }
        }
    }
}
//small change