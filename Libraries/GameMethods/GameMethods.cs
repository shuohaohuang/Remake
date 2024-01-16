using System;
using GameConstants;
using Checkers;
using Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace GameMethods
{
    public class Stats
    {
        public static float[,] Setter(float[,] stats, int difficulty, bool isHero)
        {
            if (Check.InRange(difficulty, GameConstant.Two))
                return DefaultLevel(stats, difficulty, isHero);
            return RandomLevel(stats);
        }

        public static float[,] DefaultLevel(float[,] stats, int difficulty, bool isHero) //Difficult
        {
            int rowToPick;

            if (isHero)
            {
                rowToPick = difficulty == GameConstant.One ? GameConstant.MaxValueRow : GameConstant.MinValueRow;
            }
            else
            {
                rowToPick = difficulty == GameConstant.One ? GameConstant.MinValueRow : GameConstant.MaxValueRow;
            }
            for (int i = 0; i < stats.GetLength(1); i++)
            {
                stats[GameConstant.RowToSet, i] = stats[rowToPick, i];
            }
            return stats;
        }

        public static float[,] RandomLevel(float[,] stats)
        {
            const int
                ReachMaxNum = 1;

            Random rnd = new();
            for (int i = 0; i < stats.GetLength(0); i++)
            {
                stats[GameConstant.RowToSet, i] = rnd.Next(
                    (int)stats[GameConstant.MinValueRow, i],
                    (int)stats[GameConstant.MaxValueRow, i] + ReachMaxNum
                );
            }
            return stats;
        }

        public static float GetMaxHp(float[,] stats)
        {
            return stats[GameConstant.SetMaxHpRow, GameConstant.hpValueColumn];
        }

        public static float GetMaxAttack(float[,] stats)
        {
            return stats[GameConstant.SetMaxAttackRow, GameConstant.attackValueColumn];
        }

        public static float GetMaxReduction(float[,] stats)
        {
            return stats[GameConstant.SetMaxReductionRow, GameConstant.reductionValueColumn];
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
            const int CriticalEffect = 2;
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
