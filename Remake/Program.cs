using System;
using Rpg;
using RpgConstants;
using Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace Remake
{
    public class Program
    {
        static void Main()
        {
            #region archerStats
            //sample
            string archerName = "Archer";
            int archerAbilityCurrentCD = 0;
            int archerAbilityEffect = 2;
            float archerCurrentHp,
                archerAttack,
                archerReduction;
            float[,] archer = new float[3, 3]
            {
                {
                    RpgConstant.ArcherMinHp,
                    RpgConstant.ArcherMinAttack,
                    RpgConstant.ArcherMinReduction
                },
                {
                    RpgConstant.ArcherMaxHp,
                    RpgConstant.ArcherMaxAttack,
                    RpgConstant.ArcherMaxReduction
                },
                { 0, 0, 0 }
            }; /*Matrix 3X3: 
               *1st row for min values
               *2nd roe for max values
               *3rd row for assignation;
              */
            /*The 3rd row contain character variables in the following order:
             * life,            [2,0]
             * attack and       [2,1]
             * damage reduction [2,2]
             */
            bool archerDefenseMode = false;
            #endregion

            #region MonsterStats
            string monsterName = "Monster";
            int monsterCurrentStun = RpgConstant.Zero;
            float monsterCurrentHp,
                monsterAttack,
                monsterReduction;
            float[,] monster = new float[3, 3]
            {
                {
                    RpgConstant.MonsterMinHp,
                    RpgConstant.MonsterMinAttack,
                    RpgConstant.MonsterMinReduction
                },
                {
                    RpgConstant.MonsterMaxHp,
                    RpgConstant.MonsterMaxAttack,
                    RpgConstant.MonsterMaxReduction
                },
                { RpgConstant.Zero, RpgConstant.Zero, RpgConstant.Zero }
            };
            #endregion


            #region PorgramVariables
            string userInput;
            string[] twoValidInputs = [RpgConstant.OneStr, RpgConstant.ZeroStr],
                threeValidInputs = [RpgConstant.OneStr, RpgConstant.TwoStr, RpgConstant.ThreeStr],
                fourValidInputs =

                    [
                        RpgConstant.OneStr,
                        RpgConstant.TwoStr,
                        RpgConstant.ThreeStr,
                        RpgConstant.FourStr
                    ],
                boolValidInputs = [RpgConstant.Yes, RpgConstant.No];
            int difficulty = RpgConstant.Four,
                attemps = RpgConstant.Three,
                roundCouter = RpgConstant.One;
            float inflictedDamage;
            bool inputCheck,
                isHero = true,
                hasRemainingAttempts = true,
                criticalAttack,
                failedAttack;

            #endregion


            Console.WriteLine(RpgConstant.MenuMsg);
            do
            {
                userInput = Console.ReadLine() ?? "";
                inputCheck = Utility.ValidateInput(userInput, twoValidInputs);
                if (!inputCheck)
                    Console.WriteLine(RpgConstant.ErrorMsg);
            } while (!inputCheck);

            if (userInput.Equals(RpgConstant.OneStr))
            {
                Console.WriteLine(RpgConstant.DifficultyMenuMsg);
                attemps = RpgConstant.Three;
                do
                {
                    userInput = Console.ReadLine() ?? "";
                    inputCheck = Utility.ValidateInput(userInput, fourValidInputs);
                    if (!inputCheck)
                    {
                        attemps--;
                        hasRemainingAttempts = Utility.GreaterThan(attemps);
                    }
                    else
                    {
                        difficulty = Convert.ToInt32(userInput);
                    }

                    if (!hasRemainingAttempts)
                    {
                        Console.WriteLine(RpgConstant.DefaultDifficultyMsg);
                    }
                    else
                    {
                        Console.WriteLine(RpgConstant.ErrorMsg);
                    }
                } while (!inputCheck && hasRemainingAttempts);

                Console.WriteLine(RpgConstant.RenameMsg);
                attemps = RpgConstant.Three;
                do
                {
                    userInput = Console.ReadLine()?.ToUpper() ?? "";
                    inputCheck = Utility.ValidateInput(userInput, boolValidInputs);
                    if (!inputCheck)
                    {
                        Console.WriteLine(RpgConstant.ErrorMsg);
                        attemps--;
                        hasRemainingAttempts = Utility.GreaterThan(attemps);
                    }
                } while (!inputCheck && hasRemainingAttempts);

                if (userInput == RpgConstant.Yes)
                {
                    archerName = SetStat.Rename(archerName);
                    monsterName = SetStat.Rename(monsterName);
                }
                if (Utility.InRange(difficulty, RpgConstant.Three))
                {
                    archer = SetStat.StatSetter(archer, difficulty, isHero);
                    monster = SetStat.StatSetter(monster, difficulty, !isHero);
                }
                else { }

                archerCurrentHp = GetStat.Hp(archer);
                archerAttack = GetStat.Attack(archer);
                archerReduction = GetStat.Reduction(archer);

                monsterCurrentHp = GetStat.Hp(monster);
                monsterAttack = GetStat.Attack(monster);
                monsterReduction = GetStat.Reduction(monster);

                do
                {
                    Console.WriteLine(RpgConstant.Round, roundCouter);

                    if (Utility.GreaterThan(archerCurrentHp))
                    { //Archer Turn
                        Console.WriteLine(RpgConstant.RequestCommandMsg);
                        userInput = Console.ReadLine() ?? "";
                        inputCheck = Utility.ValidateInput(userInput, fourValidInputs);
                        attemps = RpgConstant.Three;
                        do
                        {
                            if (!inputCheck)
                            {
                                Console.WriteLine(RpgConstant.ErrorMsg);
                                attemps--;
                                hasRemainingAttempts = Utility.GreaterThan(attemps);
                            }
                            if (
                                userInput.Equals(RpgConstant.TwoStr)
                                && Utility.GreaterThan(archerAbilityCurrentCD)
                            )
                            {
                                Console.WriteLine(RpgConstant.OnCooldown, archerAbilityCurrentCD);
                            }
                        } while (!inputCheck && hasRemainingAttempts);

                        if (hasRemainingAttempts)
                        {
                            attemps = RpgConstant.Three;
                            do
                            {
                                switch (userInput)
                                {
                                    case "1":
                                        failedAttack = Battle.Probability(
                                            RpgConstant.FailedAttackProbabilitty
                                        );
                                        if (!failedAttack)
                                        {
                                            criticalAttack = Battle.Probability(
                                                RpgConstant.CriticalProbability
                                            );
                                            if (criticalAttack)
                                                Console.WriteLine(
                                                    RpgConstant.CriticalAttackMsg,
                                                    archerName
                                                );
                                            inflictedDamage = Battle.CalculateDamage(
                                                archerAttack,
                                                monsterReduction,
                                                criticalAttack
                                            );
                                            Battle.InformAction(
                                                archerName,
                                                monsterName,
                                                inflictedDamage
                                            );
                                            monsterCurrentHp = Battle.RemainedHp(
                                                monsterCurrentHp,
                                                inflictedDamage
                                            );
                                        }
                                        else
                                        {
                                            Console.WriteLine(
                                                RpgConstant.FailedAttackMsg,
                                                archerName
                                            );
                                        }

                                        break;
                                    case "2":
                                        monsterCurrentStun = archerAbilityEffect;
                                        archerAbilityCurrentCD = RpgConstant.SkillCd;
                                        break;
                                    default:
                                        archerDefenseMode = !archerDefenseMode;
                                        break;
                                }
                            } while (!inputCheck && !hasRemainingAttempts);
                        }
                    }

                    if (!Utility.GreaterThan(monsterCurrentStun))
                    {
                        failedAttack = Battle.Probability(RpgConstant.FailedAttackProbabilitty);
                        if (!failedAttack)
                        {
                            criticalAttack = Battle.Probability(RpgConstant.CriticalProbability);
                            if (criticalAttack)
                                Console.WriteLine(RpgConstant.CriticalAttackMsg, monsterName);
                            inflictedDamage = Battle.CalculateDamage(
                                monsterAttack,
                                archerReduction,
                                RpgConstant.GuardEffect,
                                archerDefenseMode,
                                criticalAttack
                            );
                            Battle.InformAction(monsterName, archerName, inflictedDamage);
                            archerCurrentHp = Battle.RemainedHp(archerCurrentHp, inflictedDamage);
                        }
                        else
                        {
                            Console.WriteLine(RpgConstant.FailedAttackMsg, monsterName);
                        }
                    }

                    Console.WriteLine(RpgConstant.CurrentStatus, archerName, archerCurrentHp);
                    Console.WriteLine(RpgConstant.CurrentStatus, monsterName, monsterCurrentHp);

                    //End of Round
                    monsterCurrentStun--;
                    archerAbilityCurrentCD--;
                } while (
                    (Utility.GreaterThan(archerCurrentHp)) && Utility.GreaterThan(monsterCurrentHp)
                );
            }
        }
    }
}
