using System.Diagnostics;

namespace Utilities
{
    public class Utility
    {
        public static float Round(float value, int digits)
        {
            double result = value;
            result = Math.Round(result, digits);
            return (float)result;
        }

        public static bool InRange(int num, int max)
        {
            return num <= max;
        }

        public static bool InRange(int num, float max)
        {
            return num <= max;
        }

        public static bool InRange(float num, float min, float max)
        {
            return (num >= min && num <= max);
        }

        public static bool GreaterThan(int number, int compared = 0)
        {
            return number > compared;
        }

        public static bool GreaterThan(float number, float compared = 0)
        {
            return number > compared;
        }

        public static string NameMayus(string name)
        {
            name = name.ToLower();
            char[] chars = name.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            name = new string(chars);
            return name;
        }

        #region checkers

        public static bool ValidateInput(string input, string[] validStrings)
        {
            bool result = false;
            for (int i = 0; i < validStrings.Length && !result; i++)
            {
                result = input == validStrings[i];
            }
            return result;
        }
        #endregion
    }

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
