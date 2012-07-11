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
        float speed = 3;

        public BlueObject(Vector2 direction, float speed) : base(direction, speed)
        {
        }

        public override void KeyDown(List<Keys> keys)
        {
            if(keys.Contains(Keys.Left))
                Actions.StepFixedDirection(this, Directions.Left);
            if (keys.Contains(Keys.Right))
                Actions.StepFixedDirection(this, Directions.Right);
            if (keys.Contains(Keys.Up))
                Actions.StepFixedDirection(this, Directions.Up);
            if (keys.Contains(Keys.Down))
                Actions.StepFixedDirection(this, Directions.Down);
        }
    }
}