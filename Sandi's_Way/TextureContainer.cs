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
        public static Dictionary<Type, Texture2D> DefaultTextures; //only keeps references to textures in the dictionary "Textures"

        public static void AddTexture(string filename)
        {
            Texture2D texture = GameInfo.RefContent.Load<Texture2D>(filename);
            Textures.Add(filename, texture);
        }
        public static Texture2D AddTextureAndReturn(string filename)
        {
            Texture2D texture = GameInfo.RefContent.Load<Texture2D>(filename);
            Textures.Add(filename, texture);

            return Textures[filename];
        }
        public static Sprite AddTextureAndReturnSprite(string filename, Vector2 position)
        {
            Texture2D texture = GameInfo.RefContent.Load<Texture2D>(filename);
            Textures.Add(filename, texture);

            return new Sprite(Textures[filename], position);
        }
    }
}

// HOW TEXTURES WORK:
/*
 * So, each object has its own sprite.
 * Multiple objects can have a sprite with the same texture.
 * Thats why we have this texture container. 
 * So you dont load two same texture into memory twice.
 * 
 * But thats not all. 
 * Usually the objects of the same type have the same texture in their sprites.
 * They are all automaticlly assigned a texture of their type from DefaultTextures.
 * So, you need to load a default texture for each type in the DefaultTextures.
 * 
 * Also, if you want an object have a different sprite than its default, just use the ChangeSprite action.
 * 
 * All textures are sorted by their filenames in the dictionary Textures.
 * Even the default ones.
*/