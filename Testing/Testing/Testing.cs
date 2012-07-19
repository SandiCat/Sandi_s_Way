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
using Debugging;

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

        //Collision testing objects:
        static public Cross Cross;
        static public RotatingCross RotatingCross;
        static public SmallDot Dot;

        //The debug console:
        public static DebugConsole Console;
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
            IsMouseVisible = true;

            //Initialize the static classes:
            ObjectManager.Initialize();
            TextureContainer.Textures = new Dictionary<string, Texture2D>();
            TextureContainer.DefaultTextures = new Dictionary<Type, Texture2D>();

            //Initialize the debug console:
            Console = new DebugConsole(spriteBatch, new Vector2(0, 0));
            DebuggConsoleFont = Content.Load<SpriteFont>("DebuggConsoleFont");
            Console.Font = DebuggConsoleFont;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Initialize the GameInfo:
            GameInfo.RefSpriteBatch = spriteBatch;
            GameInfo.RefDevice = device;
            GameInfo.RefDeviceManager = graphics;
            GameInfo.RefContent = Content;
            GameInfo.RefConsole = Console;

            //Initialize default sprites:
            TextureContainer.DefaultTextures[typeof(BlueObject)] = TextureContainer.AddTextureAndReturn("BlueSquare");
            TextureContainer.DefaultTextures[typeof(RedObject)] = TextureContainer.AddTextureAndReturn("RedSquare");
            TextureContainer.DefaultTextures[typeof(YellowObject)] = TextureContainer.AddTextureAndReturn("YellowSquare");
            TextureContainer.DefaultTextures[typeof(Cross)] = TextureContainer.AddTextureAndReturn("Cross");
            TextureContainer.DefaultTextures[typeof(RotatingCross)] = TextureContainer.Textures["Cross"]; //it has the same texture as normal cross
            TextureContainer.DefaultTextures[typeof(SmallDot)] = TextureContainer.AddTextureAndReturn("SmallSquare");

            //Create the objects:
            ObjectManager.InstantCreate(typeof(BlueObject), new Vector2(0, 0));
            ObjectManager.InstantCreate(typeof(RedObject), new Vector2(300, 300));
            ObjectManager.InstantCreate(typeof(YellowObject), new Vector2(64 + 64 + 32, 32));
            ObjectManager.InstantCreate(typeof(Cross), new Vector2(64 + 32, 100 + 32));
            ObjectManager.InstantCreate(typeof(RotatingCross), new Vector2(64 + 64 + 32, 100 + 32));
            ObjectManager.InstantCreate(typeof(SmallDot), new Vector2(0, 64));
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
