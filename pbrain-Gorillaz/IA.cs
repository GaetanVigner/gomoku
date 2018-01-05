using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorillaz
{
    class IA
    {
        private int TotalPossibility(ref Board board, Pos pos, int Xoffset, int Yoffset, int color = 2)
        {
            int ret = 1;
            int i;

            i = 1;
            while (i <= 4 && pos.Y + (i * Yoffset) < board.SizeMax.Y
                && pos.Y + (i * Yoffset) >= 0
                && pos.X + (i * Xoffset) < board.SizeMax.X
                && pos.X + (i * Xoffset) >= 0
                && (board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == 0
                || board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == color))
            {
                ret += 1;
                i += 1;
            }
            i = -1;
            while (i >= -4 && pos.Y + (i * Yoffset) < board.SizeMax.Y
                && pos.Y + (i * Yoffset) >= 0
                && pos.X + (i * Xoffset) < board.SizeMax.X
                && pos.X + (i * Xoffset) >= 0
                && (board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == 0
                || board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == color))
            {
                ret += 1;
                i -= 1;
            }
            return (ret);
        }

        private int LignWeight(ref Board board, ref Pos pos, int Xoffset, int Yoffset,
            int rowFactor, int color = 2)
        {
            int tmpRowFactor;
            int highestRowFactor = 0;
            int proximityFactor = 0;
            int proximity = 0;
            int spaces;
            int ret = 0;
            int i;
            
            i= 1;
            tmpRowFactor = 0;
            spaces = 0;
#if DEBUG
            System.Console.WriteLine("case:" + (pos.X + (i * Xoffset)) + " " + (pos.Y + (i * Yoffset)));
#endif
            while (i <= 4 && pos.Y + (i * Yoffset) < board.SizeMax.Y 
                && pos.Y + (i * Yoffset) >= 0
                && pos.X + (i * Xoffset) < board.SizeMax.X
                && pos.X + (i * Xoffset) >= 0
                && (board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == 0
                || board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == color))
            {
#if DEBUG
                System.Console.WriteLine("case:" + (pos.X + (i * Xoffset)) + " " + (pos.Y + (i * Yoffset)));
#endif
                if (board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == color)
                {
                    if (proximity == 0)
                        proximityFactor += 1;
                    ret += (5 + tmpRowFactor);
                    ret -= spaces;
                    spaces = 0;
                    tmpRowFactor += 1;
                    if (tmpRowFactor > highestRowFactor)
                        highestRowFactor = tmpRowFactor;
                }
                else
                {
                    tmpRowFactor = 0;
                    spaces += 1;
                    proximity += 1;
                }
                i += 1;
            }
            i = -1;
            tmpRowFactor = 0;
            spaces = 0;
            proximity = 0;
            while (i >= -4 && pos.Y + (i * Yoffset) < board.SizeMax.Y
                && pos.Y + (i * Yoffset) >= 0
                && pos.X + (i * Xoffset) < board.SizeMax.X
                && pos.X + (i * Xoffset) >= 0
                && (board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == 0
                || board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == color))
            {
                if (board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == color)
                {
                    if (proximity == 0)
                        proximityFactor += 1;
                    ret += (5 + tmpRowFactor);
                    ret -= spaces;
                    spaces = 0;
                    tmpRowFactor += 1;
                    if (tmpRowFactor > highestRowFactor)
                        highestRowFactor = tmpRowFactor;
                }
                else
                {
                    tmpRowFactor = 0;
                    spaces += 1;
                }
                i -= 1;
            }
            if (highestRowFactor >= rowFactor) //la priorisation n'a pas l'air de fonctionner
            {
                ret = ret * (2 + (highestRowFactor - rowFactor));
                if (highestRowFactor == 4 && color == 2)
                    ret += 2500;
                else if (highestRowFactor == 4 && color == 1)
                    ret += 3000;
            }
            if (proximityFactor >= rowFactor && color == 2)
                ret = ret * (2 + (highestRowFactor - rowFactor)) * 8;
            return (ret);
        }


        private int PatternWeight(ref Board board, ref Pos pos, int Xoffset, int Yoffset, int color = 2, int row = 3)
        {
            int ret = 0;
            int spaces = 0;
            int i;

            i = 1;
            while (i <= 4 && pos.Y + (i * Yoffset) < board.SizeMax.Y
                && pos.Y + (i * Yoffset) >= 0
                && pos.X + (i * Xoffset) < board.SizeMax.X
                && pos.X + (i * Xoffset) >= 0
                && (board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == 0
                || board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == color))
            {
                if (board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == color)
                    ret += 1;
                else if (board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == 0)
                    spaces += 1;
                i += 1;
            }
            i = -1;
            while (i >= -4 && pos.Y + (i * Yoffset) < board.SizeMax.Y
                && pos.Y + (i * Yoffset) >= 0
                && pos.X + (i * Xoffset) < board.SizeMax.X
                && pos.X + (i * Xoffset) >= 0
                && (board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == 0
                || board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == color))
            {
                if (board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == color)
                    ret += 1;
                else if (board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == 0)
                    spaces += 1;
                i -= 1;
            }
            if (ret >= row && ret + spaces >= 4)
                return (2000);
            return (0);
        }

        private int OffensiveDefensivePattern(ref Board board, Pos pos)
        {

            int ret = 0;
            if (TotalPossibility(ref board, board.LastMove, 1, 0) >= 5)
            {
                ret += PatternWeight(ref board, ref pos, 1, 0);
                ret += PatternWeight(ref board, ref pos, 1, 0, 1, 4);
            }
            if (TotalPossibility(ref board, board.LastMove, 0, 1) >= 5)
            {
                ret += PatternWeight(ref board, ref pos, 0, 1);
                ret += PatternWeight(ref board, ref pos, 0, 1, 1, 4);
            }
            if (TotalPossibility(ref board, board.LastMove, 1, 1) >= 5)
            {
                ret += PatternWeight(ref board, ref pos, 1, 1);
                ret += PatternWeight(ref board, ref pos, 1, 1, 1, 4);
            }
            if (TotalPossibility(ref board, board.LastMove, -1, 1) >= 5)
            {
                ret += PatternWeight(ref board, ref pos, -1, 1);
                ret += PatternWeight(ref board, ref pos, -1, 1, 1, 4);
            }
            return (ret);
        }
        private int GetDefensiveWeight(ref Board board, Pos pos)
        {
            int ret = 0;
            if (TotalPossibility(ref board, board.LastMove, 1, 0) >= 5)
                ret += LignWeight(ref board, ref pos, 1, 0, 3);
            if (TotalPossibility(ref board, board.LastMove, 0, 1) >= 5)
                ret += LignWeight(ref board, ref pos, 0, 1, 3);
            if (TotalPossibility(ref board, board.LastMove, 1, 1) >= 5)
                ret += LignWeight(ref board, ref pos, 1, 1, 3);
            if (TotalPossibility(ref board, board.LastMove, -1, 1) >= 5)
                ret += LignWeight(ref board, ref pos, -1, 1, 3);
#if DEBUG
            System.Console.WriteLine("defensive: " + pos.X + " " + pos.Y + " = " + ret);
#endif
            return (ret);
        }

        private int GetOffensiveWeight(ref Board board, Pos pos)
        {
            int ret = 0;
            if (TotalPossibility(ref board, board.LastMove, 1, 0, 1) >= 5)
                ret += LignWeight(ref board, ref pos, 1, 0, 4, 1);
            if (TotalPossibility(ref board, board.LastMove, 0, 1, 1) >= 5)
                ret += LignWeight(ref board, ref pos, 0, 1, 4, 1);
            if (TotalPossibility(ref board, board.LastMove, 1, 1, 1) >= 5)
                ret += LignWeight(ref board, ref pos, 1, 1, 4, 1);
            if (TotalPossibility(ref board, board.LastMove, -1, 1, 1) >= 5)
                ret += LignWeight(ref board, ref pos, -1, 1, 4, 1);
#if DEBUG
            System.Console.WriteLine("offensive: " + pos.X + " " + pos.Y + " = " + ret);
#endif
            return (ret);
        }

        private int GetWeight(ref Board board, Pos pos)
        {
            int weight = 0;
            weight += OffensiveDefensivePattern(ref board, pos);
            weight += GetDefensiveWeight(ref board, pos);
            weight += GetOffensiveWeight(ref board, pos);
#if DEBUG
            System.Console.WriteLine("Total: " + pos.X + " " + pos.Y + " = " + weight);
#endif
            return (weight);
        }

        /// <summary>
        /// get the case with the highest weight
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        private Pos WeightCalcul(ref Board board)
        {
            int higher = 0;
            int tmp;
            Pos ret = new Pos();
            ret.X = ret.Y = -1;
            List<Pos> playablePositions = board.GetPlayablePos();
            if (playablePositions.Capacity <= 0)
                return (ret);
            for (int i = 0; i < playablePositions.Count(); i++)
            {
                tmp = GetWeight(ref board, playablePositions[i]);
                if (tmp > higher)
                {
                    higher = tmp;
                    ret = playablePositions[i];
                }
            }
            return (ret);
        }

        /// <summary>
        /// the AI plays it's turn and return it 
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public Pos BrainTurn(ref Board board)
        {
            Pos attackDefense;
            attackDefense = WeightCalcul(ref board);
            if (attackDefense.X >= 0 && attackDefense.Y >= 0)
                return (attackDefense);
            attackDefense = board.GetRandomPos();
            board.IALastMove = attackDefense;
            return (attackDefense);
        }
    }
}
