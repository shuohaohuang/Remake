using System.Xml.Linq;
using Utilities;

namespace Rpg

{
    public class SetStat
    {

        public static float[,] StatSetter(float[,] stats, int difficulty, bool isHero)
        {
            if (Utility.InRange(difficulty, 2))
                return DefaultLevel(stats, difficulty, isHero);
            if (Utility.Equals(difficulty, 3))
                return PersonalizedLevel(stats);
            return RandomLevel(stats);

        }

        public static float[,] DefaultLevel(float[,] stats, int difficulty, bool isHero)//Difficult
        {
            const int RowToSet = 2;
            int rowToPick;

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
            const string Hp = "Hit Points: ",
                Attack = "Attack: ",
                DmgReduccion = "Damage  Reduction: ",
                RangedIn = "In range [{0}-{1}]",
                InsertRequest = "Insert stat value";
            const int RowToSet = 2,
                MinValueRow = 0,
                MaxValueRow = 1,
                MaxAttemps = 3,
                DefaultAttemps = 1;

            string[] StatsRequirement = { Hp + RangedIn, Attack + RangedIn, DmgReduccion + RangedIn };

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
                        if (!Utility.InRange(attemps, MaxAttemps))
                            stats[RowToSet, i] = stats[MinValueRow, i];
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

            Random rnd = new();
            for (int i = 0; i < stats.GetLength(0); i++)
            {
                stats[RowToSet, i] = rnd.Next(
                    (int)stats[MinValueRow, i],
                    (int)stats[MaxValueRow, i] + ReachMaxNum
                );
            }
            return stats;
        }
        public static string Rename(string name)
        {
            const string NewName = "Character's new name is ",
                ConfirmMsg = "Is {0} {1}'s new name? \n[Y/N]",
                 Yes = "Y", No = "N",
                 ErrorMsg = "Wrong insert, try again";
            string newName, userInput;
            bool confirmation = false, checker;
            do
            {
                Console.WriteLine(NewName);
                newName = Utility.NameMayus(Console.ReadLine() ?? name);
                Console.WriteLine(ConfirmMsg, newName, name);
                do
                {
                    userInput = Console.ReadLine()?.ToUpper() ?? "";
                    switch (userInput)
                    {
                        case Yes:
                            checker = true;
                            confirmation = true;
                            break;
                        case No:
                            checker = true;
                            break;
                        default:
                            Console.WriteLine(ErrorMsg);
                            checker = false;
                            break;

                    }
                } while (!checker);
            } while (!confirmation);

            return newName;
        }

    }
    public class GetStat
    {
        public static float getHp(float[,] stats)
        {
            const int hpColumn = 0,
                currentHpRow = 3;
            return stats[hpColumn, currentHpRow];
        }
        public static float getAttack(float[,] stats)
        {
            const int attackColumn = 1,
                currentAttackRow = 3;
            return stats[attackColumn, currentAttackRow];
        }
        public static float getReduction(float[,] stats)
        {
            const int reductionColumn = 0,
                currentReductionRow = 3;
            return stats[reductionColumn, currentReductionRow];
        }

    }

}