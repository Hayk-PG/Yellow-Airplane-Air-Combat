using System;

namespace Pautik
{
    public struct Converter
    {
        public static float ConvertAngle(float axis)
        {
            axis = axis > 180 ? axis - 360 : axis;

            return axis;
        }

        public static string HhMMSS(int hours, int minutes, int seconds)
        {
            return hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2");
        }

        public static string ThousandsSeparatorString(int number, bool digitsAfterDecimalPoint = false)
        {
            return digitsAfterDecimalPoint ? $"{number:n}" : $"{number:n0}";
        }

        public static string DecimalString(int number, int digits = 1)
        {
            return digits <= 1 ? $"{number:0}" : digits == 2 ? $"{number:00}" : digits == 3 ? $"{number:000}" : digits == 4 ? $"{number:0000}" : digits == 5 ? $"{number:00000}" :
                   digits == 6 ? $"{number:000000}" : digits == 7 ? $"{number:0000000}" : digits == 8 ? $"{number:00000000}" : $"{number:000000000}";
        }

        public static double Double(float value)
        {
            return Math.Round(value, 1);
        }
    }
}