namespace Utilities
{
    public class Utility
    {
        public static bool InRange(int num, int max)
        {
            return  num <= max;
        }
        public static bool InRange(float num, float min, float max)
        {
            return (num >= min && num <= max);
        }
        public static bool Equal(int num, int secondNum)
        {
            return num==secondNum;
        }
    }
}
