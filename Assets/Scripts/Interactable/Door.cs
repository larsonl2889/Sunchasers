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
    public LevelFadeIn fadeInUI;

    private void Awake()
    {
        animator = GetComponent<Animator>();
       
    }
    private void Start()
    {
        
    }
    public void open()
    {
        if (isOpen == false)
        {
            if (openSound != null)
            {
                SFXManager.instance.playSound(openSound, transform, 1f);
            }
            isOpen = true;
            animator.SetBool("isOpen", true);
        }
    }
    public void close()
    {
        if (isOpen)
        {
            if (openSound != null)
            {
                SFXManager.instance.playSound(openSound, transform, 1f);
            }
            isOpen = false;
            animator.SetBool("isOpen", false);
        }
    }
    public void enterDoor()
    {
        if (isOpen)
        {
            if (fadeInUI != null)
            {
                fadeInUI.GetComponent<LevelFadeIn>().fadeIn(this.gameObject);
            }
            else
            {
                switchScene();
            }
        }
        
    }
    public void switchScene()
    {
        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
