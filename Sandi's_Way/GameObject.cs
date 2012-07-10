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
    abstract public class GameObject
    {
        //Object's proporties:
        public Vector2 Position = new Vector2();
        public Vector2 Direction = new Vector2();
        public float Speed = 0;
        public readonly Vector2 StartingPosition = new Vector2();
        public Sprite Sprite; 
        public List<Alarm> Alarms;
        public bool Visable = true;

        //The constructor:
        public GameObject(Vector2 position, Vector2 direction, float speed)
        {
            Position = position;
            Direction = direction;
            Speed = speed;
            StartingPosition = position;
            Alarms = new List<Alarm>();
        }

        //EVENTS: (they are all virtual instead of abstarct because you dont have to implement all of them)
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

        public virtual void Alarm()
        { 
        } //triggerd when an alarm goes off

        public virtual void Collision()
        { 
        } //triggerd when ObjectManager has detected an collision

        public virtual void Clicked()
        { 
        } //triggerd when the sprite is clicked
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
    }
}