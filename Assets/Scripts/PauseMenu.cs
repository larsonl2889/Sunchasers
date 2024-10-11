using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Resume(InputAction.CallbackContext context)
    {
        Debug.Log("Pause");
    }
    public void Pause(InputAction.CallbackContext context)
    {

    }
}
