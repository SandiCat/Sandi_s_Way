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

namespace Testing
{
    public class DebuggConsole
    {
        public Vector2 Position;
        public SpriteFont Font;
        SpriteBatch _spriteBatch;

        public List<string> Lines = new List<string>();
        public List<DebuggVariable> Variables = new List<DebuggVariable>();

        public DebuggConsole(SpriteBatch spriteBatch, Vector2 position)
        {
            Position = position;
            _spriteBatch = spriteBatch;
        }
        
        public void WriteLine(string text)
        {
            Lines.Add(text);
        }
        private void writeToScreen(string text, int xPosition)
        {
            _spriteBatch.DrawString(Font, text, new Vector2(Position.X, xPosition), Color.Yellow);
        }
        public void WriteConsole()
        {
            int lastPosition = (int)Position.X;

            //Write shiz to screen one above another:
            foreach (var variable in Variables)
            {
                writeToScreen(variable.Text, lastPosition);
                lastPosition += 8;
            }
            foreach (var line in Lines)
            {
                writeToScreen(line, lastPosition);
                lastPosition += 8;
            }

            ////Restart lines:
            //Lines.RemoveRange(0, Lines.Count);
        }
    }
}
