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

            //Initialize the testing objects:
            Blue = new BlueObject(new Vector2(0, 0), 0);
            Red = new RedObject(new Vector2(0, 0), 0);
            Yellow = new YellowObject(new Vector2(0, 0), 0);

            //Initialize the collision testing objects:
            Cross = new Cross(new Vector2(0, 0), 0);
            RotatingCross = new RotatingCross(new Vector2(0, 0), 0);
            Dot = new SmallDot(new Vector2(0, 0), 0);

            //Load the testing object sprites:
            Blue.Sprite = TextureContainer.AddTextureAndReturnSprite("BlueSquare", new Vector2(0, 0));
            Red.Sprite = TextureContainer.AddTextureAndReturnSprite("RedSquare", new Vector2(100, 100));
            Yellow.Sprite = TextureContainer.AddTextureAndReturnSprite("YellowSquare", new Vector2(300, 100));

            //Initialize the collision testing objects:
            Cross.Sprite = TextureContainer.AddTextureAndReturnSprite("Cross", new Vector2(300 + 32, 200 + 32));
            RotatingCross.Sprite = new Sprite(TextureContainer.Textures["Cross"], new Vector2(250, 200 + 32));
            Dot.Sprite = TextureContainer.AddTextureAndReturnSprite("SmallSquare", new Vector2(10, 90));

            //Create the testing objects:
            ObjectManager.Create(Red);
            ObjectManager.Create(Yellow);
            ObjectManager.Create(Blue); //blue is created last so its drawn on the top

            //Create the collison testing objects:
            ObjectManager.Create(Cross);
            ObjectManager.Create(RotatingCross);
            ObjectManager.Create(Dot);
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
