// https://www.reddit.com/r/Unity2D/comments/rebv6g/i_made_a_tutorial_on_how_to_fade_tofrom_a_load/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using TMPro.Examples;

public class LoadNewScene : MonoBehaviour
{
    //Initialize values
    AsyncOperation loadingOperation;
    AsyncOperation unLoadingOperation;
    AsyncOperation unLoadingTransitionOperation;
    //Load scene control variables
    //Have load screen show for at least 3 seconds
    float minLoadTime = 5f;
    //Have "Loading..." graphic change every .5 seconds
    float textChangeLoad = .3f;
    //Timers
    float loadTimer = 0f;
    float loadTextTimer = 0f;
    float fadeTimer = 0f;
    //Text gameobject
    public TextMeshProUGUI loadingTextBox; // Sets the loading text box | Loading...
    public TextMeshProUGUI percentLoaded;  // Sets the loading percent text | 0%
    //Fade
    public CanvasGroup canvasGroup;
    bool fadeInLoad = true;
    bool startFadeOut = false;
    float fadeInTime = .5f;
    float fadeOutTime = .5f;
    //start unload of previous level
    bool unloadStart = true;
    bool jobsDone = false;
    //Porgress
    float progressValue = 0f;


    void Update()
    {
        //Fade-in
        if (fadeInLoad)
        {
            //Fade in operation adjusts alpha for the canvas group containing all of your loading scene images
            if (loadTimer < fadeInTime)
            {
                canvasGroup.alpha = Mathf.Lerp(0, 1, loadTimer / fadeInTime);
            }
            else
            {
                //Once fade in complete, set alpha purposefully
                canvasGroup.alpha = 1;
                //Unload previous scene
                /*if (unloadStart)
                {
                    unLoadingOperation = SceneManager.UnloadSceneAsync(LoadingData.sceneToUnload, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
                    unloadStart = false;
                }*/

                //Load next scene
                if (unLoadingOperation.isDone)
                {
                    loadingOperation = SceneManager.LoadSceneAsync(LoadingData.sceneToLoad, LoadSceneMode.Additive);
                    //For preventing load screen from flashing too quickly
                    loadingOperation.allowSceneActivation = false;
                    fadeInLoad = false;
                }
            }
        }
        else
        //Progress meter and duration control
        {
            //Load percent text output
            progressValue = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            percentLoaded.text = Mathf.Round(progressValue * 100) + "%";

            //For preventing load screen from flashing too quickly even if loading is done
            if ((loadTimer > minLoadTime) && (Mathf.Approximately(loadingOperation.progress, .9f)))
            {
                loadingOperation.allowSceneActivation = true;
            }

            //If level is loaded, start the fade out process
            if (!startFadeOut && loadingOperation.isDone)
            {
                startFadeOut = true;
                //Set active scene to your newly loaded scene to prevent crossover code issues
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(LoadingData.sceneToLoad));
                //Set fade-out timer to 0 
                fadeTimer = 0f;
            }

            //Fade out operation adjusts alpha for the canvas group containing all of your loading scene images
            if (startFadeOut && (fadeTimer < fadeOutTime))
            {
                canvasGroup.alpha = Mathf.Lerp(1, 0, fadeTimer / fadeOutTime);
            }
            else if (startFadeOut && !jobsDone && (fadeTimer >= fadeOutTime))
            {
                //Once fade out complete, set alpha purposefully
                canvasGroup.alpha = 0;
                //unload loading menu scene
                unLoadingTransitionOperation = SceneManager.UnloadSceneAsync("LoadingScreen", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
                jobsDone = true;
            }
        }

        //Set text based on load time
        if (loadTextTimer < textChangeLoad)
        {
            loadingTextBox.text = "LOADING...";
        }
        else if (loadTextTimer < 2 * textChangeLoad)
        {
            loadingTextBox.text = "LOADING..";
        }
        else if (loadTextTimer < 3 * textChangeLoad)
        {
            loadingTextBox.text = "LOADING.";
        }
        else if (loadTextTimer < 4 * textChangeLoad)
        {
            loadingTextBox.text = "LOADING..";
        }
        else
        {
            loadTextTimer = 0f;
        }

        //Increment load timer for changing text
        loadTextTimer += Time.deltaTime;

        //Increment total load timer
        loadTimer += Time.deltaTime;

        //Increment total load timer
        fadeTimer += Time.deltaTime;
    }
}