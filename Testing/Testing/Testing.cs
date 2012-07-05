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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;

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

            //Initialize the static classes:
            ObjectManager.Objects = new List<GameObject>();
            SpriteContainer.Sprites = new Dictionary<string, Sprite>();
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
