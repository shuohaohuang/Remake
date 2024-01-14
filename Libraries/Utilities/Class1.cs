﻿using System.Diagnostics;

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


        #region checkers

        public static bool Check(string input, string[] validStrings)
        {
            bool result = false;
            for (int i = 0; i < validStrings.Length && !result; i++)
            {
                result = input==validStrings[i];
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