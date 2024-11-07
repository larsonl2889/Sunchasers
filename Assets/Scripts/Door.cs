using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    [SerializeField] string sceneName;
    private bool isOpen = false;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        
    }
    public void open()
    {
        isOpen = true;
        animator.SetBool("isOpen", true);
    }
    public void enterDoor()
    {
        if (isOpen)
        {
            if (sceneName != null)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
        
    }
}
