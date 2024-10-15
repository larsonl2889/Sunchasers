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
    
    public void InvokeAction()
    {
        interactAction.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
         
            OnEnter.Invoke();
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            OnExit.Invoke();
            
        }
    }
}
