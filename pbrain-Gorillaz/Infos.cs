namespace Gorillaz
{
    public class Infos
    {
        public int TimeoutTurn { get; set; } = 5;
        public int TimeoutMatch { get; set; }
        public int MaxMemory { get; set; } = 70000000; //70 Mo
        public int TimeLeft { get; set; }
        public int GameType { get; set; }
        public int Rule { get; set; }
        public int[] Evaluate { get; set; }
        public string Folder { get; set; }
    }
}
