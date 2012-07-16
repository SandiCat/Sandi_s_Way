using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugging
{
    public class DebugVariable //things that always stay in the console, and you can change them
    {
        public string Text;

        public DebugVariable(string text)
        {
            Text = text;
        }
        public DebugVariable()
        {
        }

        public void ChangeText(string text)
        {
            Text = text;
        }
    }
}
