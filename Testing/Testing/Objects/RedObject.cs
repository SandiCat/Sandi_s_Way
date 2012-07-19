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

using Sandi_s_Way;

namespace Testing
{
    public class RedObject : GameObject
    {
        public RedObject(Vector2 position)
            : base(position)
        {
        }
        public RedObject()
            : base()
        {
        }

        public override void Create(GameObject createdObject)
        {
            if (createdObject == this)
            {
                Testing.Console.WriteLine("A red object has been created");
                
                Alarms.Add("testAlarm", new Alarm());

                Alarms["testAlarm"].Restart(300);
            }
        }
        public override void Update()
        {
        }
        public override void Alarm(string name)
        {
            if (name == "testAlarm")
            {
                Testing.Console.WriteLine("Alarm \"testAlarm\" has gone of");
            }
        }
    }
}