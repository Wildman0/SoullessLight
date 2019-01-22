using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NDA.DebugUtil
{
    /// <summary>
    /// Additional utility for Debug.Log
    /// </summary>
    public class DebugLogExtensions
    {
        /// <summary>
        /// Prints a message based on whether or not a given condition is true or false
        /// </summary>
        /// <param name="logIfTrue">The message to print if condition is true</param>
        /// <param name="logIfFalse">The message to print if condition is false</param>
        /// <param name="condition">The condition that decides which Debug message to print</param>
        public static void ConditionalLog(string logIfTrue, string logIfFalse, bool condition)
        {
            Debug.Log(condition ? logIfTrue : logIfFalse);
        }
        
        /// <summary>
        /// Prints a message based on whether or not a given condition is true
        /// </summary>
        /// <param name="logIfTrue">The message to print if condition is true</param>
        /// <param name="condition">The condition that decides which Debug message to print</param>
        public static void ConditionalLog(string logIfTrue, bool condition)
        {
            if (condition)
                Debug.Log(logIfTrue);
        }
    }
}
