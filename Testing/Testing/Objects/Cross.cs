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
        public Cross(Vector2 direction, float speed) : base(direction, speed)
        {
        }

        public override void Clicked()
        {
            Testing.Console.UniqueLine("Cross is clicked");
        }
        public override void RightClicked()
        {
            Testing.Console.UniqueLine("Cross is right clicked");
        }
        public override void MouseOver()
        {
            Testing.Console.UniqueLine("The mouse is over the cross");
        }
    }
}