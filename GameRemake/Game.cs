using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
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
            bool archerDefenseMode = false,
                archerAlive = true;
            #endregion
            #region barbarian
            string barbarianName = GameConstant.BarbarianName;
            int barbarianAbilityCurrentCD = GameConstant.Zero;
            int barbarianAbilityEffect = GameConstant.Hundred,
                barbarianAbilityDuration = GameConstant.Two;
            float barbarianCurrentHp = GameConstant.Zero,
                barbarianCurrentAttack = GameConstant.Zero,
                barbarianCurrentReduction = GameConstant.Zero;
            float[,] barbarian = new float[4, 3]
            {
                {
                    GameConstant.BarbarianSystemLimitMinHp,
                    GameConstant.BarbarianSystemLimitMinAttack,
                    GameConstant.BarbarianSystemLimitMinReduction
                },
                {
                    GameConstant.BarbarianSystemLimitMaxHp,
                    GameConstant.BarbarianSystemLimitMaxAttack,
                    GameConstant.BarbarianSystemLimitMaxReduction
                },
                { GameConstant.Zero, GameConstant.Zero, GameConstant.Zero },
                { barbarianCurrentHp, barbarianCurrentAttack, barbarianCurrentReduction }
            };

            bool barbarianDefenseMode = false,
                barbarianAlive = true;
            #endregion
            #region mage
            string mageName = GameConstant.MageName;
            int mageAbilityCurrentCD = GameConstant.Zero;
            int mageAbilityEffect = GameConstant.Two;
            float mageCurrentHp = GameConstant.Zero,
                mageCurrentAttack = GameConstant.Zero,
                mageCurrentReduction = GameConstant.Zero;
            float[,] mage = new float[4, 3]
            {
                {
                    GameConstant.MageSystemLimitMinHp,
                    GameConstant.MageSystemLimitMinAttack,
                    GameConstant.MageSystemLimitMinReduction
                },
                {
                    GameConstant.MageSystemLimitMaxHp,
                    GameConstant.MageSystemLimitMaxAttack,
                    GameConstant.MageSystemLimitMaxReduction
                },
                { GameConstant.Zero, GameConstant.Zero, GameConstant.Zero },
                { mageCurrentHp, mageCurrentAttack, mageCurrentReduction }
            };

            bool mageDefenseMode = false,
                mageAlive = true;
            #endregion
            #region druid
            string druidName = GameConstant.DruidName;
            int druidAbilityCurrentCD = GameConstant.Zero;
            int druidAbilityEffect = GameConstant.FiveHundred;
            float druidCurrentHp = GameConstant.Zero,
                druidCurrentAttack = GameConstant.Zero,
                druidCurrentReduction = GameConstant.Zero;
            float[,] druid = new float[4, 3]
            {
                {
                    GameConstant.DruidSystemLimitMinHp,
                    GameConstant.DruidSystemLimitMinAttack,
                    GameConstant.DruidSystemLimitMinReduction
                },
                {
                    GameConstant.DruidSystemLimitMaxHp,
                    GameConstant.DruidSystemLimitMaxAttack,
                    GameConstant.DruidSystemLimitMaxReduction
                },
                { GameConstant.Zero, GameConstant.Zero, GameConstant.Zero },
                { druidCurrentHp, druidCurrentAttack, druidCurrentReduction }
            };

            bool druidDefenseMode = false,
                druidAlive = true;
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

            bool monsterAlive = true;
            #endregion


            #region PorgramVariables
            string userCommand,
                difficulty = GameConstant.TwoStr;

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
            int remainingAttempts = GameConstant.MaxAttempts,
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
                Msg.ValidateInput(
                    ref remainingAttempts,
                    ref hasRemainingAttemptsMenu,
                    validInput,
                    GameConstant.ErrorEndMsg
                );
            } while (!validInput && hasRemainingAttemptsMenu);

            if (userCommand.Equals(GameConstant.OneStr))
            {
                Console.WriteLine(GameConstant.DifficultyMenuMsg);

                //difficulty Selector
                do
                {
                    userCommand = Console.ReadLine() ?? "";
                    validInput = Check.ValidateInput(userCommand, fourValidInputs);
                    Msg.ValidateInput(
                        ref remainingAttempts,
                        ref hasRemainingAttemptsMenu,
                        validInput,
                        GameConstant.ErrorEndMsg
                    );
                    difficulty = userCommand;
                } while (!validInput && hasRemainingAttemptsMenu);
                //Rename
                if (hasRemainingAttemptsMenu)
                {
                    Console.WriteLine(GameConstant.RenameMsg);
                    do
                    {
                        userCommand = Console.ReadLine() ?? "";
                        validInput = Check.ValidateInput(userCommand, boolValidInputs);
                        Msg.ValidateInput(
                            ref remainingAttempts,
                            ref hasRemainingAttemptsMenu,
                            validInput,
                            GameConstant.ErrorEndMsg
                        );
                    } while (!validInput && hasRemainingAttemptsMenu);
                }

                if (userCommand.ToUpper().Equals(GameConstant.Yes))
                {
                    string oldName = archerName;
                    Console.WriteLine(GameConstant.RequestNameMsg, archerName);
                    archerName = Utility.NameMayus(Console.ReadLine() ?? archerName);
                    Console.WriteLine(GameConstant.RenamedMsg, oldName, archerName);

                    oldName = barbarianName;
                    Console.WriteLine(GameConstant.RequestNameMsg, barbarianName);
                    barbarianName = Utility.NameMayus(Console.ReadLine() ?? barbarianName);
                    Console.WriteLine(GameConstant.RenamedMsg, oldName, barbarianName);

                    oldName = mageName;
                    Console.WriteLine(GameConstant.RequestNameMsg, mageName);
                    mageName = Utility.NameMayus(Console.ReadLine() ?? mageName);
                    Console.WriteLine(GameConstant.RenamedMsg, oldName, mageName);

                    oldName = druidName;
                    Console.WriteLine(GameConstant.RequestNameMsg, druidName);
                    druidName = Utility.NameMayus(Console.ReadLine() ?? druidName);
                    Console.WriteLine(GameConstant.RenamedMsg, oldName, druidName);

                    oldName = monsterName;
                    Console.WriteLine(GameConstant.RequestNameMsg, monsterName);
                    monsterName = Utility.NameMayus(Console.ReadLine() ?? monsterName);
                    Console.WriteLine(GameConstant.RenamedMsg, oldName, monsterName);
                }

                // Starter Stats SetMax
                if (difficulty.Equals(GameConstant.DifficultyPersonalized))
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
                else
                {
                    Stats.SetPlayerCap(archer, difficulty, isHero);
                    Stats.SetPlayerCap(barbarian, difficulty, isHero);
                    Stats.SetPlayerCap(mage, difficulty, isHero);
                    Stats.SetPlayerCap(druid, difficulty, isHero);
                    Stats.SetPlayerCap(monster, difficulty, !isHero);
                }

                if (hasRemainingAttemptsMenu)
                {
                    Stats.SetInGame(archer);
                    Stats.SetInGame(barbarian);
                    Stats.SetInGame(mage);
                    Stats.SetInGame(druid);
                    Stats.SetInGame(monster);

                    while (
                        ((archerAlive || barbarianAlive || mageAlive || druidAlive) && monsterAlive)
                    )
                    {
                        archerAlive = Check.GreaterThan(Stats.GetCurrentHp(archer));
                        barbarianAlive = Check.GreaterThan(Stats.GetCurrentHp(barbarian));
                        mageAlive = Check.GreaterThan(Stats.GetCurrentHp(mage));
                        druidAlive = Check.GreaterThan(Stats.GetCurrentHp(druid));

                        float[] CurrentsHp =
                        {
                            Stats.GetCurrentHp(archer),
                            Stats.GetCurrentHp(barbarian),
                            Stats.GetCurrentHp(mage),
                            Stats.GetCurrentHp(druid),
                            Stats.GetCurrentHp(monster)
                        };

                        string[] names =
                        {
                            archerName,
                            barbarianName,
                            mageName,
                            druidName,
                            monsterName
                        };

                        Console.WriteLine(GameConstant.Round, roundCouter);

                        //Archer Turn
                        if (archerAlive && monsterAlive)
                        {
                            Console.WriteLine(GameConstant.RequestCommandMsg, archerName);

                            do
                            {
                                userCommand = Console.ReadLine() ?? "";
                                validInput = Check.ValidateInput(userCommand, threeValidInputs);
                                Msg.ValidateInput(
                                    ref remainingAttempts,
                                    ref hasRemainingAttempts,
                                    validInput,
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
                                ;
                            } while (!validInput && hasRemainingAttempts);

                            remainingAttempts = GameConstant.MaxAttempts;

                            if (hasRemainingAttempts)
                            {
                                switch (userCommand)
                                {
                                    case "1":
                                        Battle.Attack(archer, monster, archerName, monsterName);
                                        break;
                                    case "2":
                                        monsterRemainingStunnedTurn = archerAbilityEffect;
                                        archerAbilityCurrentCD = GameConstant.SkillCd;
                                        Console.WriteLine(
                                            GameConstant.ArcherAbility,
                                            archerName,
                                            monsterName,
                                            archerAbilityEffect
                                        );
                                        break;
                                    default:
                                        archerDefenseMode = !archerDefenseMode;
                                        break;
                                }
                            }
                            else
                            {
                                hasRemainingAttempts = !hasRemainingAttempts;
                                Battle.Attack(archer, monster, archerName, monsterName);
                            }
                        }
                        remainingAttempts = GameConstant.MaxAttempts;
                        monsterAlive = Check.GreaterThan(Stats.GetCurrentHp(monster));

                        //barbarian Turn
                        if (barbarianAlive && monsterAlive)
                        {
                            Console.WriteLine(GameConstant.RequestCommandMsg, barbarianName);
                            do
                            {
                                userCommand = Console.ReadLine() ?? "";
                                validInput = Check.ValidateInput(userCommand, threeValidInputs);
                                Msg.ValidateInput(
                                    ref remainingAttempts,
                                    ref hasRemainingAttempts,
                                    validInput,
                                    GameConstant.DefaultCommandMsg
                                );

                                if (
                                    userCommand.Equals(GameConstant.TwoStr)
                                    && Check.GreaterThan(barbarianAbilityCurrentCD)
                                )
                                {
                                    Battle.NoticeOnCoolDown(barbarianAbilityCurrentCD);
                                    validInput = !validInput;
                                }
                                ;
                            } while (!validInput && hasRemainingAttempts);

                            if (hasRemainingAttempts)
                            {
                                switch (userCommand)
                                {
                                    case "1":
                                        Battle.Attack(
                                            barbarian,
                                            monster,
                                            barbarianName,
                                            monsterName
                                        );
                                        break;
                                    case "2":
                                        barbarian[
                                            GameConstant.RowCurrentValues,
                                            GameConstant.ReductionValueColumn
                                        ] = barbarianAbilityEffect;
                                        barbarianAbilityCurrentCD = GameConstant.SkillCd;
                                        Console.WriteLine(
                                            GameConstant.BarbarianAbility,
                                            barbarianName,
                                            GameConstant.BarbarianSkillDuration
                                        );
                                        break;
                                    default:
                                        barbarianDefenseMode = !barbarianDefenseMode;
                                        break;
                                }
                            }
                            else
                            {
                                hasRemainingAttempts = !hasRemainingAttempts;

                                Battle.Attack(barbarian, monster, barbarianName, monsterName);
                            }
                        }
                        remainingAttempts = GameConstant.MaxAttempts;
                        monsterAlive = Check.GreaterThan(Stats.GetCurrentHp(monster));

                        //mage turn
                        if (mageAlive && monsterAlive)
                        {
                            Console.WriteLine(GameConstant.RequestCommandMsg, mageName);
                            do
                            {
                                userCommand = Console.ReadLine() ?? "";
                                validInput = Check.ValidateInput(userCommand, threeValidInputs);
                                Msg.ValidateInput(
                                    ref remainingAttempts,
                                    ref hasRemainingAttempts,
                                    validInput,
                                    GameConstant.DefaultCommandMsg
                                );

                                if (
                                    userCommand.Equals(GameConstant.TwoStr)
                                    && Check.GreaterThan(mageAbilityCurrentCD)
                                )
                                {
                                    Battle.NoticeOnCoolDown(mageAbilityCurrentCD);
                                    validInput = !validInput;
                                }
                                ;
                            } while (!validInput && hasRemainingAttempts);

                            if (hasRemainingAttempts)
                            {
                                switch (userCommand)
                                {
                                    case "1":
                                        Battle.Attack(mage, monster, mageName, monsterName);
                                        break;
                                    case "2":
                                        mageCurrentReduction = mageAbilityEffect;
                                        mageAbilityCurrentCD = GameConstant.SkillCd;
                                        float damege =
                                            Battle.CalculateDamage(
                                                Stats.GetCurrentAttack(mage),
                                                Stats.GetCurrentReduction(monster)
                                            ) * mageAbilityEffect;
                                        monster[
                                            GameConstant.RowCurrentValues,
                                            GameConstant.HpValueColumn
                                        ] -= damege;
                                        Console.WriteLine(
                                            GameConstant.MageAbility,
                                            mageName,
                                            damege,
                                            monsterName
                                        );
                                        break;
                                    default:
                                        mageDefenseMode = !mageDefenseMode;
                                        break;
                                }
                            }
                            else
                            {
                                hasRemainingAttempts = !hasRemainingAttempts;

                                Battle.Attack(mage, monster, mageName, monsterName);
                            }
                        }
                        remainingAttempts = GameConstant.MaxAttempts;
                        monsterAlive = Check.GreaterThan(Stats.GetCurrentHp(monster));

                        //druid turn
                        if (druidAlive && monsterAlive)
                        {
                            Console.WriteLine(GameConstant.RequestCommandMsg, druidName);
                            do
                            {
                                userCommand = Console.ReadLine() ?? "";
                                validInput = Check.ValidateInput(userCommand, threeValidInputs);
                                Msg.ValidateInput(
                                    ref remainingAttempts,
                                    ref hasRemainingAttempts,
                                    validInput,
                                    GameConstant.DefaultCommandMsg
                                );

                                if (
                                    userCommand.Equals(GameConstant.TwoStr)
                                    && Check.GreaterThan(druidAbilityCurrentCD)
                                )
                                {
                                    Battle.NoticeOnCoolDown(druidAbilityCurrentCD);
                                    validInput = !validInput;
                                }
                                ;
                            } while (!validInput && hasRemainingAttempts);

                            if (hasRemainingAttempts)
                            {
                                switch (userCommand)
                                {
                                    case "1":
                                        Battle.Attack(druid, monster, druidName, monsterName);
                                        break;
                                    case "2":
                                        druidCurrentReduction = druidAbilityEffect;
                                        druidAbilityCurrentCD = GameConstant.SkillCd;
                                        if (Check.GreaterThan(Stats.GetCurrentHp(archer)))
                                        {
                                            Console.WriteLine(
                                                GameConstant.DruidAbility,
                                                druidName,
                                                archerName,
                                                druidAbilityEffect
                                            );
                                            Battle.heal(druidAbilityEffect, archer);
                                        }

                                        if (Check.GreaterThan(Stats.GetCurrentHp(barbarian)))
                                        {
                                            Console.WriteLine(
                                                GameConstant.DruidAbility,
                                                druidName,
                                                barbarianName,
                                                druidAbilityEffect
                                            );
                                            Battle.heal(druidAbilityEffect, barbarian);
                                        }

                                        if (Check.GreaterThan(Stats.GetCurrentHp(mage)))
                                        {
                                            Console.WriteLine(
                                                GameConstant.DruidAbility,
                                                druidName,
                                                mageName,
                                                druidAbilityEffect
                                            );
                                            Battle.heal(druidAbilityEffect, mage);
                                        }
                                        
                                        Console.WriteLine(
                                            GameConstant.DruidAbility,
                                            druidName,
                                            druidName,
                                            druidAbilityEffect
                                        );
                                        Battle.heal(druidAbilityEffect, druid);
                                        break;
                                    default:
                                        druidDefenseMode = !druidDefenseMode;
                                        break;
                                }
                            }
                            else
                            {
                                hasRemainingAttempts = !hasRemainingAttempts;

                                Battle.Attack(druid, monster, druidName, monsterName);
                            }
                        }
                        remainingAttempts = GameConstant.MaxAttempts;
                        monsterAlive = Check.GreaterThan(Stats.GetCurrentHp(monster));

                        if (
                            !Check.GreaterThan(monsterRemainingStunnedTurn)
                            && Check.GreaterThan(Stats.GetCurrentHp(monster))
                        )
                        {
                            Console.WriteLine(GameConstant.MonsterTurnMsg, monsterName);
                            if (Check.GreaterThan(Stats.GetCurrentHp(archer)))
                            {
                                Battle.Attack(
                                    monster,
                                    archer,
                                    monsterName,
                                    archerName,
                                    archerDefenseMode
                                );
                            }

                            if (Check.GreaterThan(Stats.GetCurrentHp(barbarian)))
                            {
                                Battle.Attack(
                                    monster,
                                    barbarian,
                                    monsterName,
                                    barbarianName,
                                    barbarianDefenseMode
                                );
                            }

                            if (Check.GreaterThan(Stats.GetCurrentHp(mage)))
                            {
                                Battle.Attack(
                                    monster,
                                    mage,
                                    monsterName,
                                    mageName,
                                    mageDefenseMode
                                );
                            }

                            if (Check.GreaterThan(Stats.GetCurrentHp(druid)))
                            {
                                Battle.Attack(
                                    monster,
                                    druid,
                                    monsterName,
                                    druidName,
                                    druidDefenseMode
                                );
                            }
                        }
                        else
                        {
                            Console.WriteLine(GameConstant.MonsterStunnedMsg, monsterName);
                        }

                        Console.WriteLine(
                            GameConstant.CurrentStatus,
                            archerName,
                            Stats.GetCurrentHp(archer)
                        );
                        Console.WriteLine(
                            GameConstant.CurrentStatus,
                            barbarianName,
                            Stats.GetCurrentHp(barbarian)
                        );
                        Console.WriteLine(
                            GameConstant.CurrentStatus,
                            mageName,
                            Stats.GetCurrentHp(mage)
                        );
                        Console.WriteLine(
                            GameConstant.CurrentStatus,
                            druidName,
                            Stats.GetCurrentHp(druid)
                        );
                        Console.WriteLine(
                            GameConstant.CurrentStatus,
                            monsterName,
                            Stats.GetCurrentHp(monster)
                        );

                        //End of Round

                        monsterRemainingStunnedTurn--;
                        archerAbilityCurrentCD--;
                        archerDefenseMode = false;
                        barbarianAbilityCurrentCD--;
                        barbarianDefenseMode = false;
                        barbarianAbilityDuration--;
                        mageAbilityCurrentCD--;
                        mageDefenseMode = false;
                        druidAbilityCurrentCD--;
                        druidDefenseMode = false;
                        roundCouter++;
                        if ( barbarianAbilityDuration== 0)
                        {
                            barbarian[
                                GameConstant.RowCurrentValues,
                                GameConstant.ReductionValueColumn
                            ] = barbarian[GameConstant.MaxStatsRow, GameConstant.ReductionValueColumn];
                        }
                    }
                    
                }

                if (userCommand.Equals(GameConstant.Zero))
                    Console.WriteLine(GameConstant.EndMsg);
            }
        }
    }
}
