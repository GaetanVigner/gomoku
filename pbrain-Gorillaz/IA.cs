using System.Collections.Generic;

namespace Gorillaz
{
    class IA
    {
        /// <summary>
        /// return the total rocks that a player can align in a row from a start point
        /// </summary>
        /// <returns></returns>
        private int TotalPossibility(ref Board board, PlayPos pos, int Xoffset, int Yoffset, int color = 2)
        {
            int ret = 1;
            int i;

            i = 1;
            while (i <= 4
                && pos.Y + (i * Yoffset) < board.SizeMax.Y
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
            while (i >= -4
                && pos.Y + (i * Yoffset) < board.SizeMax.Y
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

        //no possibilities *4* *3°* *2°°* *°2°* *1°°1°* etc Already handled
        
            // **z

        //opened one side
        //opened 2 sides
        //opened middle
        //opened middle one side
        //opened middle 2 sides



        private int LignWeight(ref Board board, ref PlayPos pos, int Xoffset, int Yoffset,
            int rowFactor, int color = 2)
        {
            int spaces;
            int ret = 0;
            int i;
            
            i= 1;
            spaces = 0;

            while (i <= 4 && spaces <= 1
                && pos.Y + (i * Yoffset) < board.SizeMax.Y 
                && pos.Y + (i * Yoffset) >= 0
                && pos.X + (i * Xoffset) < board.SizeMax.X
                && pos.X + (i * Xoffset) >= 0
                && (board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == 0
                || board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == color))
            {
                if (board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == color)
                {
                    ret += i * (i - spaces);
                }
                else
                {
                    spaces += 1;
                }
                i += 1;
            }
            i = -1;
            while (i >= -4 && spaces <= 1
                && pos.Y + (i * Yoffset) < board.SizeMax.Y
                && pos.Y + (i * Yoffset) >= 0
                && pos.X + (i * Xoffset) < board.SizeMax.X
                && pos.X + (i * Xoffset) >= 0
                && (board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == 0
                || board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == color))
            {
                if (board.Grid[pos.Y + (i * Yoffset), pos.X + (i * Xoffset)] == color)
                {
                    ret += i * (i + spaces);
                }
                else
                {
                    spaces += 1;
                }
                i -= 1;
            }
            //highestRowFactor += tmpRowFactor;
            //if (highestRowFactor >= rowFactor) //la priorisation n'a pas l'air de fonctionner
            //{
            //    ret = ret * (2 + (highestRowFactor - rowFactor));
            //if (highestRowFactor == 4 && color == 1)
            //    ret += 3000;
            //else if (highestRowFactor == 4 && color == 2)
            //    ret += 2500;
            //}
            //if (proximityFactor >= rowFactor && color == 2)
            //    ret = ret * (2 + (highestRowFactor - rowFactor)) * 8;
            return (ret * rowFactor);
        }


        private int PatternWeight(ref Board board, ref PlayPos pos, int Xoffset, int Yoffset, int color = 2, int row = 3)
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

        private int OffensiveDefensivePattern(ref Board board, PlayPos pos, int color, int enemyColor)
        {

            int ret = 0;
            if (TotalPossibility(ref board, board.LastMove, 1, 0, enemyColor) >= 5)
                ret += PatternWeight(ref board, ref pos, 1, 0, enemyColor);
            if (TotalPossibility(ref board, board.LastMove, 0, 1, enemyColor) >= 5)
                ret += PatternWeight(ref board, ref pos, 0, 1, enemyColor);
            if (TotalPossibility(ref board, board.LastMove, 1, 1, enemyColor) >= 5)
                ret += PatternWeight(ref board, ref pos, 1, 1, enemyColor);
            if (TotalPossibility(ref board, board.LastMove, -1, 1, enemyColor) >= 5)
                ret += PatternWeight(ref board, ref pos, -1, 1, enemyColor);
            if (TotalPossibility(ref board, board.LastMove, 1, 0, color) >= 5)
            {
                ret += PatternWeight(ref board, ref pos, 1, 0, color, 4) + 2;
            }
            if (TotalPossibility(ref board, board.LastMove, 0, 1, color) >= 5)
            {
                ret += PatternWeight(ref board, ref pos, 0, 1, color, 4) + 2;
            }
            if (TotalPossibility(ref board, board.LastMove, 1, 1, color) >= 5)
            {
                ret += PatternWeight(ref board, ref pos, 1, 1, color, 4) + 2;
            }
            if (TotalPossibility(ref board, board.LastMove, -1, 1, color) >= 5)
            {
                ret += PatternWeight(ref board, ref pos, -1, 1, color, 4) + 2;
            }
#if DEBUG
            System.Console.WriteLine("pattern: " + pos.X + " " + pos.Y + " = " + ret);
#endif
            return (ret);
        }

        private int GetOffensiveDefensiveWeight(ref Board board, PlayPos pos, int color, int weight)
        {
            int ret = 0;
            if (TotalPossibility(ref board, pos, 1, 0, color) >= 5)
                ret += LignWeight(ref board, ref pos, 1, 0, weight, color);
            if (TotalPossibility(ref board, pos, 0, 1, color) >= 5)
                ret += LignWeight(ref board, ref pos, 0, 1, weight, color);
            if (TotalPossibility(ref board, pos, 1, 1, color) >= 5)
                ret += LignWeight(ref board, ref pos, 1, 1, weight, color);
            if (TotalPossibility(ref board, pos, -1, 1, color) >= 5)
                ret += LignWeight(ref board, ref pos, -1, 1, weight, color);
#if DEBUG
            System.Console.WriteLine("color " + color + ": " + pos.X + " " + pos.Y + " = " + ret);
#endif
            return (ret);
        }

        private int GetWeight(ref Board board, PlayPos pos, int color = 2, int enemyColor = 1)
        {
            int weight = 0;
            //weight += OffensiveDefensivePattern(ref board, pos, color, enemyColor);
            weight += GetOffensiveDefensiveWeight(ref board, pos, color, 3);
            weight += GetOffensiveDefensiveWeight(ref board, pos, enemyColor, 4);
#if DEBUG
            System.Console.WriteLine("=====Total: " + pos.X + " " + pos.Y + " = " + weight + "=====");
#endif
            return (weight);
        }

        /// <summary>
        /// get the case with the highest weight
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        private Pos WeightCalcul(ref Board board, int color = 2, int enemyColor = 1)
        {
            int higher = 0;
            int tmp;
            Pos ret = new Pos();
            ret.X = ret.Y = -1;
            List<Pos> playablePositions = board.GetPlayablePos();
            List<PlayPos> playpos = new List<PlayPos>();
            foreach(var pos in playablePositions)
            {
                playpos.Add(new PlayPos(pos));
            }
            if (playablePositions.Capacity <= 0)
                return (ret);
            foreach(var pos in playpos)
            {
                tmp = GetWeight(ref board, pos, color, enemyColor);
                if (tmp > higher)
                {
                    higher = tmp;
                    ret = pos;
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
