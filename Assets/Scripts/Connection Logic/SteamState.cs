using System;

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
    }
}
