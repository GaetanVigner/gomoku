namespace Gorillaz
{
    public class PlayPos : Pos
    {
        private int enemyLength;
        private int allyLength;
        
        public int EnemyLength { get => enemyLength; set => enemyLength = value > enemyLength ? value : enemyLength; }
        public int AllyLength { get => allyLength; set => allyLength = value > allyLength ? value : allyLength; }

        public void ZeroEnemyLength() => enemyLength = 0;
        public void ZeroAllyLength() => allyLength = 0;

        public PlayPos()
        {
            X = 0;
            Y = 0;
            enemyLength = 0;
            allyLength = 0;
        }

        public PlayPos(Pos pos)
        {
            this.X = pos.X;
            this.Y = pos.Y;
            enemyLength = 0;
            allyLength = 0;
        }
    }
}
