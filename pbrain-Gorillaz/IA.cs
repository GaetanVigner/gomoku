using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorillaz
{
    class IA
    {
        /// <summary>
        /// check the maximum alignement of one color verticaly
        /// </summary>
        /// <param name="board"></param>
        /// <param name="pos"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private int VerticalPossibility(ref Board board, ref Pos pos, int color)
        {
            int size = 1;

            while (pos.Y - 1 > 0 && 
                (board.Grid[pos.Y - 1, pos.X] == 0 || board.Grid[pos.Y - 1, pos.X] == color))
                size += 1;
            while (pos.Y + 1 < board.SizeMax.Y &&
                (board.Grid[pos.Y + 1, pos.X] == 0 || board.Grid[pos.Y + 1, pos.X] == color))
                size += 1;
            return (size);
        }

        /// <summary>
        /// check the maximum alignement of one color horizontaly
        /// </summary>
        /// <param name="board"></param>
        /// <param name="pos"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private int HorizontalPossibility(ref Board board, ref Pos pos, int color)
        {
            int size = 1;

            while (pos.X - 1 > 0 &&
                (board.Grid[pos.Y, pos.X - 1] == 0 || board.Grid[pos.Y, pos.X - 1] == color))
                size += 1;
            while (pos.X + 1 < board.SizeMax.X &&
                (board.Grid[pos.Y, pos.X + 1] == 0 || board.Grid[pos.Y, pos.X + 1] == color))
                size += 1;
            return (size);
        }

        /// <summary>
        /// check the maximum alignement of one color in diagonal
        /// </summary>
        /// <param name="board"></param>
        /// <param name="pos"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private int DiagonalPossibility(ref Board board, ref Pos pos, int color)
        {
            int size = 1;
            int tmpSize = 1;

            //diagonale haut-gauche => bas-droite
            while (pos.Y - 1 > 0 && pos.X - 1 > 0 &&
                (board.Grid[pos.Y - 1, pos.X - 1] == 0 || board.Grid[pos.Y - 1, pos.X - 1] == color))
                tmpSize += 1;
            while (pos.Y + 1 < board.SizeMax.Y && pos.X + 1 < board.SizeMax.X &&
                (board.Grid[pos.Y + 1, pos.X + 1] == 0 || board.Grid[pos.Y + 1, pos.X + 1] == color))
                tmpSize += 1;
            size = tmpSize;
            tmpSize = 1;
            //diagonale haut-droite => bas-gauche
            while (pos.Y - 1 > 0 && pos.X + 1 < board.SizeMax.X &&
                (board.Grid[pos.Y - 1, pos.X + 1] == 0 || board.Grid[pos.Y - 1, pos.X + 1] == color))
                tmpSize += 1;
            while (pos.Y + 1 < board.SizeMax.Y && pos.X - 1 > 0 &&
                (board.Grid[pos.Y + 1, pos.X - 1] == 0 || board.Grid[pos.Y + 1, pos.X - 1] == color))
                tmpSize += 1;
            if (tmpSize > size)
                return (tmpSize);
            return (size);
        }

        public int BrainTurn(ref Board board, ref IOinterface ointerface)
        {

            return (0);
        }
    }
}
