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
    public static class TextureContainer
    {
        public static Dictionary<string, Texture2D> Textures;

        public static void AddTexture(string filename)
        {
            Texture2D texture = GameInfo.RefContent.Load<Texture2D>(filename);
            Textures.Add(filename, texture);
        }
        public static Sprite AddTextureAndReturnSprite(string filename, Vector2 position)
        {
            Texture2D texture = GameInfo.RefContent.Load<Texture2D>(filename);
            Textures.Add(filename, texture);

            return new Sprite(Textures[filename], position);
        }
    }
}