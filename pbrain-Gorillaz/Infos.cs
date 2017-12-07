using System;
using System.Collections.Generic;
using System.Text;

namespace Gorillaz
{
    class Infos
    {
        int timeoutTurn = 5;
        int timeoutMatch;
        int maxMemory = 70000000; //70 Mo
        int timeLeft;
        int gameType;
        int rule;
        int[] evaluate;
        string folder;

        public int TimeoutTurn { get => timeoutTurn; set => timeoutTurn = value; }
        public int TimeoutMatch { get => timeoutMatch; set => timeoutMatch = value; }
        public int MaxMemory { get => maxMemory; set => maxMemory = value; }
        public int TimeLeft { get => timeLeft; set => timeLeft = value; }
        public int GameType { get => gameType; set => gameType = value; }
        public int Rule { get => rule; set => rule = value; }
        public int[] Evaluate { get => evaluate; set => evaluate = value; }
        public string Folder { get => folder; set => folder = value; }
    }
}
