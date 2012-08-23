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
    public static class Grid
    {
        public void Initialize(int squareSide)
        {
            _squareSide = squareSide;
        }

        int _squareSide;

        public Vector2 GetXYPosition(Vector2 XY)
        {
            return new Vector2(XY.X * _squareSide, XY.Y * _squareSide);
        }
        public Vector2 GetXYFromPosition(Vector2 position)
        {
            float x = (float)Math.Floor(position.X / _squareSide);
            float y = (float)Math.Floor(position.Y / _squareSide);

            return new Vector2(x, y);
        }
    }
}
