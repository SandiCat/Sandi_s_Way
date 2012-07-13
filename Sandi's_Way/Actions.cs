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
        public static void MoveTowards(GameObject obj, Vector2 point, float speed)
        {
            point.Normalize();
            obj.Direction = point;
            obj.Speed = speed;
        }      
        public static void MoveAngle(GameObject obj, float angle, float speed)
        {
            Vector2 up = new Vector2(0, -1);
            Matrix rotationMat = Matrix.CreateRotationZ((float)angle);
            Vector2 direction = Vector2.Transform(up, rotationMat);

            obj.Direction = direction;
            obj.Speed = speed;
        }
        public static void StopMovment(GameObject obj)
        {
            obj.Direction = Vector2.Zero;
            obj.Speed = 0;
        }
        public static void StepTowards(GameObject obj, Vector2 point, float distance)
        {
            point.Normalize();
            obj.Sprite.Position += point * distance;
        }
        public static void StepAngle(GameObject obj, float angle, float distance)
        {
            if (angle == null) return;

            Vector2 up = new Vector2(0, -1);
            Matrix rotationMat = Matrix.CreateRotationZ((float)angle);
            Vector2 direction = Vector2.Transform(up, rotationMat);

            obj.Sprite.Position += direction * distance;
        } 
        public static void JumpTo(GameObject obj, Vector2 point)
        {
            obj.Sprite.Position = point;
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
