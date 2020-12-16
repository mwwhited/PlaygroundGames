using System;

namespace ScrollerEngine
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ScrollerGame game = new ScrollerGame())
            {
                game.Run();
            }
        }
    }
}

