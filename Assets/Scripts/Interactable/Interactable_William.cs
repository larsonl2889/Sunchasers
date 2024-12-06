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
    
    
    public void InvokeAction()
    {
        interactAction.Invoke();
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onEnter.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            onExit.Invoke();
        }
    }
    
    public void showPrompt(GameObject theObject)
    {
        Vector2 objectPos = theObject.transform.position;
        promptPos = new Vector2(objectPos.x, objectPos.y + promptPosOffset);
        if (promptPrefab != null)
        {
            currentPrompt = Instantiate(promptPrefab, promptPos, Quaternion.identity);
            currentPrompt.transform.parent = theObject.transform;
        }
        
    }
    public void hidePrompt()
    {
        if (promptPrefab != null)
        {
            Destroy(currentPrompt);
        }
       
        
    }
}
