using System.Diagnostics;

namespace Utilities
{
    public class Utility
    {
        public static bool InRange(int num, int max)
        {
            return num <= max;
        }
        public static bool InRange(float num, float min, float max)
        {
            return (num >= min && num <= max);
        }
        public static bool Equal(int num, int secondNum)
        {
            return num == secondNum;
        }

        public static string NameMayus(string name)
        {
            char[] chars = name.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            name = new string(chars);
            return name;
        }

        public static void TwoControl(string input, string errorMsg, ref int numOutput)
        {

            if (MenuCheck(input))
            {
                numOutput = Convert.ToInt32(input);
            }
            else
            {
                Console.WriteLine(errorMsg);
            }
        }

        #region checkers
        public static bool MenuCheck(string input)
        {

            const string ZeroStr = "0", OneStr = "1";

            switch (input)
            {
                case ZeroStr:
                case OneStr:
                    return true;
                default: return false;
            }
        }
        public static bool CheckTrio(string input)
        {
            const string OneStr = "1", TwoStr = "2", ThreeStr = "3";
            switch (input)
            {
                case OneStr:
                case TwoStr:
                case ThreeStr:
                    return true;
                default: return false;
            }
        }
    }


    #endregion
    /*
    /// <summary>
    /// Este método realiza alguna operación con los parámetros dados.
    /// </summary>
    /// <param name="parametro1">Descripción del primer parámetro.</param>
    /// <param name="parametro2">Descripción del segundo parámetro.</param>
    /// <returns>Descripción del valor de retorno.</returns>
    public static int MiMetodo(int parametro1, int parametro2)
    {
        // Código del método aquí
        return resultado;
    }
    */

}