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
    //creates, destroys, updates, drawes and moves gameobjects, and calls their events
    //this is static because you only need one instance
    public static class ObjectManager 
    {
        static public List<GameObject> Objects;
        static public List<GameObject> ObjectsToCreate;
        static public List<GameObject> ObjectsToDestroy; 
            //This list serve as a to-do list for the manager, he destroys and creates objects from the Objects list acorrding to this two lists
            //After the foreach has finished looping through Objects, because you can modify list while looping through them

        //Keyboard state variables:
        static private List<Keys> previousState;
        static private List<Keys> currentState;
        static private List<Keys> pressedKeys;
        static private List<Keys> releasedKeys;

        public static void Initialize() //since this is a static object
        {
            Objects = new List<GameObject>();
            ObjectsToCreate = new List<GameObject>();
            ObjectsToDestroy = new List<GameObject>();

            previousState = new List<Keys>();
            currentState = new List<Keys>();
            pressedKeys = new List<Keys>();
            releasedKeys = new List<Keys>();
        }

        static public void Create(Type type, Vector2 position)
        {
            GameObject obj = (GameObject)Activator.CreateInstance(type);
            obj.Sprite.Position = position;
            ObjectsToCreate.Add(obj);

            //Call the create event:
            foreach (var i in Objects) //I used 'i' here instead of 'obj' because 'obj' is taken
            {
                i.Create(obj);
            }
        }
        static public void InstantCreate(Type type, Vector2 position)
        {
            GameObject obj = (GameObject)Activator.CreateInstance(type);
            obj.Sprite.Position = position;
            Objects.Add(obj);

            //Call the create event:
            foreach (var i in Objects) //I used 'i' here instead of 'obj' because 'obj' is taken
            {
                i.Create(obj);
            }
        }  //Creates without using ObjectsToCreat. Use wisely.
        static public void Destroy(GameObject obj)
        {
            ObjectsToDestroy.Add(obj);

            //Call the destroy event:
            foreach (var i in Objects) //I used 'i' here instead of 'obj' because 'obj' is taken
            {
                i.Destroy(obj);
            }           
        }
        static public void InstantDestroy(GameObject obj)
        {
            Objects.Remove(obj);

            //Call the destroy event:
            foreach (var i in Objects) //I used 'i' here instead of 'obj' because 'obj' is taken
            {
                i.Destroy(obj);
            }
        } //Destroys without using ObjectsToDestroy. Use wisely.

        static public List<GameObject> Get(Type type)
        {
            List<GameObject> list = new List<GameObject>();

            foreach (var obj in Objects)
            {
                if (obj.GetType() == type)
                {
                    list.Add(obj);
                }
            }

            return list;
        }

        static public void UpdateAll()
        {    
            foreach (var obj in Objects)
            {
                //Move the objects:
                obj.Sprite.Position += obj.Direction * obj.Speed;
            
                //Call the update event:
                obj.Update();

                //Call the "Outside of window" event:
                Rectangle screenRectangle = GameInfo.RefDevice.Viewport.Bounds;
                if (!screenRectangle.Intersects(obj.Sprite.GetRectangle()))
                {
                    obj.OutsideOfWindow();
                }

                //Call the "Intersect boundary" event:
                if (screenRectangle.Intersects(obj.Sprite.GetRectangle())
                    && !screenRectangle.Contains(obj.Sprite.GetRectangle()))
                {
                    obj.IntersectBoundary();
                }

                //Update alarms and call their events
                foreach (var alarm in obj.Alarms)
                {
                    if (alarm.Value.IsDone()) obj.Alarm(alarm.Key);
                }
            }

            //Manage input:
            ManageKeyboard();
            ManageCollisions();
            ManageMouse();

            //Create objects from the ObjectsToCreat list
            foreach (var obj in ObjectsToCreate)
            {
                Objects.Add(obj);
            }
            ObjectsToCreate.Clear();

            //Destroy objects from the ObjectsToDestroy list
            foreach (var obj in ObjectsToDestroy)
            {
                Objects.Remove(obj);
            }
            ObjectsToDestroy.Clear();
        }
        static public void DrawAll()
        {
            foreach (var obj in Objects)
            {
                if (obj.Visable)
                {
                    obj.Sprite.Draw();
                }           

                //Call the draw event:
                obj.Draw();
            }
        }
        
        static private void ManageKeyboard()
        {
            currentState = Keyboard.GetState().GetPressedKeys().ToList();

            //Clear lists:
            pressedKeys.Clear();
            releasedKeys.Clear();

            //Get pressed keys:
            foreach (var key in currentState)
            {
                if (!previousState.Contains(key))
                    pressedKeys.Add(key);
            }
            
            //Get released keys:
            foreach (var key in previousState)
            {
                if (!currentState.Contains(key))
                    releasedKeys.Add(key);
            }

            //Call events:
            foreach (var obj in Objects)
            {
                obj.KeyDown(currentState);
                obj.KeyPressed(pressedKeys);
                obj.KeyReleased(releasedKeys);
            }            

            previousState = Keyboard.GetState().GetPressedKeys().ToList(); 
        }
        static private void ManageCollisions()
        {
            foreach (var obj1 in Objects)
            {
                List<GameObject> collisions = new List<GameObject>();

                foreach (var obj2 in Objects)
                {
                    if (obj1 != obj2) // you dont wanna check collisions with your self! That would be stupid!
                    {
                        if (obj1.Sprite.GetRectangle().Intersects(obj2.Sprite.GetRectangle())) //check if rectangles collide
                        {
                            if (IntersectPixels(obj1.Sprite, obj2.Sprite)) //check pixel collision
                            {
                                collisions.Add(obj2);
                            }
                        }
                    }
                }
                
                obj1.Collision(collisions);
            }
        }
        static private void ManageMouse()
        {
            //Get mouse info:
            MouseState mouse = Mouse.GetState();
            Vector2 position = new Vector2(mouse.X, mouse.Y);
            
            //The way I'll check mouse clicks is I'll create a small sprite and check collision.

            //Make a little texture for the mouse sprite (this wont be drawn):
            Texture2D texture = new Texture2D(GameInfo.RefDevice, 1, 1);
            texture.SetData(new Color[] { Color.Black });

            //Make a sprite where the mouse is:
            Sprite point = new Sprite(texture, position);  

            //Check collisions with sprite:
            foreach (var obj in Objects)
            {
                point.Scale = obj.Sprite.Scale;
                point.Rotation = obj.Sprite.Rotation;

                if (obj.Sprite.GetRectangle().Intersects(point.GetRectangle()))
                {
                    GameInfo.RefConsole.UniqueLine("I entred rectangle check");

                    if (IntersectPixels(obj.Sprite, point))
                    {
                        GameInfo.RefConsole.UniqueLine("I entred per-pixel check");

                        if (mouse.LeftButton == ButtonState.Pressed)
                        {
                            obj.Clicked();
                        }
                        else if (mouse.RightButton == ButtonState.Pressed)
                        {
                            obj.RightClicked();
                        }
                        else
                        {
                            obj.MouseOver();
                        }
                    }
                }
            }
        }
        
        private static bool IntersectPixels(Sprite spriteA, Sprite spriteB)
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

        public static GameObject GetLastCreated()
        {
            return Objects.Last();
        }
    } 
}

//HOW TO DO OBJECT STORING:
