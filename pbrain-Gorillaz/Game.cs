using System;
using System.Collections.Generic;
using System.Text;

namespace Gorillaz
{
    class Game
    {
        private IA _IA;
        private int _end;
        Board board = new Board();
        Infos infos = new Infos();
        IOinterface iointerface = new IOinterface();

        public int Start()
        {
            _IA = new IA();
            _end = 0;
            Pos pos = new Pos();

            while (_end != 84 && _end != 42)
            {
                Console.WriteLine("start tests");
                _end = iointerface.GetInput(ref board, ref infos);
                if (_end == 1)
                {
                    pos = _IA.BrainTurn(ref board, ref iointerface);
                    iointerface.SetTurn(pos);
                    if (board.PlaceARock(1, pos.Y, pos.X) == 1)
                        return (84);

                }
            }
            return _end;
        }
    }
}
