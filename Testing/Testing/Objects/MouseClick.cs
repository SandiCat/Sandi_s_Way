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
    public class MouseClick : GameObject
    {
        public MouseClick(Vector2 position)
            : base(position)
        {
            Solid = false;
        }
        public MouseClick()
            : base()
        {
            Solid = false;
        }

        private MouseState previousMouse;

        public override void Create(GameObject createdObject)
        {
            if (createdObject == this)
            {
                previousMouse = Mouse.GetState();
            }
        }

        public override void Update()
        {
            MouseState mouse = Mouse.GetState();

            //if (mouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton != ButtonState.Pressed)
            //{
            //    CreateObject(typeof(YellowObject), new Vector2(mouse.X, mouse.Y));
            //}

            //if (mouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton != ButtonState.Pressed)
            //{
            //    CreateMovingObject(typeof(YellowObject), new Vector2(mouse.X, mouse.Y), Directions.Left, 3);
            //}

            previousMouse = Mouse.GetState();
        }
    }
}