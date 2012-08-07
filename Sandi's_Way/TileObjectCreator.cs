using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Sandi_s_Way
{
    public static class TileObjectCreator
    {
        public static void Create(int elementWidth, int elementHeight, Vector2 position, 
            Dictionary<char, Type> discription, params string[] rows)
        {
            float x = position.X;
            float y = position.Y;


            foreach (var row in rows)
            {
                foreach (var character in row)
                {
                    ObjectManager.Create(discription[character], new Vector2(x, y));
                    x += elementWidth;
                }

                y += elementHeight;
            }
        }
    }
}
