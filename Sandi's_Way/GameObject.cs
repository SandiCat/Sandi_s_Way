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
    public class GameObject
    {
        //Object's proporties:
        public Vector2 Direction = new Vector2();
        public float Speed = 0;
        public Sprite Sprite; 
        public Dictionary<string, Alarm> Alarms;
        public bool Visable = true;

        
        public GameObject(Vector2 position)
        {
            Alarms = new Dictionary<string, Alarm>();
            try
            {
                Sprite = new Sprite(TextureContainer.DefaultTextures[this.GetType()], position);
            }
            catch { }
        }
        public GameObject()
        {
            Alarms = new Dictionary<string, Alarm>();
            try
            {
                Sprite = new Sprite(TextureContainer.DefaultTextures[this.GetType()], new Vector2(0, 0));
            }
            catch { }
        }

        //EVENTS:
        public virtual void Update()
        {
        } //triggerd when ObjectManager is updated
        public virtual void Draw()
        {
        } //triggerd when ObjectManger's DrawAll() method is called
        
        public virtual void Create(GameObject createdObject)
        { 
        } //triggerd at creation of an object  
        public virtual void Destroy(GameObject destroyedObject)
        { 
        } //triggerd at detruction of an object

        public virtual void Alarm(string name)
        { 
        } //triggerd when an alarm goes off

        public virtual void Collision(List<GameObject> collisions)
        { 
        } //triggerd when ObjectManager has detected an collision

        public virtual void Clicked()
        { 
        } //triggerd when the sprite is clicked
        public virtual void RightClicked()
        {
        } //triggerd when the sprite is right clicked
        public virtual void MouseOver()
        {
        } //trigerd when the mouse is over the sprite (not clicked)

        public virtual void KeyDown(List<Keys> keys)
        { 
        } //triggerd IF a key is pressed (not at the moment its pressed, but during its pressed)
        public virtual void KeyPressed(List<Keys> keys)
        { 
        } //triggerd WHEN a key is pressed (at the exact moment)
        public virtual void KeyReleased(List<Keys> keys)
        { 
        } //triggerd when the key is released

        public virtual void OutsideOfWindow()
        { 
        } //triggerd when object leaves the boundaries of a window and if its already outside
        public virtual void IntersectBoundary()
        { 
        } //triggerd when and if an object touches the boundary

        //ACTIONS:
        public void MoveTowards(Vector2 point, float speed)
        {
            point.Normalize();
            Direction = point;
            Speed = speed;
        }
        public void MoveAngle(float angle, float speed)
        {
            Vector2 up = new Vector2(0, -1);
            Matrix rotationMat = Matrix.CreateRotationZ((float)angle);
            Vector2 direction = Vector2.Transform(up, rotationMat);

            Direction = direction;
            Speed = speed;
        }
        public void StopMovment()
        {
            Direction = Vector2.Zero;
            Speed = 0;
        }
        public void StepTowards(Vector2 point, float distance)
        {
            point.Normalize();
            Sprite.Position += point * distance;
        }
        public void StepAngle(float angle, float distance)
        {
            Vector2 up = new Vector2(0, -1);
            Matrix rotationMat = Matrix.CreateRotationZ((float)angle);
            Vector2 direction = Vector2.Transform(up, rotationMat);

            Sprite.Position += direction * distance;
        }
        public void JumpTo(Vector2 point)
        {
            Sprite.Position = point;
        }

        public void CreateObject(Type type, Vector2 position)
        {
            ObjectManager.Create(type, position);
        }
        public void CreateMovingObject(Type type, Vector2 position, int angle, int speed)
        {
            Vector2 up = new Vector2(0, -1);
            Matrix rotationMat = Matrix.CreateRotationZ((float)angle);
            Vector2 direction = Vector2.Transform(up, rotationMat);

            CreateMovingObject(type, position, direction, speed);
        }
        public void CreateMovingObject(Type type, Vector2 position, Vector2 direction, int speed)
        {
            ObjectManager.Create(type, position);

            GameObject obj = ObjectManager.GetLastCreated();
            obj.Direction = direction;
            obj.Speed = speed;
        }
        public void DestroyObject(GameObject obj)
        {
            ObjectManager.Destroy(obj);
        }

        public void ChangeObject(Type type)
        {
            DestroyObject(this);

            ObjectManager.Create(type, Sprite.Position);

            GameObject newObject = ObjectManager.GetLastCreated();

            newObject.Direction = Direction;
            newObject.Speed = Speed;
        }
            //changes the object in argument to another type, but keeping some properties
        public void ChangeSprite(Sprite sprite)
        {
            Sprite = sprite;
        }
    }
}