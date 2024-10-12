using System;
using System.Diagnostics;
using UnityEngine;

// Contributors: Leif Larson
// Last updated 10/9/2024

namespace Pipes
{ 
    public enum SteamState : int
    {
        SOURCE = 2, FULL = 1, EMPTY = 0, LEAKING = -1
    }
    /// <summary>
    /// Extension class for SteamState.
    /// <br></br>Functionally, can be treated as holding "methods" for the SteamState enum.
    /// </summary>
    public static class SteamStateOps
    {
        /// <summary>
        /// Detects whether the pipe should activate any objectives.
        /// </summary>
        /// <param name="state">the steam state</param>
        /// <returns>Whether this pipe activates objectives.</returns>
        public static bool IsOn(this SteamState state)
        {
            return state > 0;
        }

        /// <summary>
        /// Detects whether the pipe should be animated as steaming at any openings.
        /// </summary>
        /// <param name="state">the steam state</param>
        /// <returns>Whether steam animations should be considered at this pipe.</returns>
        public static bool IsSteaming(this SteamState state)
        {
            return state != 0;
        }

        /// <summary>
        /// Returns a string representation of the given SteamState
        /// </summary>
        /// <param name="state">the given SteamState</param>
        /// <returns>Returns a string representation of the given SteamState</returns>
        public static string ToString(this SteamState state)
        {
            string s;
            switch (state)
            {
                case SteamState.SOURCE:
                    return "SOURCE";
                case SteamState.FULL:
                    return "FULL";
                case SteamState.EMPTY:
                    return "EMPTY";
                case SteamState.LEAKING:
                    return "LEAKING";
                default:
                    UnityEngine.Debug.LogWarning("SteamStateOps.ToString() Defaulted!");
                    return "DEFAULTED!";
            }
        }

    }
}
