namespace SetStats
{
    public class Class1
    {

        public static float[,] LevelRandom(float[,] stats)
        {
            const int RowToSet = 2,
                MinValueRow = 0,
                MaxValueRow = 1,
                ReachMaxNum = 1;

            Random rnd = new Random();
            for (int i = 0; i < stats.GetLength(1); i++)
            {
                stats[RowToSet, i] = rnd.Next(
                    (int)stats[MinValueRow, i],
                    (int)stats[MaxValueRow, i] + ReachMaxNum
                );
            }
            return stats;
        }
    }

    
}
