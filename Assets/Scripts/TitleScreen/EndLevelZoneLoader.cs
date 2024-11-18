using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelZoneLoader : MonoBehaviour
{
    //Next level loader
    public Button playButton; // Start button
    public SceneLoadTrigger sceneloadtrigger;
    [SerializeField] float waitTimeLoad = 0f;
    public bool nextSceneLoadStart = false;


    // Start is called before the first frame update
    void Start()
    {
        sceneloadtrigger = GetComponent<SceneLoadTrigger>();
    }

    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check if sloth entered warp zone
        SlothController player = other.GetComponent<SlothController>();
        if (player != null)
        {
            //Bool for when next scene load has been triggered
            nextSceneLoadStart = true;
            //Start coroutine that loads next level
            StartCoroutine(LoadNextonTime(waitTimeLoad, player));
        }
    }
    */
    public void PlayGame()
    {
        Debug.Log("Achoo");
        //Bool for when next scene load has been triggered
        nextSceneLoadStart = true;
        

        //Start coroutine that loads next level
        StartCoroutine(LoadNextonTime(waitTimeLoad));
    }

    IEnumerator LoadNextonTime(float timeWait)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(timeWait);

        //Load next level
        sceneloadtrigger.LoadAScene();
    }
}