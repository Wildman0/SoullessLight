using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NDA.BoolUtil
{
    public static class BoolCasting
    {
        /// <summary>
        /// Takes a boolean input and converts it to an int (0 = false, 1 = true)
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int ToInt(bool b)
        {
            return b ? 1 : 0;
        }

        /// <summary>
        /// Takes a boolean input and converts it to a float (0 = false, 1 = true)
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float ToFloat(bool b)
        {
            return b ? 1.0f : 0.0f;
        }
    }
}

namespace NDA.KeyUtil
{
    public static class KeyCasting
    {
        //Takes a float value for a key, button or axis and returns whether or not it's
        //in use
        public static bool IsKeyPressed(float key, float deadZone)
        {
            if (key > deadZone)
                return true;
            return false;
        }

        public static bool IsKeyFullyPressed(float key)
        {
            if (key > 0.9f)
                return true;
            return false;
        }
    }
}