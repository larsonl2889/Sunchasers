using System;

// Contributors: Leif Larson
// Last updated 10/9/2024

namespace Pipes
{
    public enum SteamState : byte
    {
        SOURCE = 2, FULL = 1, EMPTY = 0, LEAKING = -1
    }
    /// <summary>
    /// Extension class for SteamState.
    /// <br></br>Functionally, can be treated as holding "methods" for the SteamState enum.
    /// </summary>
    public static class SteamStateOps
    {
        public static bool IsOn(this SteamState state)
        {
            return state > 0;
        }
        public static bool IsSteaming(this SteamState state)
        {
            return state != 0;
        }
    }
}
