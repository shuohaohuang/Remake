using System;
using System.Reflection.Metadata;
using System.Threading;
using Checkers;
using GameConstants;
using Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace GameMethods
{
    public class Stats
    {
        public static void SetPlayerCap(float[,] CharacterStats, string difficulty, bool isHero)
        {
            if (difficulty.Equals(GameConstant.DifficultyDifficult)||difficulty.Equals(GameConstant.DifficultyEasy))
            {
                DefaultLevel(CharacterStats, difficulty, isHero);
            }
            else
            {
                RandomLevel(CharacterStats);
            }
        }

        public static void DefaultLevel(float[,] CharacterStats, string difficulty, bool isHero) //Difficult
        {
            int rowToPick;

            if (isHero)
            {
                rowToPick =
                    difficulty == GameConstant.DifficultyEasy
                        ? GameConstant.MaxValueRow
                        : GameConstant.MinValueRow;
            }
            else
            {
                rowToPick =
                    difficulty == GameConstant.DifficultyDifficult
                        ? GameConstant.MinValueRow
                        : GameConstant.MaxValueRow;
            }
            for (int i = 0; i < CharacterStats.GetLength(GameConstant.RowsIteration); i++)
            {
                CharacterStats[GameConstant.RowToSetMaxValues, i] = CharacterStats[rowToPick, i];
            }
        }

        public static void RandomLevel(float[,] CharacterStats)
        {
            const int ReachMaxNum = 1;

            Random rnd = new();
            for (int i = 0; i < CharacterStats.GetLength(GameConstant.RowsIteration); i++)
            {
                CharacterStats[GameConstant.RowToSetMaxValues, i] = rnd.Next(
                    (int)CharacterStats[GameConstant.MinValueRow, i],
                    (int)CharacterStats[GameConstant.MaxValueRow, i] + ReachMaxNum
                );
            }
        }

        #region Max Stats getter
        public static float GetMaxHp(float[,] CharacterStats)
        {
            return CharacterStats[GameConstant.MaxStatsRow, GameConstant.HpValueColumn];
        }

        public static float GetMaxAttack(float[,] CharacterStats)
        {
            return CharacterStats[GameConstant.MaxStatsRow, GameConstant.AttackValueColumn];
        }

        public static float GetMaxReduction(float[,] CharacterStats)
        {
            return CharacterStats[GameConstant.MaxStatsRow, GameConstant.ReductionValueColumn];
        }
        #endregion

        #region Current Stats Getter
        public static float GetCurrentHp(float[,] CharacterStats)
        {
            return CharacterStats[GameConstant.RowCurrentValues, GameConstant.HpValueColumn];
        }

        public static float GetCurrentAttack(float[,] CharacterStats)
        {
            return CharacterStats[GameConstant.RowCurrentValues, GameConstant.AttackValueColumn];
        }

        public static float GetCurrentReduction(float[,] CharacterStats)
        {
            return CharacterStats[GameConstant.RowCurrentValues, GameConstant.ReductionValueColumn];
        }
        #endregion

        public static void SetInGame(float[,] CharacterStats)
        {
            CharacterStats[GameConstant.RowCurrentValues, GameConstant.HpValueColumn] = GetMaxHp(
                CharacterStats
            );
            CharacterStats[GameConstant.RowCurrentValues, GameConstant.AttackValueColumn] =
                GetMaxAttack(CharacterStats);
            CharacterStats[GameConstant.RowCurrentValues, GameConstant.ReductionValueColumn] =
                GetMaxReduction(CharacterStats);
        }

        public static float[] Sort(float[] hp, string[] name)
        {
            for (int i = 0; i < hp.Length; i++)
            {
                for (int j = i; j< hp.Length-1; j++)
                {
                    if (hp[i] > hp[j])
                    {
                        float aux = hp[i];
                        hp[i] = hp[j];
                        hp[j] = aux;
                    }
                }
            }
            return hp;
        }
    }

    public class Battle
    {
        public static void Attack(
            float[,] attacker,
            float[,] defensor,
            string attackerName,
            string defensorName,
            bool isGuarding = false
        )
        {
            float inflictedDamage;
            bool failedAttack,
                criticalAttack;

            failedAttack = Battle.Probability(GameConstant.FailedAttackProbability);
            if (!failedAttack)
            {
                criticalAttack = Battle.Probability(GameConstant.CriticalProbability);
                if (criticalAttack)
                    Console.WriteLine(GameConstant.CriticalAttackMsg, attackerName);
                inflictedDamage = Battle.CalculateDamage(
                    Stats.GetCurrentAttack(attacker),
                    Stats.GetCurrentReduction(defensor),
                    criticalAttack,
                    isGuarding
                );
                Battle.InformAction(attackerName, defensorName, inflictedDamage);
                defensor[GameConstant.RowCurrentValues, GameConstant.HpValueColumn] =
                    Battle.RemainedHp(Stats.GetCurrentHp(defensor), inflictedDamage);
            }
            else
            {
                Console.WriteLine(GameConstant.FailedAttackMsg, attackerName);
            }
        }

        public static float CalculateDamage(
            float attackerAd,
            float defenderReduction,
            bool criticAttack,
            bool isGuarding
        )
        {
            const float CriticalEffect = 2;
            const float Percentage = 100,
                One = 1;
            defenderReduction = isGuarding
                ? defenderReduction * GameConstant.GuardEffect
                : defenderReduction;
            if (criticAttack)
                return Math.Abs(
                    attackerAd * (One - (defenderReduction / Percentage)) * CriticalEffect
                );

            return Math.Abs(attackerAd * (One - (defenderReduction / Percentage)));
        }

        public static float CalculateDamage(
            float attackerAd,
            float defenderReduction
        )
        {
            const float Percentage = 100,
                One = 1;

            return Math.Abs(attackerAd * (One - (defenderReduction / Percentage)));
        }

        public static float RemainedHp(float currentHp, float receivedDamage)
        {
            return receivedDamage > currentHp ? GameConstant.Zero : currentHp - receivedDamage;
        }

        public static void InformAction(
            string attackerName,
            string defenderName,
            float inflictedDamage
        )
        {
            Console.WriteLine(GameConstant.AttackMsg, attackerName, inflictedDamage, defenderName);
        }

        public static bool Probability(float probability)
        {
            const int MaxProbability = 100;
            
            Random random = new();

            return Check.InRange(random.Next(MaxProbability), probability);
        }

        public static void NoticeOnCoolDown(int RemainingCD)
        {
            Console.WriteLine(GameConstant.OnCooldown, RemainingCD);
        }
        public static void heal(float healAmount, float[,] stats) 
        {

            if (Check.GreaterThan(Stats.GetCurrentHp(stats)))
            {
                if(Check.InRange(Stats.GetCurrentHp(stats) + healAmount, Stats.GetMaxHp(stats)))
                {
                    stats[GameConstant.RowCurrentValues,GameConstant.HpValueColumn] += healAmount;
                }
                else
                {
                    stats[GameConstant.RowCurrentValues, GameConstant.HpValueColumn] = Stats.GetMaxHp(stats);
                }
            }
 
        }
    }

    public class Msg
    {
        public static void ErrorCommand(ref bool moreAttempts ,ref int attempts ,string outOfAttempts)
        {
            attempts--;
            moreAttempts = Check.GreaterThan(attempts);
            Console.WriteLine(
                moreAttempts
                    ? GameConstant.ErrorMsg
                    : outOfAttempts
            );
        }
        
        public static void CurrentHp(float[] Hp, string[] names)
        {

        }
        public static void ValidateInput(ref int remainingAttempts, ref bool hasMoreAttempts, bool validInput, string ErrorOutOfAttemptsMsg )
        {
            if (!validInput)
            {
                remainingAttempts--;
                hasMoreAttempts = Check.GreaterThan(remainingAttempts);
                Console.WriteLine(
                    hasMoreAttempts ? GameConstant.ErrorMsg : ErrorOutOfAttemptsMsg
                );
            }
            else
            {
                remainingAttempts = GameConstant.MaxAttempts;
                hasMoreAttempts = Check.GreaterThan(remainingAttempts);
            }
            
        }
        
    }
}
