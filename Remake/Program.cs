using System;
using Rpg;
using Utilities;

namespace Remake
{
    public class Program
    {
        static void Main()
        {
            const string Menu = "1. Start a new game" +
                                "\n0. Exit",
                        DifficultyMenu = "Choose the difficulty:" +
                                       "\n\t1.Easy: highest stats for heroes, lowest stats for monster" +
                                       "\n\t2.Difficult: lowest stats for heroes, highest stats for monster" +
                                       "\n\t3.Personalized: personalize your heroes attributes and monster " +
                                       "\n\t4.RandomStats: Is the goddess of luck smiling upon you? ",

                        ErrorMsg = "Wrong insert, try again",
                        RenameMsg = "Do you want rename characters:\n[Y/N]",
                        Yes = "Y", No = "N";

            const string ZeroStr="0", OneStr = "1", TwoStr = "2", ThreeStr = "3", FourStr = "4";
            const int Zero=0,One = 1, Two = 2, Three = 3;   
            #region archerStats
            //sample
            string archerName = "Archer";
            int archerAbilityCD = 0;
            int archerAbilityEffect = 2;
            float[,] archer = new float[3, 3]
            {
                { 1000f, 30f, 10f },
                { 1500f, 50f, 20f },
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
            bool archerGuard = false;
            #endregion

            #region MonsterStats
            string monsterName = "Monster";
            int monsterStun = 0;
            float[,] monster = new float[3, 3]
            {
                { 10000f, 40f, 10f },
                { 15000f, 200f, 20f },
                { 0, 0, 0 }
            };
            #endregion

              
            #region PorgramVariables
            string userInput;
            string[] validInputsTwoInts = { OneStr, ZeroStr },
                     validInputsFourInts = { OneStr, TwoStr, ThreeStr, FourStr },
                     validInputsBool = { Yes, No };
            int userCommand = Zero,
                difficulty=Zero ;
            bool checker,isHero = true;
            #endregion

              
            Console.WriteLine(Menu);
            do
            {
                userInput = Console.ReadLine() ?? "";
                switch (Utility.Check(userInput, validInputsTwoInts))
                {
                    case true:
                        userCommand = Convert.ToInt32(userInput);
                        checker = true;
                        break;
                    default:
                        Console.WriteLine(ErrorMsg);
                        checker = false;
                        break;
                }
                checker = Utility.MenuCheck(userInput);
            } while (!checker);

            if (Utility.Equal(userCommand, One))
            {
                Console.WriteLine(DifficultyMenu);
                do
                {
                    userInput = Console.ReadLine() ?? "";
                    switch (Utility.Check(userInput,validInputsFourInts))
                    {
                        case true:
                            difficulty=Convert.ToInt32(userInput);
                            checker = true;
                            break;
                        default: 
                            Console.WriteLine(ErrorMsg);
                            checker = false;
                            break;
                    }
                } while (!checker);

                Console.WriteLine(RenameMsg);
                do
                {
                    userInput = Console.ReadLine()?.ToUpper() ?? "";
                    switch (Utility.Check(userInput, validInputsBool)) 
                    {
                        case true:
                            checker = true;
                            break;
                        default:
                            Console.WriteLine(ErrorMsg);
                            checker = false;
                            break;

                    }
                } while (!checker);
          
                if (userInput==Yes)
                {
                    archerName = SetStat.Rename(archerName);
                }
                archer = SetStat.StatSetter(archer, difficulty, isHero);
                
                monster = SetStat.StatSetter(monster, difficulty, !isHero);

            }
            
        }
    }
}
