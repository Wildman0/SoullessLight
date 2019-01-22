using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NDA.FloatUtil
{
    /// <summary>
    /// Casting extensions for floats
    /// </summary>
    public static class FloatCasting
    {
        /// <summary>
        /// Converts a given float to a bool
        /// </summary>
        /// <param name="f">The float you want to convert</param>
        /// <returns></returns>
        public static bool ToBool(float f)
        {
            return f > 0.95f;
        }
    }
    
    /// <summary>
    /// Math related extensions for floats
    /// </summary>
    public static class FloatMath       
    {
        //Checks if f is between a and b
        public static bool IsBetween(float f, float a, float b)
        {
            return f > a && b < f || f > b && a < f;
        }

        /// <summary>
        /// Checks if a given float is approximately zero
        /// </summary>
        /// <param name="f">The float you want to check</param>
        /// <returns></returns>
        public static bool IsZero(float f)
        {
            return f > -0.00000001f && f < 0.00000001f;
        }

        /// <summary>
        /// Inverts a given float
        /// </summary>
        /// <param name="f">The float you want to invert</param>
        /// <returns></returns>
        public static float Invert(float f)
        {
            return f * -1;
        }

        /// <summary>
        /// Inverts a given float if it's negative
        /// </summary>
        /// <param name="f">The float you want to invert if negative</param>
        /// <returns></returns>
        public static float InvertIfNegative(float f)
        {
            return f < 0 ? f * -1 : f;
        }

        /// <summary>
        /// Gets the ratio of 2 numbers to each other
        /// </summary>
        /// <param name="f1">The first number you want checked against the other</param>
        /// <param name="f2">The second number you want checked against the other</param>
        /// <returns></returns>
        public static float GetRatio(float f1, float f2)
        {
            float larger;
            float smaller;

            if (f1 > f2)
            {
                larger = f1;
                smaller = f2;
            }
            else
            {
                larger = f2;
                smaller = f1;
            }

            return smaller / larger;
        }
        /// <summary>
        /// Gets the amount that a number is above 0, returns 0 if it's below it
        /// </summary>
        /// <param name="f">The number you want to check</param>
        /// <returns></returns>
        public static float GetAmountAboveZero(float f)
        {
            if (f < 0)
                return 0;

            return f;
        }

        /// <summary>
        /// Gets the amount that a number is below 0, returns 0 if it's above it
        /// </summary>
        /// <param name="f">The number you want to check</param>
        /// <returns></returns>
        public static float GetAmountBelowZero(float f)
        {
            if (f > 0)
                return 0;

            return Invert(f);
        }
    }
}