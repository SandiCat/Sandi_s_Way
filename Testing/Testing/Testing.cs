using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Testing : Microsoft.Xna.Framework.Game
    {
        //Basic game info:
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;

        //Testing objects:
        public BlueObject Blue;
        public RedObject Red;
        public YellowObject Yellow;

        public Testing()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //Initialize the game:
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Testing the Sandi's Way wrapper";

            //Initialize the static classes:
            ObjectManager.Objects = new List<GameObject>();
            SpriteContainer.Sprites = new Dictionary<string, Sprite>();

            //Initialize the testing objects:
            Blue = new BlueObject(new Vector2(0, 0), new Vector2(0, 0), 0);
            Red = new RedObject(new Vector2(64, 0), new Vector2(0, 0), 0);
            Yellow = new YellowObject(new Vector2(64 + 64, 0), new Vector2(0, 0), 0);
            ObjectManager.Create(Blue);
            ObjectManager.Create(Red);
            ObjectManager.Create(Yellow);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;

            //Initialize the GameInfo:
            GameInfo.RefSpriteBatch = spriteBatch;
            GameInfo.RefDevice = device;
            GameInfo.RefDeviceManager = graphics;
            GameInfo.RefContent = Content;

            //Load the testing object sprites:
            SpriteContainer.AddSprite("BlueSquare");
            SpriteContainer.AddSprite("RedSquare");
            SpriteContainer.AddSprite("YellowSquare");
            Blue.Sprite = SpriteContainer.Sprites["BlueSquare"];
            Red.Sprite = SpriteContainer.Sprites["RedSquare"];
            Yellow.Sprite = SpriteContainer.Sprites["YellowSquare"];
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            ObjectManager.UpdateAll();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); 
            ObjectManager.DrawAll();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Texture2D ColoredSquareTexture(Color color, int a)
        {
            Color[] colorArray = new Color[a * a];
            for (int i = 0; i < (a * a); i++) //fill colorArray with colors
            {
                colorArray[i] = color;
            }

            Texture2D returnTexture = new Texture2D(device, a, a);
            returnTexture.SetData(colorArray);

            return returnTexture;
        }
    }
}
