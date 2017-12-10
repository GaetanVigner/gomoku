using System;

namespace Gorillaz
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                Game game = new Game();

                return (game.Start());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey(true);
                return (84);
            }
        }
    }
}
