using System;
using Checkers;
using GameConstants;
using Rpg;
using Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace Remake
{
    public class Game
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
                    GameConstant.ArcherMinHp,
                    GameConstant.ArcherMinAttack,
                    GameConstant.ArcherMinReduction
                },
                {
                    GameConstant.ArcherMaxHp,
                    GameConstant.ArcherMaxAttack,
                    GameConstant.ArcherMaxReduction
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
            int monsterCurrentStun = GameConstant.Zero;
            float monsterCurrentHp,
                monsterAttack,
                monsterReduction;
            float[,] monster = new float[3, 3]
            {
                {
                    GameConstant.MonsterMinHp,
                    GameConstant.MonsterMinAttack,
                    GameConstant.MonsterMinReduction
                },
                {
                    GameConstant.MonsterMaxHp,
                    GameConstant.MonsterMaxAttack,
                    GameConstant.MonsterMaxReduction
                },
                { GameConstant.Zero, GameConstant.Zero, GameConstant.Zero }
            };
            #endregion


            #region PorgramVariables
            string userCommand;
            string[] twoValidInputs = [GameConstant.OneStr, GameConstant.ZeroStr],
                threeValidInputs =
                    [GameConstant.OneStr, GameConstant.TwoStr, GameConstant.ThreeStr],
                fourValidInputs =

                    [
                        GameConstant.OneStr,
                        GameConstant.TwoStr,
                        GameConstant.ThreeStr,
                        GameConstant.FourStr
                    ],
                StatsRequirementMsg =

                    [
                        GameConstant.HpMenuMsg + GameConstant.RangedInMsg,
                        GameConstant.AttackMenuMsg + GameConstant.RangedInMsg,
                        GameConstant.DmgReduccionMenuMsg + GameConstant.RangedInMsg
                    ],
                boolValidInputs = [GameConstant.Yes, GameConstant.No];
            int difficulty = GameConstant.Four,
                remainingAttempts = GameConstant.MaxAttempts,
                roundCouter = GameConstant.One;
            float inflictedDamage,
                userInsertedStatsValue;
            bool validInput,
                isHero = true,
                hasRemainingAttemptsMenu = true,
                hasRemainingAttempts = true,
                criticalAttack,
                failedAttack;

            #endregion


            Console.WriteLine(GameConstant.MenuMsg);

            //Start Menu
            do
            {
                userCommand = Console.ReadLine() ?? "";
                validInput = Check.ValidateInput(userCommand, twoValidInputs);
                if (!validInput)
                {
                    remainingAttempts--;
                    hasRemainingAttemptsMenu = Check.GreaterThan(remainingAttempts);
                    Console.WriteLine(
                        hasRemainingAttemptsMenu ? GameConstant.ErrorMsg : GameConstant.ErrorEndMsg
                    );
                }
                else
                {
                    remainingAttempts = GameConstant.MaxAttempts;
                    hasRemainingAttemptsMenu = Check.GreaterThan(remainingAttempts);
                }
            } while (!validInput && hasRemainingAttemptsMenu);

            if (userCommand.Equals(GameConstant.OneStr))
            {
                Console.WriteLine(GameConstant.DifficultyMenuMsg);

                //difficulty Selector
                do
                {
                    userCommand = Console.ReadLine() ?? "";
                    validInput = Check.ValidateInput(userCommand, fourValidInputs);

                    if (!validInput)
                    {
                        remainingAttempts--;
                        hasRemainingAttemptsMenu = Check.GreaterThan(remainingAttempts);
                        Console.WriteLine(
                            hasRemainingAttemptsMenu
                                ? GameConstant.ErrorMsg
                                : GameConstant.DefaultDifficultyMsg
                        );
                    }
                    else
                    {
                        remainingAttempts = GameConstant.MaxAttempts;
                        hasRemainingAttemptsMenu = Check.GreaterThan(remainingAttempts);
                        difficulty = Convert.ToInt32(userCommand);
                    }
                } while (!validInput && hasRemainingAttemptsMenu);

                Console.WriteLine(GameConstant.RenameMsg);
                do
                {
                    userCommand = Console.ReadLine() ?? "";
                    validInput = Check.ValidateInput(userCommand, boolValidInputs);
                    if (!validInput)
                    {
                        remainingAttempts--;
                        hasRemainingAttemptsMenu = Check.GreaterThan(remainingAttempts);
                        Console.WriteLine(
                            hasRemainingAttemptsMenu
                                ? GameConstant.ErrorMsg
                                : GameConstant.DefaultDifficultyMsg
                        );
                    }
                    else
                    {
                        remainingAttempts = GameConstant.MaxAttempts;
                        hasRemainingAttemptsMenu = Check.GreaterThan(remainingAttempts);
                    }
                } while (!validInput && hasRemainingAttemptsMenu);

                if (Check.Equals(userCommand, GameConstant.Yes))
                {
                    archerName = Stats.Rename(archerName);
                    monsterName = Stats.Rename(monsterName);
                }
                // Starter Stats Setter
                if (Check.InRange(difficulty, GameConstant.Three))
                {
                    archer = Stats.Setter(archer, difficulty, isHero);
                    monster = Stats.Setter(monster, difficulty, !isHero);
                }
                else
                {
                    //archer
                    Console.WriteLine(GameConstant.RequestValueOfStatsMsg, archerName);
                    for (int i = 0; i < archer.GetLength(GameConstant.One); i++)
                    {
                        do
                        {
                            Console.WriteLine(
                                $"{GameConstant.InsertRequestMsg}\n {StatsRequirementMsg[i]}",
                                archer[GameConstant.MinValueRow, i],
                                archer[GameConstant.MaxValueRow, i]
                            );
                            userInsertedStatsValue = Convert.ToSingle(Console.ReadLine());
                            validInput = Check.InRange(
                                userInsertedStatsValue,
                                archer[GameConstant.MinValueRow, i],
                                archer[GameConstant.MaxValueRow, i]
                            );
                            if (!validInput)
                            {
                                remainingAttempts--;
                                hasRemainingAttempts = Check.GreaterThan(remainingAttempts);

                                if (hasRemainingAttempts)
                                {
                                    Console.WriteLine(GameConstant.ErrorMsg);
                                }
                                else
                                {
                                    Console.WriteLine(GameConstant.DefaultStatsMsg);
                                    archer[GameConstant.RowToSet, i] = archer[
                                        GameConstant.MinValueRow,
                                        i
                                    ];
                                }
                            }
                            else
                            {
                                remainingAttempts = GameConstant.MaxAttempts;
                                hasRemainingAttemptsMenu = Check.GreaterThan(remainingAttempts);
                                archer[GameConstant.RowToSet, i] = userInsertedStatsValue;
                            }
                        } while (!validInput && hasRemainingAttempts);
                    }
                    //monster
                    Console.WriteLine(GameConstant.RequestValueOfStatsMsg, monsterName);
                    for (int i = 0; i < monster.GetLength(GameConstant.One); i++)
                    {
                        do
                        {
                            Console.WriteLine(
                                $"{GameConstant.InsertRequestMsg}\n {StatsRequirementMsg[i]}",
                                monster[GameConstant.MinValueRow, i],
                                monster[GameConstant.MaxValueRow, i]
                            );
                            userInsertedStatsValue = Convert.ToSingle(Console.ReadLine());
                            validInput = Check.InRange(
                                userInsertedStatsValue,
                                monster[GameConstant.MinValueRow, i],
                                monster[GameConstant.MaxValueRow, i]
                            );
                            if (!validInput)
                            {
                                remainingAttempts--;
                                hasRemainingAttempts = Check.GreaterThan(remainingAttempts);

                                if (hasRemainingAttempts)
                                {
                                    Console.WriteLine(GameConstant.ErrorMsg);
                                }
                                else
                                {
                                    Console.WriteLine(GameConstant.DefaultStatsMsg);
                                    monster[GameConstant.RowToSet, i] = monster[
                                        GameConstant.MinValueRow,
                                        i
                                    ];
                                }
                            }
                            else
                            {
                                remainingAttempts = GameConstant.MaxAttempts;
                                monster[GameConstant.RowToSet, i] = userInsertedStatsValue;
                            }
                        } while (!validInput && hasRemainingAttempts);
                    }
                }

                archerCurrentHp = Stats.GetHp(archer);
                archerAttack = Stats.GetAttack(archer);
                archerReduction = Stats.GetReduction(archer);

                monsterCurrentHp = Stats.GetHp(monster);
                monsterAttack = Stats.GetAttack(monster);
                monsterReduction = Stats.GetReduction(monster);

                if (hasRemainingAttemptsMenu)
                {
                    while (
                        Check.GreaterThan(archerCurrentHp) && Check.GreaterThan(monsterCurrentHp)
                    )
                    {
                        Console.WriteLine(GameConstant.Round, roundCouter);

                        if (Check.GreaterThan(archerCurrentHp))
                        { //Archer Turn
                            Console.WriteLine(GameConstant.RequestCommandMsg);

                            do
                            {
                                userCommand = Console.ReadLine() ?? "";
                                validInput = Check.ValidateInput(userCommand, threeValidInputs);
                                if (!validInput)
                                {
                                    remainingAttempts--;
                                    hasRemainingAttempts = Check.GreaterThan(remainingAttempts);
                                    Console.WriteLine(
                                        hasRemainingAttempts
                                            ? GameConstant.ErrorMsg
                                            : GameConstant.DefaultCommandMsg
                                    );
                                }
                                else
                                {
                                    remainingAttempts = GameConstant.MaxAttempts;
                                }

                                if (
                                    userCommand.Equals(GameConstant.TwoStr)
                                    && Check.GreaterThan(archerAbilityCurrentCD)
                                )
                                {
                                    Console.WriteLine(
                                        GameConstant.OnCooldown,
                                        archerAbilityCurrentCD
                                    );
                                }
                            } while (!validInput && hasRemainingAttempts);

                            if (hasRemainingAttempts)
                            {
                                remainingAttempts = GameConstant.MaxAttempts;
                                do
                                {
                                    switch (userCommand)
                                    {
                                        case "1":
                                            failedAttack = Battle.Probability(
                                                GameConstant.FailedAttackProbabilitty
                                            );
                                            if (!failedAttack)
                                            {
                                                criticalAttack = Battle.Probability(
                                                    GameConstant.CriticalProbability
                                                );
                                                if (criticalAttack)
                                                    Console.WriteLine(
                                                        GameConstant.CriticalAttackMsg,
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
                                                    GameConstant.FailedAttackMsg,
                                                    archerName
                                                );
                                            }

                                            break;
                                        case "2":
                                            monsterCurrentStun = archerAbilityEffect;
                                            archerAbilityCurrentCD = GameConstant.SkillCd;
                                            break;
                                        default:
                                            archerDefenseMode = !archerDefenseMode;
                                            break;
                                    }
                                } while (!validInput && !hasRemainingAttempts);
                            }
                            else
                            {
                                hasRemainingAttempts = !hasRemainingAttempts;
                                remainingAttempts = GameConstant.MaxAttempts;

                                failedAttack = Battle.Probability(
                                                GameConstant.FailedAttackProbabilitty
                                            );
                                if (!failedAttack)
                                {
                                    criticalAttack = Battle.Probability(
                                        GameConstant.CriticalProbability
                                    );
                                    if (criticalAttack)
                                        Console.WriteLine(
                                            GameConstant.CriticalAttackMsg,
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
                                        GameConstant.FailedAttackMsg,
                                        archerName
                                    );
                                }
                            }

                            
                        }

                        if (!Check.GreaterThan(monsterCurrentStun))
                        {
                            failedAttack = Battle.Probability(
                                GameConstant.FailedAttackProbabilitty
                            );
                            if (!failedAttack)
                            {
                                criticalAttack = Battle.Probability(
                                    GameConstant.CriticalProbability
                                );
                                if (criticalAttack)
                                    Console.WriteLine(GameConstant.CriticalAttackMsg, monsterName);
                                inflictedDamage = Battle.CalculateDamage(
                                    monsterAttack,
                                    archerReduction,
                                    GameConstant.GuardEffect,
                                    archerDefenseMode,
                                    criticalAttack
                                );
                                Battle.InformAction(monsterName, archerName, inflictedDamage);
                                archerCurrentHp = Battle.RemainedHp(
                                    archerCurrentHp,
                                    inflictedDamage
                                );
                            }
                            else
                            {
                                Console.WriteLine(GameConstant.FailedAttackMsg, monsterName);
                            }
                        }

                        Console.WriteLine(GameConstant.CurrentStatus, archerName, archerCurrentHp);
                        Console.WriteLine(
                            GameConstant.CurrentStatus,
                            monsterName,
                            monsterCurrentHp
                        );

                        //End of Round
                        monsterCurrentStun--;
                        archerAbilityCurrentCD--;
                    }
                }
            }

            if (userCommand.Equals(GameConstant.Zero))
                Console.WriteLine(GameConstant.EndMsg);
        }
    }
}
