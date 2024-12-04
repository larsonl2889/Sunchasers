// https://www.reddit.com/r/Unity2D/comments/rebv6g/i_made_a_tutorial_on_how_to_fade_tofrom_a_load/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadingData
{
    public static string sceneToLoad;   // Stores the scene to load
    public static string sceneToUnload; // Stores the scene that needs to be unloaded
}