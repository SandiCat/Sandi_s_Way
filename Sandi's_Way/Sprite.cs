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
    public class Sprite //keeps an image and draws it. (this doesn't handle the position of the image)
    {
        private Texture2D _image;

        public Sprite(Texture2D image)
        {
            _image = image;
        }

        public void Draw(Vector2 position)
        {
            GameInfo.RefSpriteBatch.Draw(_image, position, Color.White);
        }
    }
}
