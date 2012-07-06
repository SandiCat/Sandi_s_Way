using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testing
{
    public class DebuggVariable //things that always stay in the console, and you can change them
    {
        public string Text;

        public DebuggVariable(string text)
        {
            Text = text;
        }
        public DebuggVariable()
        {
        }

        public void ChangeText(string text)
        {
            Text = text;
        }
    }
}
