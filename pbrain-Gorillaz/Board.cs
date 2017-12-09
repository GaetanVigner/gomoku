using System;
using System.Collections.Generic;
using System.Text;

namespace Gorillaz
{
    class Board
    {
        private int[,] _grid;
        private int _xMax = 19;
        private int _yMax = 19;
        private int _lastMoveX = -1;
        private int _lastMoveY = -1;

        public int XMax { get => _xMax; set => _xMax = value; }
        public int YMax { get => _yMax; set => _yMax = value; }
        public int[,] Grid { get => _grid; set => _grid = value; }
        public int LastMoveX { get => _lastMoveX; set => _lastMoveX = value; }
        public int LastMoveY { get => _lastMoveY; set => _lastMoveY = value; }

        /// <summary>
        /// initialize to board and place a 0 on every case of the grid
        /// </summary>
        public void Initialize()
        {        
            int i;
            int j;

            for (i = 0; i < YMax; i++)
            {
                for (j = 0; j < XMax; j++)
                {
                    Grid[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Define the board size
        /// </summary>
        public int SetBoardSize(dynamic x, dynamic y)
        {
            if (x < 5 || y < 5 || (x * y) > 1600)
                return (1);
            XMax = x;
            YMax = y;
            Grid = new int[YMax, XMax];
            Initialize();
            return (0);
        }

        /// <summary>
        /// Define the board size
        /// </summary>
        public int SetBoardSize(dynamic size)
        {
            if (size < 5 || size > 40)
                return (1);
            XMax = size;
            YMax = size;
            Grid = new int[YMax, XMax];
            Initialize();
            return (0);
        }

        /// <summary>
        /// Place a rock on the board, 1 for the client 2 for the opponent
        /// (has a probability to place Dwayne Johnson)
        /// </summary>
        /// <returns></returns>
        public int PlaceARock(int player, int y, int x)
        {
            //there is some checks to add
            if (Grid == null || Grid.Length == 0 ||
                Grid.GetLength(0) <= y || Grid.GetLength(1) <= x)
            {
                return (1);
            }
            Grid[y, x] = player;
            LastMoveX = x;
            LastMoveY = y;
            return (0);
        }

        /// <summary>
        /// takes a wordtab, interpret and fill the board
        /// </summary>
        /// <param name="wordtab"></param>
        public void FillFromWordTab(string[] wordtab)
        {
            int x = -1;
            int y = -1;
            int field = -1;
            int tmp;

            foreach (string str in wordtab)
            {
                if (Int32.TryParse(str, out tmp) == true)
                {
                    if (x == -1)
                    {
                        x = tmp;
                    }
                    else if (y == -1)
                    {
                        y = tmp;
                    }
                    else if (field == -1)
                    {
                        field = tmp;
                        PlaceARock(field, y, x);
                        x = y = field = -1;
                    }
                }
            }
        }
    }
}
