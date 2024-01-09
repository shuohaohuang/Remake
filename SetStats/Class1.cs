using Utilities;

namespace SetStats
{
    public class Class1
    {
        public static float[,] PersonalizedLevel(float[,] stats)
        {
            const string HP = "Hit Points: ",
                Attack = "Attack: ",
                DmgReduccion = "Damage  Reduction: ",
                RangedIn = "In range [{0}-{1}]",
                InsertRequest = "Insert stat value";
            const int RowToSet = 2,
                MinValueRow = 0,
                MaxValueRow = 1,
                ReachMaxNum = 1,
                MaxAttemps = 3;
            int attemps = 1;

            string[] StatsRequirement = { HP + RangedIn, Attack + RangedIn, DmgReduccion + RangedIn };

            for (int i = 0; i < stats.GetLength(1); i++)
            {
                float userInsert;
                bool inRange;
                do
                {
                    Console.WriteLine($"{InsertRequest}\n {StatsRequirement[i]}",
                                     stats[MinValueRow, i], stats[MaxValueRow, i]);

                    userInsert = Convert.ToSingle(Console.ReadLine());
                    inRange = Utility.InRange(userInsert, stats[MinValueRow, i], stats[MaxValueRow, i]);

                    if (inRange)
                        attemps++;

                } while (!inRange && Utility.InRange(attemps, MaxAttemps));
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
