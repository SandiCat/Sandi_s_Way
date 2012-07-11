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

        public float Scale = 1.0f;
        public float Rotation = 0.0f;
        public Vector2 Position = new Vector2();
        public Vector2 Origin = new Vector2();

        public Sprite(Texture2D image, Vector2 position, Vector2 origin, float roatation, float scale)
        {
            _image = image;

            Scale = scale;
            Rotation = roatation;
            Position = position;
            Origin = origin;
        }
        public Sprite(Texture2D image, Vector2 position)
        {
            _image = image;

            Position = position;
        }

        public void Draw()
        {
            GameInfo.RefSpriteBatch.Draw(_image, Position, null, Color.White, Rotation, Origin, Scale, SpriteEffects.None, 0.0f);
        }

        public Matrix GetMatrix()
        {
            Matrix matrix =
                Matrix.CreateTranslation(-Origin.X, -Origin.Y, 0)
                * Matrix.CreateRotationZ(Rotation)
                * Matrix.CreateScale(Scale)
                * Matrix.CreateTranslation(Position.X, Position.Y, 0);

            return matrix;
        }
        public Rectangle GetRectangle()
        {
            Rectangle rectangle = new Rectangle((int)Position.X, (int)Position.Y, _image.Width, _image.Height);
            
            return rectangle;
        }
        public Color[] GetColorData()
        {
            Color[] colorData = new Color[_image.Width * _image.Height];
            _image.GetData(colorData);

            return colorData;
        }
    }
}
