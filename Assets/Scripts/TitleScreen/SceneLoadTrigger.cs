// https://www.reddit.com/r/Unity2D/comments/rebv6g/i_made_a_tutorial_on_how_to_fade_tofrom_a_load/

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    public string targetScene; // Sets the scene to be loaded after the loading screen is completed

    // LoadAScene is called in "EndLevelZoneLoader.cs", and starts the loading screen
    public void LoadAScene()
    {
        LoadingData.sceneToLoad = targetScene; // Sets the global "sceneToLoad" variable in "LoadingData.cs" to be the target screen
        LoadingData.sceneToUnload = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive); // Opens the loading screen scene
    }

    // Called if 
    public void LoadASceneNoLoadingScreen()
    {
        LoadingData.sceneToLoad = targetScene; // Sets the global "sceneToLoad" variable in "LoadingData.cs" to be the target screen
        SceneManager.LoadSceneAsync(LoadingData.sceneToLoad, LoadSceneMode.Single);
    }
}