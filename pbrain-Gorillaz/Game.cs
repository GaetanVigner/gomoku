using System;
using System.Collections.Generic;
using System.Text;

namespace Gorillaz
{
    class Game
    {
        //private IA _IA;
        private int _end;
        Board board = new Board();
        Infos infos = new Infos();
        IOinterface iointerface = new IOinterface();

        public void start()
        {
            //_IA = new IA();
            _end = 0;
            

            while (_end == 0)
            {
                Console.WriteLine("start tests");
                iointerface.GetInput(board, infos);
            }
        }
    }
}
