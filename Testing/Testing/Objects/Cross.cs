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
    public class Cross : GameObject
    {
        public Cross(Vector2 position)
            : base(position)
        {
        }
        public Cross()
            : base()
        {
        }

        public override void Create(GameObject createdObject)
        {
            Sprite.Origin = new Vector2(32, 32);
            Sprite.Rotation = MathHelper.ToRadians(45);
        }

        public override void Clicked()
        {
            Testing.Console.UniqueLine("Cross is clicked");
            Sprite.Scale += 0.1f;
        }
        public override void RightClicked()
        {
            Testing.Console.UniqueLine("Cross is right clicked");
        }
        public override void MouseOver()
        {
            Testing.Console.UniqueLine("The mouse is over the cross");
        }

        public override void Draw()
        {
            Sprite.DrawRectangle();
            Sprite.Draw();
        }
    }
}