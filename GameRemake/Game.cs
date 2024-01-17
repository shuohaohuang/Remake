using System;
using System.Xml.Linq;
using Checkers;
using GameConstants;
using GameMethods;
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
            string archerName = GameConstant.ArcherName;
            int archerAbilityCurrentCD = GameConstant.Zero;
            int archerAbilityEffect = GameConstant.Two;
            float archerCurrentHp = GameConstant.Zero,
                archerCurrentAttack = GameConstant.Zero,
                archerCurrentReduction = GameConstant.Zero;
            float[,] archer = new float[4, 3]
            {
                {
                    GameConstant.ArcherSystemLimitMinHp,
                    GameConstant.ArcherSystemLimitMinAttack,
                    GameConstant.ArcherSystemLimitMinReduction
                },
                {
                    GameConstant.ArcherSystemLimitMaxHp,
                    GameConstant.ArcherSystemLimitMaxAttack,
                    GameConstant.ArcherSystemLimitMaxReduction
                },
                { GameConstant.Zero, GameConstant.Zero, GameConstant.Zero },
                { archerCurrentHp, archerCurrentAttack, archerCurrentReduction }
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
            string monsterName = GameConstant.MonsterName;
            int monsterRemainingStunnedTurn = GameConstant.Zero;
            float monsterCurrentHp = GameConstant.Zero,
                monsterCurrentAttack = GameConstant.Zero,
                monsterCurrentReduction = GameConstant.Zero;
            float[,] monster = new float[4, 3]
            {
                {
                    GameConstant.MonsterSystemLimitMinHp,
                    GameConstant.MonsterSystemLimitMinAttack,
                    GameConstant.MonsterSystemLimitMinReduction
                },
                {
                    GameConstant.MonsterSystemLimitMaxHp,
                    GameConstant.MonsterSystemLimitMaxAttack,
                    GameConstant.MonsterSystemLimitMaxReduction
                },
                { GameConstant.Zero, GameConstant.Zero, GameConstant.Zero },
                { monsterCurrentHp, monsterCurrentAttack, monsterCurrentReduction },
            };
            #endregion


            #region PorgramVariables
            string userCommand,
                oldName = archerName;

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
                        GameConstant.DmgReductionMenuMsg + GameConstant.RangedInMsg
                    ],
                boolValidInputs = [GameConstant.Yes, GameConstant.No];
            int difficulty = GameConstant.Two,
                remainingAttempts = GameConstant.MaxAttempts,
                roundCouter = GameConstant.One;
            float userInsertedStatsValue;
            bool validInput,
                isHero = true,
                hasRemainingAttemptsMenu = true,
                hasRemainingAttempts = true;

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
                        Msg.ErrorCommand(
                                ref hasRemainingAttemptsMenu,
                                ref remainingAttempts,
                                GameConstant.ErrorEndMsg
                            );
                    }
                    else
                    {
                        remainingAttempts = GameConstant.MaxAttempts;
                        hasRemainingAttemptsMenu = Check.GreaterThan(remainingAttempts);
                        difficulty = Convert.ToInt32(userCommand);
                    }
                } while (!validInput && hasRemainingAttemptsMenu);

                if (hasRemainingAttemptsMenu)
                {
                    Console.WriteLine(GameConstant.RenameMsg);
                    do
                    {
                        userCommand = Console.ReadLine() ?? "";
                        validInput = Check.ValidateInput(userCommand, boolValidInputs);
                        if (!validInput)
                        {
                            Msg.ErrorCommand(
                                ref hasRemainingAttemptsMenu,
                                ref remainingAttempts,
                                GameConstant.DefaultDifficultyMsg
                            );
                        }
                        else
                        {
                            remainingAttempts = GameConstant.MaxAttempts;
                            hasRemainingAttemptsMenu = Check.GreaterThan(remainingAttempts);
                        }
                    } while (!validInput && hasRemainingAttemptsMenu);
                }

                if (Check.Equals(userCommand, GameConstant.Yes))
                {
                    Console.WriteLine(GameConstant.RequestNameMsg, archerName);
                    archerName = Utility.NameMayus(Console.ReadLine() ?? archerName);
                    Console.WriteLine(GameConstant.RenamedMsg, oldName, archerName);

                    oldName = monsterName;
                    Console.WriteLine(GameConstant.RequestNameMsg, monsterName);
                    monsterName = Utility.NameMayus(Console.ReadLine() ?? monsterName);
                    Console.WriteLine(GameConstant.RenamedMsg, oldName, monsterName);
                }

                // Starter Stats SetMax
                if (Check.InRange(difficulty, GameConstant.Three))
                {
                    Stats.SetPlayerCap(archer, difficulty, isHero);
                    Stats.SetPlayerCap(monster, difficulty, !isHero);
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
                                    Console.WriteLine(GameConstant.DefaultHeroStatsMsg);
                                    archer[GameConstant.RowToSetMaxValues, i] = archer[
                                        GameConstant.MinValueRow,
                                        i
                                    ];
                                }
                            }
                            else
                            {
                                remainingAttempts = GameConstant.MaxAttempts;
                                hasRemainingAttempts = Check.GreaterThan(remainingAttempts);
                                archer[GameConstant.RowToSetMaxValues, i] = userInsertedStatsValue;
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
                                    Console.WriteLine(GameConstant.DefaultMonsterStatsMsg);
                                    monster[GameConstant.RowToSetMaxValues, i] = monster[
                                        GameConstant.MaxValueRow,
                                        i
                                    ];
                                }
                            }
                            else
                            {
                                remainingAttempts = GameConstant.MaxAttempts;
                                monster[GameConstant.RowToSetMaxValues, i] = userInsertedStatsValue;
                            }
                        } while (!validInput && hasRemainingAttempts);
                    }
                }
                

                if (hasRemainingAttemptsMenu)
                {
                    Stats.InGame(archer);
                    Stats.InGame(monster);
                    while (
                        Check.GreaterThan(Stats.GetCurrentHp(archer))
                        && Check.GreaterThan(Stats.GetCurrentHp(monster))
                    )
                    {
                        Console.WriteLine(GameConstant.Round, roundCouter);

                        if (Check.GreaterThan(Stats.GetCurrentHp(archer)))
                        { //Archer Turn
                            Console.WriteLine(GameConstant.RequestCommandMsg, archerName);

                            do
                            {
                                userCommand = Console.ReadLine() ?? "";
                                validInput = Check.ValidateInput(userCommand, threeValidInputs);
                                if (!validInput)
                                    Msg.ErrorCommand(
                                        ref hasRemainingAttempts,
                                        ref remainingAttempts,
                                        GameConstant.DefaultCommandMsg
                                    );

                                if (
                                    userCommand.Equals(GameConstant.TwoStr)
                                    && Check.GreaterThan(archerAbilityCurrentCD)
                                )
                                {
                                    Battle.NoticeOnCoolDown(archerAbilityCurrentCD);
                                    validInput = !validInput;
                                }
                            } while (!validInput && hasRemainingAttempts);

                            remainingAttempts = GameConstant.MaxAttempts;

                            if (hasRemainingAttempts)
                            {
                                remainingAttempts = GameConstant.MaxAttempts;
                                do
                                {
                                    switch (userCommand)
                                    {
                                        case "1":
                                            Battle.Attack(archer, monster, archerName, monsterName);
                                            break;
                                        case "2":
                                            monsterRemainingStunnedTurn = archerAbilityEffect;
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

                                Battle.Attack(archer, monster, archerName, monsterName);
                            }
                        }

                        if (
                            !Check.GreaterThan(monsterRemainingStunnedTurn)
                            && Check.GreaterThan(Stats.GetCurrentHp(monster))
                        )
                        {
                            Battle.Attack(
                                monster,
                                archer,
                                monsterName,
                                archerName,
                                archerDefenseMode
                            );
                        }

                        Console.WriteLine(
                            GameConstant.CurrentStatus,
                            archerName,
                            Stats.GetCurrentHp(archer)
                        );
                        Console.WriteLine(
                            GameConstant.CurrentStatus,
                            monsterName,
                            Stats.GetCurrentHp(monster)
                        );

                        //End of Round
                        monsterRemainingStunnedTurn--;
                        archerAbilityCurrentCD--;
                        archerDefenseMode = !archerDefenseMode;
                        roundCouter++;
                    }
                }
            }

            if (userCommand.Equals(GameConstant.Zero))
                Console.WriteLine(GameConstant.EndMsg);
        }
    }
}
