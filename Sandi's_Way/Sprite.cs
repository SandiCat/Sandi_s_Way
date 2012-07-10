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
    public class Sprite //keeps an image and draws it
    {
        private Texture2D _image;
        public Rectangle Rectangle;

        public float Scale = 1.0f;
        public float Rotation = 0.0f;
        public Vector2 Position = new Vector2();
        public Vector2 Origin = new Vector2();

        public Sprite(Texture2D image, Vector2 position, Vector2 origin, float roatation, float scale)
        {
            _image = image;
            Rectangle = new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);

            Scale = scale;
            Rotation = roatation;
            Position = position;
            Origin = origin;
        }
        public Sprite(Texture2D image, Vector2 position)
        {
            _image = image;
            Rectangle = new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);

            Position = position;
        }

        public void Draw()
        {
            GameInfo.RefSpriteBatch.Draw(_image, Position, null, Color.White, Rotation, Origin, Scale, SpriteEffects.None, 0.0f);
        }
    }
}
