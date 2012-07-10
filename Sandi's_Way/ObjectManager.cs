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
    //creates, destroys, updates, drawes and moves gameobjects, and triggers their events
    //this is static because you only need one instance
    public static class ObjectManager 
    {
        static public List<GameObject> Objects;

        //Keyboard state variables:
        static private List<Keys> previousState;
        static private List<Keys> currentState;
        static private List<Keys> pressedKeys;
        static private List<Keys> releasedKeys;

        public static void Initialize() //since this is a static object
        {
            Objects = new List<GameObject>();

            previousState = new List<Keys>();
            currentState = new List<Keys>();
            pressedKeys = new List<Keys>();
            releasedKeys = new List<Keys>();
        }

        static public void Create(GameObject obj)
        {
            Objects.Add(obj);

            //Call the create event:
            foreach (var i in Objects) //I used 'i' here instead of 'obj' because 'obj' is taken
            {
                i.Create(obj);
            }
        }
        static public void Destroy(GameObject obj)
        {
            Objects.Remove(obj);

            //Call the destroy event:
            foreach (var i in Objects) //I used 'i' here instead of 'obj' because 'obj' is taken
            {
                i.Destroy(obj);
            }
        }

        static public void UpdateAll()
        {    
            foreach (var obj in Objects)
            {
                //Move the objects:
                obj.Position += obj.Direction * obj.Speed;
            
                //Call the update event:
                obj.Update();
            }

            //Check keyboard:
            ManageKeyboard();
        }
        static public void DrawAll()
        {
            foreach (var obj in Objects)
            {
                if (obj.Visable)
                {
                    obj.Sprite.Draw(obj.Position);
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
                    pressedKeys.Add(key);
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
    } 
}

//HOW TO DO OBJECT STORING:
/*
 * So, there are 2 types of game objects. 
 * 
 * First are the ones that are there from the begining. 
 * You want to have an easy way to acces this objects. 
 * You want them to have an indentificator. Not just an element in the list of objects.
 * So, what you do, is you define them inside the game class. 
 * Then, you add them to the object manager by using the Create() method.
 * Now you can simply use them by the name you assinged to them in the game class, but they
 * are still a part of the GameManager.
 * 
 * Second type are the ones that are created during the game. Usually by another object.
 * For example: bullets, particles, explosions, random pick ups and so on.
 * The thing with this objects is that you don't need to acces them after they are created. 
 * You can create them by just doing Create(new TypeOfObject());
 * If you really need to acces them, you can just have a reference ready for them in the object, and leater fill it with the object.
 * Or you could make the object, than just Create() and Destroy() it whenever you need.
 * 
 * Ofcourse, you can do this any way you want, but I find this is a quite descent way.
 */