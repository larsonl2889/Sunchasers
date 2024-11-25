// https://www.reddit.com/r/Unity2D/comments/rebv6g/i_made_a_tutorial_on_how_to_fade_tofrom_a_load/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelZoneLoader : MonoBehaviour
{
    // Next level loader
    public Button playButton; // Start button
    public SceneLoadTrigger sceneloadtrigger;
    [SerializeField] float waitTimeLoad = 0f;
    public bool nextSceneLoadStart = false;


    // Start is called before the first frame update
    void Start()
    {
        sceneloadtrigger = GetComponent<SceneLoadTrigger>();
    }
    

    // Called when the start button is clicked
    public void PlayGame()
    {
        //Bool for when next scene load has been triggered
        nextSceneLoadStart = true;

        //Start coroutine that loads next level
        StartCoroutine(LoadNextonTime(waitTimeLoad));
    }
    public IEnumerator LoadNextonTime(float timeWait)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(timeWait);

        //Load next level
        sceneloadtrigger.LoadAScene();
    }
}