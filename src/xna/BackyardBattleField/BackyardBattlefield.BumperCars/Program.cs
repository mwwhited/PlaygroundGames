using System;

namespace BackyardBattlefield.BumperCars
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (BumperCarsMiniGame game = new BumperCarsMiniGame())
            {
                game.Run();
            }
        }
    }
}

