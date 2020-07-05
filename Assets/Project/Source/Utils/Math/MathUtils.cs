namespace Exa.Utils
{
    public static partial class MathUtils
    {
        public static int GreatestCommonDevisor(int a, int b)
        {
            int remainder;

            while (b != 0)
            {
                remainder = a % b;
                a = b;
                b = remainder;
            }

            return a;
        }

        public static float Remap(
            this float value,
            float from1, 
            float to1, 
            float from2, 
            float to2)
        {
            return 
                // Get base value
                (value - from1) / 
                // Transform the value in range
                (to1 - from1) * 
                (to2 - from2) + 
                // Apply base
                from2;
        }
    }
}