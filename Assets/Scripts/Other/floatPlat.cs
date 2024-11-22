using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using DG.Tweening;
public class floatPlat : MonoBehaviour
{
    public bool isOn = false;
    private Animator animator;   
    public float speed;
    public float targetHeight;
    private Vector3 startPos;
    private Vector3 targetPos;
   

    private int i;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startPos = transform.position;
        targetPos = new Vector3(startPos.x, targetHeight + startPos.y, startPos.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    
       if (isOn)
       {
            transform.position = Vector2.MoveTowards(transform.position,targetPos, speed * Time.deltaTime);
       }
       else
       {
            transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
       }
        if (transform.position == targetPos || transform.position == startPos)
        {
            animator.SetBool("isOn", false);
        }
        else
        {
            animator.SetBool("isOn", true);
        }
    }
    
    

    public void changeState()
    {
        isOn = !isOn;
        if (isOn)
        {
            
            
        }
        else
        {
            
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.y > transform.position.y)
            {
                collision.transform.SetParent(transform);
                
            }

        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);

        }
    }
}
