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
    public class RotatingCross : GameObject
    {
        public RotatingCross(Vector2 position)
            : base(position)
        {
        }
        public RotatingCross()
            : base()
        {
        }

        public override void Create(GameObject createdObject)
        {
            if (createdObject == this)
            {
                Sprite.Origin = new Vector2(32, 32);
            }
        }

        public override void Update()
        {
            Sprite.Rotation += MathHelper.ToRadians(1.0f);
        }
    }
}