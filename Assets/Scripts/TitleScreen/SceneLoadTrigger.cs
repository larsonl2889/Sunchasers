using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    public string targetScene;

    public void LoadAScene()
    {
        LoadingData.sceneToLoad = targetScene;
        LoadingData.sceneToUnload = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
    }

    public void LoadASceneNoLoadingScreen()
    {
        LoadingData.sceneToLoad = targetScene;
        SceneManager.LoadSceneAsync(LoadingData.sceneToLoad, LoadSceneMode.Single);
    }
}