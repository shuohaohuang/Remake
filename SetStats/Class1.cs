namespace SetStats
{
    public class Class1
    {

        public static float[,] DefaultLevel(float[,] stats)//Easy
        {
            int RowToSet = 2,
                RowToPick = 1;

            for (int i = 0; i < stats.GetLength(1); i++)
            {
                stats[RowToSet, i] = stats[RowToPick, i];
            }
            return stats;
        }

        public static float[,] DefaultLevel(float[,] stats, int difficulty)//Difficult
        {
            int RowToSet = 2,
                RowToPick = 0;

            for (int i = 0; i < stats.GetLength(1); i++)
            {
                stats[RowToSet, i] = stats[RowToPick, i];
            }

            return stats;
        }


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
