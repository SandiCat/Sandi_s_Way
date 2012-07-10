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
        static public BlueObject Blue;
        static public RedObject Red;
        static public YellowObject Yellow;

        //The debug console:
        public static DebuggConsole Console;
        SpriteFont DebuggConsoleFont;

        public Testing()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;

            //Initialize the game:
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Testing the Sandi's Way wrapper";

            //Initialize the static classes:
            ObjectManager.Initialize();
            TextureContainer.Textures = new Dictionary<string, Texture2D>();

            //Initialize the debug console:
            Console = new DebuggConsole(spriteBatch, new Vector2(0, 0));
            DebuggConsoleFont = Content.Load<SpriteFont>("DebuggConsoleFont");
            Console.Font = DebuggConsoleFont;

            //Initialize the testing objects:
            Blue = new BlueObject(new Vector2(0, 0), 0);
            Red = new RedObject(new Vector2(0, 0), 0);
            Yellow = new YellowObject(new Vector2(0, 0), 0);
            ObjectManager.Create(Blue);
            ObjectManager.Create(Red);
            ObjectManager.Create(Yellow);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Initialize the GameInfo:
            GameInfo.RefSpriteBatch = spriteBatch;
            GameInfo.RefDevice = device;
            GameInfo.RefDeviceManager = graphics;
            GameInfo.RefContent = Content;

            //Load the testing object sprites:
            Blue.Sprite = TextureContainer.AddTextureAndReturnSprite("BlueSquare", new Vector2(0, 0));
            Red.Sprite = TextureContainer.AddTextureAndReturnSprite("RedSquare", new Vector2(64, 0));
            Yellow.Sprite = TextureContainer.AddTextureAndReturnSprite("YellowSquare", new Vector2(64 + 64, 0));
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            ObjectManager.UpdateAll();
            Console.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); 
            ObjectManager.DrawAll();
            Console.WriteConsole();
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
