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
        private class _line
        {
            public string Text;
            public static int DefaultTime = 90;
            public int Time;
            private DebuggConsole _console;

            public _line(string text, DebuggConsole console)
            {
                Text = text;
                Time = DefaultTime;
                _console = console;
            }

            public void Write(float yPosition)
            {
                _console._spriteBatch.DrawString(_console.Font, Text, new Vector2(_console.Position.X, yPosition), Color.Yellow);
            }
            public void Update()
            {
                Time--;
            }

            public bool ShouldIRemove()
            {
                if (Time == 0)
                    return true;
                else
                    return false;
            }
        }

        public Vector2 Position;
        public SpriteFont Font;
        public const int FontSize = 8;
        SpriteBatch _spriteBatch;

        private List<_line> _lines = new List<_line>();
        public List<DebuggVariable> Variables = new List<DebuggVariable>();

        public DebuggConsole(SpriteBatch spriteBatch, Vector2 position)
        {
            Position = position;
            _spriteBatch = spriteBatch;
        }
        
        public void WriteLine(string text)
        {
            _lines.Add(new _line(text, this));
        }
        
        public void WriteConsole()
        {
            int lastPosition = (int)Position.Y;

            //Write shiz to screen one above another:
            foreach (var variable in Variables)
            {
                _spriteBatch.DrawString(Font, variable.Text, new Vector2(Position.X, lastPosition), Color.Yellow);
                lastPosition += FontSize;
            }
            foreach (var line in _lines)
            {
                line.Write(lastPosition);
                lastPosition += FontSize;
            }
        }
        public void Update()
        {
            List<_line> linesToRemove = new List<_line>();

            foreach (var line in _lines)
            {
                line.Update();

                if (line.ShouldIRemove())
                    linesToRemove.Add(line);
            }

            foreach (var line in linesToRemove)
            {
                _lines.Remove(line);
            }
        }
    }
}
