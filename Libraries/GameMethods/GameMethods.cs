using System;
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
            const int CriticalEffect = 2;
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

        public static void NoticeOnCoolDown(int RemainingCD)
        {
            Console.WriteLine(GameConstant.OnCooldown, RemainingCD);
        }
    }

    public class Msg
    {
        public static void ErrorCommand(ref bool moreAttemps ,ref int attemps ,string oufOfAttemps)
        {
            attemps--;
            moreAttemps = Check.GreaterThan(attemps);
            Console.WriteLine(
                moreAttemps
                    ? GameConstant.ErrorMsg
                    : oufOfAttemps
            );
        }

        /*public static void ValidateInput(ref int remainingAttempts, ref bool hasMoreAttempts, bool validInput, string ErrorOutOfAttemptsMSg)
        {
            remainingAttempts--;
            hasMoreAttempts = Check.GreaterThan(remainingAttempts);
            Console.WriteLine(
                hasMoreAttempts
                    ? GameConstant.ErrorMsg
                    : ErrorOutOfAttemptsMSg
            );
        }*/
        public static void ValidateInput(ref int remainingAttempts, ref bool hasMoreAttempts, bool validInput, string ErrorOutOfAttemptsMSg )
        {
            if (!validInput)
            {
                remainingAttempts--;
                hasMoreAttempts = Check.GreaterThan(remainingAttempts);
                Console.WriteLine(
                    hasMoreAttempts ? GameConstant.ErrorMsg : ErrorOutOfAttemptsMSg
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
