﻿using System;
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

        /// <summary>
        /// unit test for the class Board
        /// </summary>
        public void test_unit()
        {
            Board board_test = new Board();
            List<Pos> tab_test;
            board_test.SetBoardSize(20);
            Console.WriteLine("set the boardsize to 20");
            board_test.PlaceARock(2, 2, 1);
            Console.WriteLine("Placed a rock color 1 in 2,2");
            board_test.PlaceARock(2, 5, 2);
            Console.WriteLine("Placed a rock color 2 in 2,5");
            board_test.PlaceARock(0, 0, 1);
            Console.WriteLine("Placed a rock color 1 in 0,0");
            board_test.PlaceARock(19, 19, 2);
            Console.WriteLine("Placed a rock color 2 in 19,19");
            tab_test = board_test.GetPlayablePos();
            Console.WriteLine("got a tab of playable pos");
            foreach(Pos s in tab_test)
            {
                Console.WriteLine(s.X + " " + s.Y);
            }
            Console.WriteLine("displayed playable pos");
        }
        public int Start()
        {
#if DEBUG
            test_unit();
#endif
            _IA = new IA();
            _end = 0;
            Pos pos = new Pos();

            while (_end != 84 && _end != 42)
            {
                _end = iointerface.GetInput(ref board, ref infos);
                if (_end == 1)
                {
                    pos = _IA.BrainTurn(ref board);
                    iointerface.SetTurn(pos);
                    if (board.PlaceARock(1, pos.Y, pos.X) == 1)
                        return (84);
                }
            }
            return _end;
        }
    }
}
