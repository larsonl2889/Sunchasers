using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable_William : MonoBehaviour
{
    private bool isInRange;
    public UnityEvent interactAction;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;   
    
    // Eventually might change to account for new input system
    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E)) {
                interactAction.Invoke();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            isInRange = true;
            OnEnter.Invoke();
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            OnExit.Invoke();
            
        }
    }
}
