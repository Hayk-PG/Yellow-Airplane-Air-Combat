﻿using System;

namespace Pautik
{
    /// <summary>
    /// Provides conversion and utility methods.
    /// </summary>
    public struct Converter
    {
        /// <summary>
        /// Converts an angle value to a range of -180 to 180 degrees.
        /// </summary>
        /// <param name="axis">The input angle value.</param>
        /// <returns>The converted angle value within the range of -180 to 180 degrees.</returns>
        public static float ConvertAngle(float axis)
        {
            axis = axis > 180 ? axis - 360 : axis;

            return axis;
        }

        /// <summary>
        /// Calculates the difference between two angles, accounting for angle wrapping around 360 degrees.
        /// </summary>
        /// <param name="currentAngle">The current angle.</param>
        /// <param name="previousAngle">The previous angle.</param>
        /// <returns>The difference in angles.</returns>
        public static float GetAngleDifference(float currentAngle, float previousAngle)
        {
            float angleDifference = currentAngle - previousAngle;

            if(angleDifference > 180f)
            {
                angleDifference -= 360f;
            }

            else if(angleDifference < -180f)
            {
                angleDifference += 360f;
            }

            return angleDifference;
        }

        /// <summary>
        /// Converts a direction vector to an angle in radians.
        /// </summary>
        /// <param name="direction">The direction vector.</param>
        /// <returns>The angle in radians.</returns>
        public static float RadianAngle(UnityEngine.Vector2 direction)
        {
            return UnityEngine.Mathf.Atan2(direction.y, direction.x) * UnityEngine.Mathf.Rad2Deg;
        }

        /// <summary>
        /// Interpolates between two quaternions using spherical linear interpolation (SLERP).
        /// </summary>
        /// <param name="a">The starting quaternion.</param>
        /// <param name="b">The target quaternion.</param>
        /// <param name="speed">The speed of interpolation.</param>
        /// <param name="time">The interpolation time.</param>
        /// <returns>The interpolated quaternion.</returns>
        public static UnityEngine.Quaternion QuaternionSlerp(UnityEngine.Quaternion a, UnityEngine.Quaternion b, float speed, float time)
        {
            return UnityEngine.Quaternion.Slerp(a, b, speed * time);
        }

        /// <summary>
        /// Calculates the direction vector from 'from' to 'to'.
        /// </summary>
        /// <param name="to">The target position.</param>
        /// <param name="from">The starting position.</param>
        /// <returns>The direction vector.</returns>
        public static UnityEngine.Vector2 Direction(UnityEngine.Vector2 to, UnityEngine.Vector2 from)
        {
            return to - from;
        }

        /// <summary>
        /// Calculates the direction vector from 'from' to 'to'.
        /// </summary>
        /// <param name="to">The target position.</param>
        /// <param name="from">The starting position.</param>
        /// <returns>The direction vector.</returns>

        public static UnityEngine.Vector3 Direction(UnityEngine.Vector3 to, UnityEngine.Vector3 from)
        {
            return to - from;
        }

        /// <summary>
        /// Calculates the distance between two points in 3D space.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>The distance between the points.</returns>
        public static float Distance(UnityEngine.Vector3 a, UnityEngine.Vector3 b)
        {
            return UnityEngine.Vector3.Distance(a, b);
        }

        /// <summary>
        /// Formats the provided time values into a string representation in the format "HH:MM:SS".
        /// </summary>
        /// <param name="hours">The hours value.</param>
        /// <param name="minutes">The minutes value.</param>
        /// <param name="seconds">The seconds value.</param>
        /// <returns>The formatted time string.</returns>
        public static string HhMMSS(int hours, int minutes, int seconds)
        {
            return hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2");
        }

        /// <summary>
        /// Formats an integer number with thousands separators.
        /// </summary>
        /// <param name="number">The number to format.</param>
        /// <param name="digitsAfterDecimalPoint">Determines whether to include digits after the decimal point.</param>
        /// <returns>The formatted string representation of the number.</returns>
        public static string ThousandsSeparatorString(int number, bool digitsAfterDecimalPoint = false)
        {
            return digitsAfterDecimalPoint ? $"{number:n}" : $"{number:n0}";
        }

        /// <summary>
        /// Formats an integer number as a decimal string with a specified number of digits.
        /// </summary>
        /// <param name="number">The number to format.</param>
        /// <param name="digits">The number of digits to include in the decimal string.</param>
        /// <returns>The formatted decimal string.</returns>
        public static string DecimalString(int number, int digits = 1)
        {
            return digits <= 1 ? $"{number:0}" : digits == 2 ? $"{number:00}" : digits == 3 ? $"{number:000}" : digits == 4 ? $"{number:0000}" : digits == 5 ? $"{number:00000}" :
                   digits == 6 ? $"{number:000000}" : digits == 7 ? $"{number:0000000}" : digits == 8 ? $"{number:00000000}" : $"{number:000000000}";
        }

        /// <summary>
        /// Rounds a floating-point value to one decimal place and returns a double.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <returns>The rounded value.</returns>
        public static double Double(float value)
        {
            return Math.Round(value, 1);
        }
    }
}