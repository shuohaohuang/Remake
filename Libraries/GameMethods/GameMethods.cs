using System;
using GameConstants;
using Checkers;
using Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace Rpg
{
    public class Stats
    {
        public static float[,] Setter(float[,] stats, int difficulty, bool isHero)
        {
            if (Check.InRange(difficulty, 2))
                return DefaultLevel(stats, difficulty, isHero);
            return RandomLevel(stats);
        }

        public static float[,] DefaultLevel(float[,] stats, int difficulty, bool isHero) //Difficult
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
            const int RowToSet = 2,
                MinValueRow = 0,
                MaxValueRow = 1,
                MaxAttemps = 3,
                DefaultAttemps = 1;

            string[] StatsRequirement =
            [
                GameConstant.HpMenuMsg + GameConstant.RangedInMsg,
                GameConstant.AttackMenuMsg + GameConstant.RangedInMsg,
                GameConstant.DmgReduccionMenuMsg + GameConstant.RangedInMsg
            ];

            for (int i = 0; i < stats.GetLength(1); i++)
            {
                int attemps = DefaultAttemps;
                float userInsert;
                bool inRange;
                do
                {
                    Console.WriteLine(
                        $"{GameConstant.InsertRequestMsg}\n {StatsRequirement[i]}",
                        stats[MinValueRow, i],
                        stats[MaxValueRow, i]
                    );

                    userInsert = Convert.ToSingle(Console.ReadLine());
                    inRange = Check.InRange(
                        userInsert,
                        stats[MinValueRow, i],
                        stats[MaxValueRow, i]
                    );

                    if (!inRange)
                    {
                        attemps++;
                        if (!Check.InRange(attemps, MaxAttemps))
                            stats[RowToSet, i] = stats[MinValueRow, i];
                    }
                    else
                    {
                        stats[RowToSet, i] = userInsert;
                    }
                } while (!inRange && Check.InRange(attemps, MaxAttemps));
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

        public static float GetHp(float[,] stats)
        {
            const int hpColumn = 0,
                SettedHpRow = 2;
            return stats[SettedHpRow, hpColumn];
        }

        public static float GetAttack(float[,] stats)
        {
            const int attackColumn = 1,
                SettedAttackRow = 2;
            return stats[SettedAttackRow, attackColumn];
        }

        public static float GetReduction(float[,] stats)
        {
            const int reductionColumn = 2,
                SettedReductionRow = 2;
            return stats[SettedReductionRow, reductionColumn];
        }


        public static string Rename(string name) //
        {
            const string RequestNameMsg = "Insert {0}'s new name is ",
                RenamedMsg = "{0}'s new name is {1}";

            string newName;
            Console.WriteLine(RequestNameMsg, name);
            newName = Utility.NameMayus(Console.ReadLine() ?? name);
            Console.WriteLine(RenamedMsg, name, newName);
            return newName;
        }
    }
    public class Battle
    {
        public static float CalculateDamage(
            float attackerAd,
            float defenderReduction,
            bool criticAttack
        )
        {
            const int Digits = 2,
                CriticalEffect = 2;
            const float Percentage = 100,
                One = 1;

            if (criticAttack)
                return Math.Abs(
                    attackerAd * (One - (defenderReduction / Percentage)) * CriticalEffect
                );

            return 
                Math.Abs(attackerAd * (One - (defenderReduction / Percentage))
            );
        }

        public static float CalculateDamage(
            float attackerAd,
            float defenderReduction,
            float guardEffect,
            bool isGuarding,
            bool criticAttack
        )
        {
            const int CriticalEffect = 2;
            const float Percentage = 100,
                One = 1;

            if (isGuarding)
                defenderReduction *= guardEffect;

            if (criticAttack)
                return Math.Abs(
                    attackerAd * (One - (defenderReduction / Percentage)) * CriticalEffect
                );

            return Math.Abs(attackerAd * (One - (defenderReduction / Percentage)));
        }

        public static float RemainedHp(float currentHp, float receivedDamage)
        {
            return receivedDamage > currentHp ? 0 : currentHp - receivedDamage;
        }

        public static void InformAction(
            string attackerName,
            string defenderName,
            float inflictedDamage
        )
        {
            const string Msg = "{0} has dealt {1} damage to {2}";
            Console.WriteLine(Msg, attackerName, inflictedDamage, defenderName);
        }

        public static bool Probability(float probability)
        {
            const int MaxProbability = 100;
            Random random = new();

            return Check.InRange(random.Next(MaxProbability), probability);
        }
    }
}
