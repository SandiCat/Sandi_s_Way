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
        public bool Solid = true; //If this is false collisions will not be checked with this oject

        
        public GameObject(Vector2 position)
        {
            Alarms = new Dictionary<string, Alarm>();
            try
            {
                Sprite = new Sprite(TextureContainer.DefaultTextures[this.GetType()], position);
            }
            catch
            {
                Sprite = new Sprite(TextureContainer.EmptyTexture, position);
            }
        }
        public GameObject()
        {
            Alarms = new Dictionary<string, Alarm>();
            try
            {
                Sprite = new Sprite(TextureContainer.DefaultTextures[this.GetType()], new Vector2(0, 0));
            }
            catch
            {
                Sprite = new Sprite(TextureContainer.EmptyTexture, new Vector2(0, 0));
            }
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
            Direction = AngleToDirection(angle);
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
            Sprite.Position += AngleToDirection(angle) * distance;
        }
        public void JumpTo(Vector2 point)
        {
            Sprite.Position = point;
        }

        public void CreateObject(Type type, Vector2 position)
        {
            ObjectManager.Create(type, position);
        }
        public void CreateMovingObject(Type type, Vector2 position, float angle, int speed)
        {
            CreateMovingObject(type, position, AngleToDirection(angle), speed);
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
            newObject.Sprite.Origin = Sprite.Origin;
        }
            //changes the object to another type, but keeping some properties
        public void ChangeSpriteTexture(Texture2D texture)
        {
            Sprite.Image = texture;
        }
        public void ChangeSpriteTexture(string filename)
        {
            ChangeSpriteTexture(TextureContainer.Textures[filename]);
        }

        private Vector2 AngleToDirection(float angle)
        {
            Vector2 up = new Vector2(0, -1);
            Matrix rotationMat = Matrix.CreateRotationZ(angle);
            Vector2 direction = Vector2.Transform(up, rotationMat);

            return direction;
        }
    }
}