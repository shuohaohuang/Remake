using System.Diagnostics;

namespace Utilities
{
    public class Utility
    {
        public static string NameMayus(string name)
        {
            name = name.ToLower();
            char[] chars = name.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            name = new string(chars);
            return name;
        }
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
