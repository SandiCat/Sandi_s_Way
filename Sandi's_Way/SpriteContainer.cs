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

namespace Sandi_s_Way
{
    public static class SpriteContainer
    {
        public static Dictionary<string, Sprite> Sprites;

        public static void AddSprite(string filename)
        {
            Sprite sprite = new Sprite(GameInfo.RefContent.Load<Texture2D>(filename));
            Sprites.Add(filename, sprite);
        }
    }
}
/* HOW TO DO SPRITE STORING:
 * 
 * You first initialize the dictionary in the LoadContent() and then add your sprites with the AddSprite() method.
 * You can later acces them by filename when creating GameObjects. 
 * The reason I made this object is because a lot of objects of the same type are gonna have the same sprites, but not always.
 * Its more efficent than having every object holding its sprite. They all have a seperate reference, but each sprite is allocated only once.
*/