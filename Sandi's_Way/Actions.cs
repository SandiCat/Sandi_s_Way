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
    public static class Actions //a big ol' colection of actions
    {
        public static void MoveTo(GameObject obj)
        {
        }
        public static void MoveFixedDirection(GameObject obj, Directions direction, float speed)
        {
            obj.Speed = speed;

            switch (direction)
            {
                case Directions.Up:
                    obj.Direction = new Vector2(0, -1);
                    break;
                case Directions.Down:
                    obj.Direction = new Vector2(0, 1);
                    break;
                case Directions.Left:
                    obj.Direction = new Vector2(-1, 0);
                    break;
                case Directions.Right:
                    obj.Direction = new Vector2(1, 0);
                    break;

                case Directions.UpLeft:
                    obj.Direction = new Vector2(-1, -1);
                    break;
                case Directions.UpRight:
                    obj.Direction = new Vector2(1, -1);
                    break;
                case Directions.DownLeft:
                    obj.Direction = new Vector2(-1, 1);
                    break;
                case Directions.DownRight:
                    obj.Direction = new Vector2(1, 1);
                    break;
                case Directions.None:
                    obj.Direction = new Vector2(0, 0);
                    break;
            }
        }
        public static void StepFixedDirection(GameObject obj, Directions direction)
        {
            switch (direction)
            {
                case Directions.Up:
                    obj.Sprite.Position += new Vector2(0, -1);
                    break;
                case Directions.Down:
                    obj.Sprite.Position += new Vector2(0, 1);
                    break;
                case Directions.Left:
                    obj.Sprite.Position += new Vector2(-1, 0);
                    break;
                case Directions.Right:
                    obj.Sprite.Position += new Vector2(1, 0);
                    break;

                case Directions.UpLeft:
                    obj.Sprite.Position += new Vector2(-1, -1);
                    break;
                case Directions.UpRight:
                    obj.Sprite.Position += new Vector2(1, -1);
                    break;
                case Directions.DownLeft:
                    obj.Sprite.Position += new Vector2(-1, 1);
                    break;
                case Directions.DownRight:
                    obj.Sprite.Position += new Vector2(1, 1);
                    break;
                case Directions.None:
                    obj.Sprite.Position += new Vector2(0, 0);
                    break;
            }
        }
        public static void MoveDirection(GameObject obj)
        {
        }

        public static void JumpTo(GameObject obj)
        {
        }

        public static void CreateObject(GameObject obj)
        {
        }
        public static void CreateMovingObject(GameObject obj)
        {
        }
        public static void DestroyObject(GameObject obj)
        {
        }

        public static void ChangeObject(GameObject obj)
        {
        } //changes the object in argument to another type, but keeping some properties
        public static void ChangeSprite(GameObject obj)
        {
        }

        public static void PlaySound(GameObject obj)
        {
        }

        public static void DrawSprite(GameObject obj)
        {
        }
    }
}
