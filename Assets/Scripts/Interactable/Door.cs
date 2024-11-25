using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] AudioClip openSound;
    private bool isOpen = false;
    private Animator animator;

    // Next Level Loader
    public SceneLoadTrigger sceneloadtrigger;
    [SerializeField] float waitTimeLoad = 0f;
    public bool nextSceneLoadStart = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        sceneloadtrigger = GetComponent<SceneLoadTrigger>();
    }
    public void LoadLevel()
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

    public void open()
    {
        if (openSound != null)
        {
            SFXManager.instance.playSound(openSound, transform, 1f);
        }
        isOpen = true;
        animator.SetBool("isOpen", true);
    }
    public void enterDoor()
    {
        if (isOpen)
        {
            if (sceneName != null)
            {
                LoadLevel();
            }
        }
        
    }
}
