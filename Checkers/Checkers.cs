namespace Checkers
{
    public class Check
    {

        public static bool ValidateInput(string input, string[] validStrings)
        {
            bool Checker = false;

            input = input.ToUpper();
            
            for (int i = 0; i < validStrings.Length && !Checker; i++)
            {
                Checker = input == validStrings[i].ToUpper();
            }
            return Checker;
        }
        public static bool Equals(string a, string b)
        {
            return a.ToUpper() == b.ToUpper();
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

    }
}
