using System;

namespace Testing
{
    static class Program
    {
        static void Main(string[] args)
        {
            using (Testing game = new Testing())
            {
                game.Run();
            }
        }
    }
}

