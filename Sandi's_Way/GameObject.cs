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
    public abstract class GameObject
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

        #region EVENTS
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
        #endregion

        #region ACTIONS
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
            GameObject obj =  ObjectManager.CreateAndReturn(type, position);
            obj.Direction = direction;
            obj.Speed = speed;
        }

        public GameObject CreateAndReturnObject(Type type, Vector2 position)
        {
            return ObjectManager.CreateAndReturn(type, position);
        }
        public GameObject CreateAndReturnMovingObject(Type type, Vector2 position, float angle, int speed)
        {
            return CreateAndReturnMovingObject(type, position, AngleToDirection(angle), speed);
        }
        public GameObject CreateAndReturnMovingObject(Type type, Vector2 position, Vector2 direction, int speed)
        {
            GameObject obj = ObjectManager.CreateAndReturn(type, position);
            obj.Direction = direction;
            obj.Speed = speed;
            return obj;
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
        #endregion

        #region CHECKS
        public bool IsIntersecting()
        {
            Rectangle screenRectangle = GameInfo.RefDevice.Viewport.Bounds;
            if (screenRectangle.Intersects(Sprite.GetRectangle())
                && !screenRectangle.Contains(Sprite.GetRectangle()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsOutsideWindow()
        {
            Rectangle screenRectangle = GameInfo.RefDevice.Viewport.Bounds;
            if (!screenRectangle.Contains(Sprite.GetRectangle()) && !IsIntersecting())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsColliding(GameObject obj)
        {
            if (this != obj) // dont check collisions with yourself
            {
                if (Sprite.GetRectangle().Intersects(obj.Sprite.GetRectangle())) //check if rectangles collide
                {
                    if (IntersectPixels(Sprite, obj.Sprite)) //check pixel collision
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool IsClicked()
        {
            //The way I'll check mouse clicks is I'll create a small sprite and check collision.
            Texture2D texture = TextureContainer.Textures["Mouse"];

            Vector2 position = new Vector2(ObjectManager.CurrentMouseState.X, ObjectManager.CurrentMouseState.Y);

            //Make a sprite where the mouse is:
            Sprite point = new Sprite(texture, position);
            point.Scale = Sprite.Scale;
            point.Rotation = Sprite.Rotation;

            if (Sprite.GetRectangle().Intersects(point.GetRectangle()))
            {
                if (IntersectPixels(Sprite, point))
                {
                    if (ObjectManager.CurrentMouseState.LeftButton == ButtonState.Pressed
                        && ObjectManager.PreviousMouseState.LeftButton == ButtonState.Released)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool IsRightClicked()
        {
            //The way I'll check mouse clicks is I'll create a small sprite and check collision.
            Texture2D texture = TextureContainer.Textures["Mouse"];

            Vector2 position = new Vector2(ObjectManager.CurrentMouseState.X, ObjectManager.CurrentMouseState.Y);

            //Make a sprite where the mouse is:
            Sprite point = new Sprite(texture, position);
            point.Scale = Sprite.Scale;
            point.Rotation = Sprite.Rotation;

            if (Sprite.GetRectangle().Intersects(point.GetRectangle()))
            {
                if (IntersectPixels(Sprite, point))
                {
                    if (ObjectManager.CurrentMouseState.RightButton == ButtonState.Pressed
                        && ObjectManager.PreviousMouseState.RightButton == ButtonState.Released)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool IsMouseOver()
        {
            //The way I'll check mouse clicks is I'll create a small sprite and check collision.
            Texture2D texture = TextureContainer.Textures["Mouse"];

            Vector2 position = new Vector2(ObjectManager.CurrentMouseState.X, ObjectManager.CurrentMouseState.Y);

            //Make a sprite where the mouse is:
            Sprite point = new Sprite(texture, position);
            point.Scale = Sprite.Scale;
            point.Rotation = Sprite.Rotation;

            if (Sprite.GetRectangle().Intersects(point.GetRectangle()))
            {
                if (IntersectPixels(Sprite, point))
                {
                    if (ObjectManager.CurrentMouseState.LeftButton == ButtonState.Released
                        && ObjectManager.PreviousMouseState.RightButton == ButtonState.Released)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool IsDestroyed()
        {
            if (!ObjectManager.Objects.Contains(this) && !ObjectManager.ObjectsToCreate.Contains(this))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        private Vector2 AngleToDirection(float angle)
        {
            Vector2 up = new Vector2(0, -1);
            Matrix rotationMat = Matrix.CreateRotationZ(angle);
            Vector2 direction = Vector2.Transform(up, rotationMat);

            return direction;
        }
        public static bool IntersectPixels(Sprite spriteA, Sprite spriteB)
        {
            Matrix transformA = spriteA.GetMatrix();
            Matrix transformB = spriteB.GetMatrix();
            int widthA = spriteA.Image.Width;
            int widthB = spriteB.Image.Width;
            int heightA = spriteA.Image.Height;
            int heightB = spriteB.Image.Height;
            Color[] dataA = spriteA.GetColorData();
            Color[] dataB = spriteB.GetColorData();
            // Calculate a matrix which transforms from A's local space into
            // world space and then into B's local space
            Matrix transformAToB = transformA * Matrix.Invert(transformB);

            // When a point moves in A's local space, it moves in B's local space with a
            // fixed direction and distance proportional to the movement in A.
            // This algorithm steps through A one pixel at a time along A's X and Y axes
            // Calculate the analogous steps in B:
            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
            Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

            // Calculate the top left corner of A in B's local space
            // This variable will be reused to keep track of the start of each row
            Vector2 yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

            // For each row of pixels in A
            for (int yA = 0; yA < heightA; yA++)
            {
                // Start at the beginning of the row
                Vector2 posInB = yPosInB;

                // For each pixel in this row
                for (int xA = 0; xA < widthA; xA++)
                {
                    // Round to the nearest pixel
                    int xB = (int)Math.Round(posInB.X);
                    int yB = (int)Math.Round(posInB.Y);

                    // If the pixel lies within the bounds of B
                    if (0 <= xB && xB < widthB &&
                        0 <= yB && yB < heightB)
                    {
                        // Get the colors of the overlapping pixels
                        Color colorA = dataA[xA + yA * widthA];
                        Color colorB = dataB[xB + yB * widthB];

                        // If both pixels are not completely transparent,
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            // then an intersection has been found
                            return true;
                        }
                    }

                    // Move to the next pixel in the row
                    posInB += stepX;
                }

                // Move to the next row
                yPosInB += stepY;
            }

            // No intersection found
            return false;
        } //this code was taken from a microsoft app hub turorial
    }
}