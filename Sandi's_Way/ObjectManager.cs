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
        static private List<Keys> previousKeyboardState;
        static private List<Keys> currentKeyboardState;
        static private List<Keys> pressedKeys;
        static private List<Keys> releasedKeys;

        //Mouse state variables:
        static public MouseState PreviousMouseState;
        static public MouseState CurrentMouseState;
        static public Texture2D MouseTexture;

        public static void Initialize() //since this is a static object
        {
            Objects = new List<GameObject>();
            ObjectsToCreate = new List<GameObject>();
            ObjectsToDestroy = new List<GameObject>();

            previousKeyboardState = new List<Keys>();
            currentKeyboardState = new List<Keys>();
            pressedKeys = new List<Keys>();
            releasedKeys = new List<Keys>();

            PreviousMouseState = new MouseState();
            CurrentMouseState = new MouseState();
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

            obj.Create(obj); //the loop above won't do this since it loops trough "Objects" and "obj" isnt in that list
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
                if (obj.IsOutsideWindow())
                {
                    obj.OutsideOfWindow();
                }

                //Call the "Intersect boundary" event:
                if (obj.IsIntersecting())
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
            currentKeyboardState = Keyboard.GetState().GetPressedKeys().ToList();

            //Clear lists:
            pressedKeys.Clear();
            releasedKeys.Clear();

            //Get pressed keys:
            foreach (var key in currentKeyboardState)
            {
                if (!previousKeyboardState.Contains(key))
                    pressedKeys.Add(key);
            }
            
            //Get released keys:
            foreach (var key in previousKeyboardState)
            {
                if (!currentKeyboardState.Contains(key))
                    releasedKeys.Add(key);
            }

            //Call events:
            foreach (var obj in Objects)
            {
                obj.KeyDown(currentKeyboardState);
                obj.KeyPressed(pressedKeys);
                obj.KeyReleased(releasedKeys);
            }            

            previousKeyboardState = Keyboard.GetState().GetPressedKeys().ToList(); 
        }
        static private void ManageCollisions()
        {
            List<GameObject> solidObjects = new List<GameObject>();
            foreach (var obj in Objects)
            {
                if (obj.Solid) solidObjects.Add(obj);
            }

            foreach (var obj1 in solidObjects)
            {
                List<GameObject> collisions = new List<GameObject>();

                foreach (var obj2 in solidObjects)
                {
                    if (obj1.IsColliding(obj2))
                    {
                        collisions.Add(obj2);
                    }
                }
                
                obj1.Collision(collisions);
            }
        }
        static private void ManageMouse()
        {
            //Get mouse info:
            CurrentMouseState = Mouse.GetState();

            foreach (var obj in Objects)
            {
                if (obj.IsClicked())
                {
                    obj.Clicked();
                }
                else if (obj.IsRightClicked())
                {
                    obj.RightClicked();
                }
                else if (obj.IsMouseOver())
                {
                    obj.MouseOver();
                }
            }

            PreviousMouseState = Mouse.GetState();
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
            return ObjectsToCreate.Last();
        }
        public static void Clear() //Cleans this object out - destroys all objects
        {
            Objects.Clear();
            ObjectsToCreate.Clear();
            ObjectsToDestroy.Clear();
        }
    } 
}

//HOW TO DO OBJECT STORING:
