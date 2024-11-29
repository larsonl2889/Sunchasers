using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject currentOneWayPlatform;

    private BoxCollider2D playerCollider;
    

    // Update is called once per frame
    private void Awake()
    {
        playerCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        
    }
    public void GoDownPlatform()
    {
        if (currentOneWayPlatform != null)
        {
            StartCoroutine(DisableCollision());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform")){
            currentOneWayPlatform = collision.gameObject;
            
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform")){
            currentOneWayPlatform = null;
        }
    }
    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(.5f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}
