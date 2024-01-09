using Utilities;

namespace SetStats
{
    public class SetStat
    {

        public static float[,] StatSetter(float[,] stats, int difficulty, bool isHero)
        {
            if(Utility.InRange(difficulty,2))
                return DefaultLevel(stats, difficulty, isHero);
            if(Utility.Equals(difficulty,3))
                return PersonalizedLevel(stats);
            return RandomLevel(stats);
            
        }

        public static float[,] DefaultLevel(float[,] stats, int difficulty,bool isHero)//Difficult
        {
            const int RowToSet = 2;
            int rowToPick ;
            
            if (isHero)
            {
                 rowToPick = difficulty == 1 ? 1 : 0;
            }
            else
            {
                 rowToPick = difficulty == 1 ? 0 : 1;
            }
                

            for (int i = 0; i < stats.GetLength(1); i++)
            {
                stats[RowToSet, i] = stats[rowToPick, i];
            }

            return stats;
        }

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
                MaxAttemps = 3,
                DefaultAttemps=1;

            string[] StatsRequirement = { HP + RangedIn, Attack + RangedIn, DmgReduccion + RangedIn };

            for (int i = 0; i < stats.GetLength(1); i++)
            { 
                int attemps = DefaultAttemps;
                float userInsert;
                bool inRange;
                do
                {
                    Console.WriteLine($"{InsertRequest}\n {StatsRequirement[i]}",
                                     stats[MinValueRow, i], stats[MaxValueRow, i]);

                    userInsert = Convert.ToSingle(Console.ReadLine());
                    inRange = Utility.InRange(userInsert, stats[MinValueRow, i], stats[MaxValueRow, i]);

                    if (!inRange)
                    {
                        attemps++;
                    }
                    else
                    {
                        stats[RowToSet, i] = userInsert;
                    }        

                } while (!inRange && Utility.InRange(attemps, MaxAttemps));
            }
            return stats;
        }

        public static float[,] RandomLevel(float[,] stats)
        {
            const int RowToSet = 2,
                MinValueRow = 0,
                MaxValueRow = 1,
                ReachMaxNum = 1;

            Random rnd = new Random();
            for (int i = 0; i < stats.GetLength(i); i++)
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
