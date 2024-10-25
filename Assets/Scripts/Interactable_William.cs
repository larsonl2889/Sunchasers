using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable_William : MonoBehaviour
{
    private bool isInRange;
    public UnityEvent interactAction;
    public UnityEvent onEnter;
    public UnityEvent onExit;
    public GameObject promptPrefab;
    public float promptPosOffset = 1;
    private Vector2 promptPos;
    private GameObject currentPrompt;
    // Eventually might change to account for new input system
    
    public void InvokeAction()
    {
        interactAction.Invoke();
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if (promptPrefab != null)
            {
                showPrompt();
            }
            onEnter.Invoke();
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (promptPrefab != null)
            {
                hidePrompt();
            }
            onExit.Invoke();
            
        }
    }
    public void setPromptPos(Vector2 objectPos)
    {
        promptPos = new Vector2(objectPos.x, objectPos.y + promptPosOffset);
    }
    public void showPrompt()
    {

        currentPrompt = Instantiate(promptPrefab, promptPos, Quaternion.identity);
    }
    public void hidePrompt()
    {
        Destroy(currentPrompt);
        
    }
}
