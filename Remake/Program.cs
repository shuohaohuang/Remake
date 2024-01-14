using System;
using Rpg;
using Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace Remake
{
    public class Program
    {
        static void Main()
        {
            const string MenuMsg = "1. Start a new game" +
                                "\n0. Exit",
                        DifficultyMenuMsg = "Choose the difficulty:" +
                                        "\n\t1.Easy: highest stats for heroes, lowest stats for monster" +
                                        "\n\t2.Difficult: lowest stats for heroes, highest stats for monster" +
                                        "\n\t3.Personalized: personalize your heroes attributes and monster " +
                                        "\n\t4.RandomStats: Is the goddess of luck smiling upon you? ",
                        RequestCommandMsg = "Insert {0}'s action" +
                                        "\n\t1.Normal attack" +
                                        "\n\t2. Character's ability" +
                                        "\n\t3. Guard ",
                        CurrentStatus = "{0} : {1} Hp",
                        OnCooldown = "Skill on Cooldown, {0} turns until available ",
                        ErrorMsg = "Wrong insert, try again",
                        RenameMsg = "Do you want rename characters:\n[Y/N]",
                        Yes = "Y", No = "N";

            const string ZeroStr = "0", OneStr = "1", TwoStr = "2", ThreeStr = "3", FourStr = "4";
            const int Zero = 0, One = 1, Two = 2, Three = 3, SkillCd = 5;
            const float ArcherMinHp = 1000f, ArcherMinAttack = 200f, ArcherMinReduction = 25f,
                        ArcherMaxHp = 2000f, ArcherMaxAttack = 300f, ArcherMaxReduction = 35f,
                        MonsterMinHp = 7000f, MonsterMinAttack = 300f, MonsterMinReduction = 20f,
                        MonsterMaxHp = 10000f, MonsterMaxAttack = 400f, MonsterMaxReduction = 30f,
                        GuardEffect = 2;


            #region archerStats
            //sample
            string archerName = "Archer";
            int archerAbilityCurrentCD = 0;
            int archerAbilityEffect = 2;
            float archerHp,
                  archerAttack,
                  archerReduction;
            float[,] archer = new float[3, 3]
            {
                { ArcherMinHp, ArcherMinAttack, ArcherMinReduction },
                { ArcherMaxHp, ArcherMaxAttack, ArcherMaxReduction },
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
            int monsterCurrentStun = Zero;
            float monsterHp,
                  monsterAttack,
                  monsterReduction;
            float[,] monster = new float[3, 3]
            {
                { MonsterMinHp, MonsterMinAttack, MonsterMinReduction },
                { MonsterMaxHp, MonsterMaxAttack, MonsterMaxReduction },
                { Zero, Zero, Zero }
            };
            #endregion


            #region PorgramVariables
            string userInput;
            string[] twoValidInputs = { OneStr, ZeroStr },
                     threeValidInputs = { OneStr, TwoStr, ThreeStr },
                     fourValidInputs = { OneStr, TwoStr, ThreeStr, FourStr },
                     boolValidInputs = { Yes, No };
            int difficulty = Zero,
                attemps = Two;
            float inflictedDamage;
            bool inputCheck,
                isHero = true,
                hasRemainingAttempts = true;

            #endregion


            Console.WriteLine(MenuMsg);
            do
            {
                userInput = Console.ReadLine() ?? "";
                inputCheck = Utility.ValidateInput(userInput, twoValidInputs);
                if (!inputCheck)
                    Console.WriteLine(ErrorMsg);
            } while (!inputCheck);

            if (userInput.Equals(OneStr))
            {
                Console.WriteLine(DifficultyMenuMsg);
                attemps = Two;
                do
                {
                    userInput = Console.ReadLine() ?? "";
                    inputCheck = Utility.ValidateInput(userInput, fourValidInputs);
                    if (!inputCheck)
                    {
                        Console.WriteLine(ErrorMsg);
                        attemps--;
                        hasRemainingAttempts = Utility.GreaterThan(attemps);
                    }
                    else
                    {
                        difficulty = Convert.ToInt32(userInput);
                    }

                } while (!inputCheck && hasRemainingAttempts);

                Console.WriteLine(RenameMsg);
                attemps = Two;
                do
                {
                    userInput = Console.ReadLine()?.ToUpper() ?? "";
                    inputCheck = Utility.ValidateInput(userInput, boolValidInputs);
                    if (!inputCheck)
                    {
                        Console.WriteLine(ErrorMsg);
                        attemps--;
                        hasRemainingAttempts = Utility.GreaterThan(attemps);

                    }

                } while (!inputCheck && hasRemainingAttempts);

                if (userInput == Yes)
                {
                    archerName = SetStat.Rename(archerName);
                    monsterName = SetStat.Rename(monsterName);
                }

                archer = SetStat.StatSetter(archer, difficulty, isHero);
                archerHp = GetStat.Hp(archer);
                archerAttack = GetStat.Attack(archer);
                archerReduction = GetStat.Reduction(archer);

                monster = SetStat.StatSetter(monster, difficulty, !isHero);
                monsterHp = GetStat.Hp(monster);
                monsterAttack = GetStat.Attack(monster);
                monsterReduction = GetStat.Reduction(monster);


                do
                {

                    if (Utility.GreaterThan(GetStat.Hp(archer)))
                    {   //Archer Turn

                        Console.WriteLine(RequestCommandMsg);
                        userInput = Console.ReadLine() ?? "";
                        inputCheck = Utility.ValidateInput(userInput, fourValidInputs);
                        attemps = Two;
                        do
                        {
                            if (!inputCheck)
                            {
                                Console.WriteLine(ErrorMsg);
                                attemps--;
                                hasRemainingAttempts = Utility.GreaterThan(attemps);
                            }
                            if (userInput.Equals(TwoStr) && Utility.GreaterThan(archerAbilityCurrentCD))
                            {
                                Console.WriteLine(OnCooldown, archerAbilityCurrentCD);
                            }
                        } while (!inputCheck && hasRemainingAttempts);

                        if (hasRemainingAttempts)
                        {
                            attemps = Two;
                            do
                            {
                                switch (userInput)
                                {
                                    case "1":
                                        inflictedDamage = Battle.CalculateDamage(archerAttack, monsterReduction);
                                        Battle.InformAction(archerName, monsterName, inflictedDamage);
                                        monsterHp = Battle.RemainedHp(monsterHp, inflictedDamage);
                                        break;
                                    case "2":
                                        monsterCurrentStun = archerAbilityEffect;
                                        archerAbilityCurrentCD = SkillCd;
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
                        inflictedDamage = Battle.CalculateDamage(monsterAttack, archerReduction, GuardEffect, archerDefenseMode);
                        Battle.InformAction(monsterName, archerName, inflictedDamage);
                        archerHp = Battle.RemainedHp(monsterHp, inflictedDamage);
                    }

                    Console.WriteLine(CurrentStatus, archerName, archerHp);
                    Console.WriteLine(CurrentStatus, monsterName, monsterHp);

                    //End of Round
                    monsterCurrentStun--;
                    archerAbilityCurrentCD--;


                } while (Utility.GreaterThan(GetStat.Hp(archer))
                    && Utility.GreaterThan(GetStat.Hp(monster)));
            }

        }
    }
}