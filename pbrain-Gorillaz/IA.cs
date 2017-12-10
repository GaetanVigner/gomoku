using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorillaz
{
    class IA
    {
        private int VerticalColorPossibility(ref Board board, Pos pos, int color = 2)
        {
            int size = 1;
            int ofs;

            ofs = 1;
            while (pos.Y - ofs > 0 &&
                (board.Grid[pos.Y - ofs, pos.X] == 0 || board.Grid[pos.Y - ofs, pos.X] == color))
            {
                if (board.Grid[pos.Y - ofs, pos.X] == color)
                    size += 1;
                ofs += 1;
            }
            ofs = 1;
            while (pos.Y + ofs < board.SizeMax.Y &&
                (board.Grid[pos.Y + ofs, pos.X] == 0 || board.Grid[pos.Y + ofs, pos.X] == color))
            {
                if (board.Grid[pos.Y + ofs, pos.X] == color)
                    size += 1;
                ofs += 1;
            }
            return (size);
        }

        private int VerticalTotalPossibility(ref Board board, Pos pos, int color = 2)
        {
            int size = 1;
            int ofs;

            ofs = 1;
            while (pos.Y - ofs > 0 &&
                (board.Grid[pos.Y - ofs, pos.X] == 0 || board.Grid[pos.Y - ofs, pos.X] == color))
            {
                size += 1;
                ofs += 1;
            }
            ofs = 1;
            while (pos.Y + ofs < board.SizeMax.Y &&
                (board.Grid[pos.Y + ofs, pos.X] == 0 || board.Grid[pos.Y + ofs, pos.X] == color))
            {
                size += 1;
                ofs += 1;
            }
            return (size);
        }

        private Pos VerticalClosestEmpty(ref Board board, Pos pos, int color = 2)
        {
            Pos ret = new Pos();

            int size = 1;
            int hofs;
            int bofs;
            int end = 0;

            hofs = 1;
            ret.X = -1;
            while (end == 0 && pos.Y - hofs > 0 &&
                (board.Grid[pos.Y - hofs, pos.X] == 0 || board.Grid[pos.Y - hofs, pos.X] == color))
            {
                if (board.Grid[pos.Y - hofs, pos.X] == 0)
                {
                    ret.X = pos.X;
                    ret.Y = pos.Y - hofs;
                    end = 1;
                }
                size += 1;
                hofs += 1;
            }
            bofs = 1;
            end = 0;
            while (end == 0 && pos.Y + bofs < board.SizeMax.Y &&
                (board.Grid[pos.Y + bofs, pos.X] == 0 || board.Grid[pos.Y + bofs, pos.X] == color))
            {
                if (board.Grid[pos.Y + bofs, pos.X] == 0 && (ret.X == -1 || bofs < hofs))
                {
                    ret.X = pos.X;
                    ret.Y = pos.Y + bofs;
                    end = 1;
                }
                size += 1;
                bofs += 1;
            }
            return (ret);
        }

        private int HorizontalColorPossibility(ref Board board, Pos pos, int color = 2)
        {
            int size = 1;
            int ofs;

            ofs = 1;
            while (pos.X - ofs > 0 &&
                (board.Grid[pos.Y, pos.X - ofs] == 0 || board.Grid[pos.Y, pos.X - ofs] == color))
            {
                if (board.Grid[pos.Y, pos.X - ofs] == color)
                    size += 1;
                ofs += 1;
            }
            ofs = 1;
            while (pos.X + ofs < board.SizeMax.X &&
                (board.Grid[pos.Y, pos.X + ofs] == 0 || board.Grid[pos.Y, pos.X + ofs] == color))
            {
                if (board.Grid[pos.Y, pos.X + ofs] == color)
                    size += 1;
                ofs += 1;
            }
            return (size);
        }

        private int HorizontalTotalPossibility(ref Board board, Pos pos, int color = 2)
        {
            int size = 1;
            int ofs;

            ofs = 1;
            while (pos.X - ofs > 0 &&
                (board.Grid[pos.Y, pos.X - ofs] == 0 || board.Grid[pos.Y, pos.X - ofs] == color))
            {
                size += 1;
                ofs += 1;
            }
            ofs = 1;
            while (pos.X + ofs < board.SizeMax.X &&
                (board.Grid[pos.Y, pos.X + ofs] == 0 || board.Grid[pos.Y, pos.X + ofs] == color))
            {
                size += 1;
                ofs += 1;
            }
            return (size);
        }

        private Pos HorizontalClosestEmpty(ref Board board, Pos pos, int color = 2)
        {
            Pos ret = new Pos();

            int size = 1;
            int hofs;
            int bofs;
            int end = 0;
            
            hofs = 1;
            ret.X = -1;
            while (end == 0 && pos.X - hofs > 0 &&
                (board.Grid[pos.Y, pos.X - hofs] == 0 || board.Grid[pos.Y - hofs, pos.X] == color))
            {
                if (board.Grid[pos.Y, pos.X- hofs] == 0)
                {
                    ret.X = pos.X;
                    ret.Y = pos.Y - hofs;
                    end = 1;
                }
                size += 1;
                hofs += 1;
            }
            bofs = 1;
            end = 0;
            while (end == 0 && pos.X + bofs < board.SizeMax.X &&
                (board.Grid[pos.Y, pos.X + bofs] == 0 || board.Grid[pos.Y, pos.X + bofs] == color))
            {
                if (board.Grid[pos.Y, pos.X + bofs] == 0 && (ret.X == -1 || bofs < hofs))
                {
                    ret.Y = pos.Y + bofs;
                    end = 1;
                }
                size += 1;
                bofs += 1;
            }
            return (ret);
        }
        
        private void PutARock(ref int[,] boardCopy, int y, int x, int color)
        {
            boardCopy[y, x] = color;
        }

        private int DiagonalWin(int[,] boardCopy, int y, int x, int yMax, int xMax, int color)
        {
            int ySave = y;
            int xSave = x;
            int tmpSize = 1;

            //diagonale haut-gauche => bas-droite
            while (y - 1 > 0 && x - 1 > 0 &&
                boardCopy[y - 1, x - 1] == color)
            {
                x--;
                y--;
                tmpSize += 1;
                if (tmpSize == 5)
                    return (1);
            }
            x = xSave;
            y = ySave;
            while (y + 1 < yMax && x + 1 < xMax &&
                boardCopy[y + 1, x + 1] == color)
            {
                x++;
                y++;
                tmpSize += 1;
                if (tmpSize == 5)
                    return (1);
            }
            tmpSize = 1;
            //diagonale haut-droite => bas-gauche
            x = xSave;
            y = ySave;
            while (y - 1 > 0 && x + 1 < xMax &&
                boardCopy[y - 1, x + 1] == color)
            {
                x++;
                y--;
                tmpSize += 1;
                if (tmpSize == 5)
                    return (1);
            }
            x = xSave;
            y = ySave;
            while (y + 1 < yMax && x - 1 > 0 &&
                boardCopy[y + 1, x - 1] == color)
            {
                y++;
                x--;
                tmpSize += 1;
                if (tmpSize == 5)
                    return (1);
            }
            return (0);
        }

        private int HorizontalWin(int[,] boardCopy, int y, int x, int yMax, int xMax, int color)
        {
            int size = 1;
            int xSave = x;

            while (x - 1 > 0 &&
                boardCopy[y, x - 1] == color)
            {
                size += 1;
                x = x - 1;
                if (size == 5)
                    return (1);
            }
            x = xSave;
            while (x + 1 < xMax &&
                boardCopy[y, x + 1] == color)
            {
                size += 1;
                x = x + 1;
                if (size == 5)
                    return (1);
            }
            return (0);
        }

        private int VerticalWin(int[,] boardCopy, int y, int x, int yMax, int xMax, int color)
        {
            int size = 1;
            int ySave = y;

            while (y - 1 > 0 &&
                boardCopy[y - 1, x] == color)
            {
                y = y--;
                size += 1;
                if (size == 5)
                    return (1);
            }
            y = ySave;
            while (y + 1 < yMax &&
                boardCopy[y + 1, x] == color)
            {
                y = y++;
                size += 1;
                if (size == 5)
                    return (1);
            }
            return (0);
        }

        private int CheckEmptyCase(int[,] boardCopy, int yMax, int xMax)
        {
            int x = 0;
            int y = 0;

            while (y < yMax)
            {
                x = 0;
                while (x < xMax)
                {
                    if (boardCopy[y, x] == 0)
                        return (0);
                    x = x + 1;
                }
                y = y + 1;
            }
            return (1);
        }

        private int CheckWin(int[,] boardCopy, int y, int x, int yMax, int xMax)
        {
            if (CheckEmptyCase(boardCopy, yMax, xMax) == 1)
                return (1);
            if (VerticalWin(boardCopy, y, x, yMax, xMax, 1) == 1 || 
                HorizontalWin(boardCopy, y, x, yMax, xMax, 1) == 1 ||
                DiagonalWin(boardCopy, y, x, yMax, xMax, 1) == 1)
                return (0);
            if (VerticalWin(boardCopy, y, x, yMax, xMax, 2) == 1 || 
                HorizontalWin(boardCopy, y, x, yMax, xMax, 2) == 1 ||
                DiagonalWin(boardCopy, y, x, yMax, xMax, 2) == 1)
                return (2);
            return (3);
        }

        private int DoAGame(int[,] boardCopy, int yMax, int xMax, int y, int x)
        {
            int ret;
            int i = 0;
            int j = 0;
            int color = 1;
            var random = new Random((int)DateTime.Now.Ticks);

            while ((ret = CheckWin(boardCopy, y, x, yMax, xMax)) == 3)
            {
                j = random.Next(0, yMax);
                i = random.Next(0, xMax);
                while (boardCopy[j, i] != 0)
                {
                    j = random.Next(0, yMax);
                    i = random.Next(0, xMax);
                }
                PutARock(ref boardCopy, j, i, color);
                if ((color = color + 1) > 2)
                    color = 1;
                y = j;
                x = i;
            }
            return (ret);
        }

        private void MonteCarlo(ref Board board, Pos pos, ref int numberWin, ref int numberNotLoose, int numberGame)
        {
            int[,] boardCopy = new int[board.SizeMax.Y, board.SizeMax.X];
            int i = 0;
            int res = 0;

            while (i < numberGame)
            {
                board.CopyGrid(ref boardCopy);
                PutARock(ref boardCopy, pos.Y, pos.X, 1);
                if ((res = DoAGame(boardCopy, board.SizeMax.Y, board.SizeMax.X, pos.Y, pos.X)) == 0)
                {
                    numberWin++;
                    numberNotLoose++;
                }
                else if (res == 1)
                    numberNotLoose++;
                i++;
            }
        }

        public Pos CheckDefense(ref Board board)
        {
            Pos ret;
            if (HorizontalColorPossibility(ref board, board.LastMove) >= 3 &&
                HorizontalTotalPossibility(ref board, board.LastMove) >= 5)
            {
                return (HorizontalClosestEmpty(ref board, board.LastMove));
            }
            if (VerticalColorPossibility(ref board, board.LastMove) >= 3 &&
                VerticalTotalPossibility(ref board, board.LastMove) >= 5)
            {
                return (VerticalClosestEmpty(ref board, board.LastMove));
            }
            ret = new Pos();
            ret.X = -1;
            ret.Y = -1;
            return (ret);
        }

        public Pos BrainTurn(ref Board board, ref IOinterface ointerface)
        {
            List<Pos> posPossible;
            Pos saveMaxWin = new Pos();
            Pos saveMinLoose = new Pos();
            Pos defense = new Pos();
            int i = 0;
            int numberGame = 15000;
            int numberMaxWin = 0;
            int numberMinLoose = 0;
            int numberWin = 0;
            int numberNotLoose = 0;

            posPossible = board.GetPlayablePos();
            if ((i = posPossible.Count()) == 1)
                return (posPossible[0]);
            if (i == 0)
            {
                Pos middle = new Pos();
                middle.X = board.SizeMax.X / 2;
                middle.Y = board.SizeMax.Y / 2;
                return (middle);
            }
            defense = CheckDefense(ref board);
            if (defense.X > 0 && defense.Y > 0)
            {
                return (defense);
            }
            numberGame = numberGame / i;
            while (i > 0)
            {
                MonteCarlo(ref board, posPossible[i - 1], ref numberWin, ref numberNotLoose, numberGame);
                if (numberMaxWin < numberWin)
                {
                    saveMaxWin = posPossible[i - 1];
                    numberMaxWin = numberWin;
                }
                if (numberMinLoose < numberNotLoose)
                {
                    saveMinLoose = posPossible[i - 1];
                    numberMinLoose = numberNotLoose;
                }
                numberWin = 0;
                numberNotLoose = 0;
                i--;
            }
            if (numberMaxWin > 1.1 * numberMinLoose || numberMaxWin > numberGame * 0.8)
                return (saveMaxWin);
            return (saveMinLoose);
        }
    }
}
