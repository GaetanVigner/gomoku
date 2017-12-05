using System;

namespace Gorillaz
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Game game = new Game();

                game.start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
