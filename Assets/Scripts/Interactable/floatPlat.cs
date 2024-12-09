using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using DG.Tweening;
public class floatPlat : MonoBehaviour
{
    public bool movingToOtherPnt = false;
    public bool isPowered = false;
    private Animator animator;   
    public float speed;
    public float moveUnitsVertical = 0;
    public float moveUnitsHorizontal = 0;
    private Vector3 startPos;
    private Vector3 targetPos;
    private AudioSource platSound;
   

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startPos = transform.position;
        targetPos = new Vector3(moveUnitsHorizontal + startPos.x, moveUnitsVertical + startPos.y, startPos.z);
        platSound = GetComponent<AudioSource>();
        if (isPowered)
        {
            platSound.enabled = true;
        }
        else
        {
            platSound.enabled = false;
        }
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPowered)
        {
            if (movingToOtherPnt)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
            }
            animator.SetBool("isOn", true);
            /*
            if (transform.position == targetPos || transform.position == startPos)
            {
                animator.SetBool("isOn", false);
            }
            else
            {
                animator.SetBool("isOn", true);
            }
            */
            platSound.enabled = true;
        }
        else
        {
            platSound.enabled = false;
            animator.SetBool("isOn", false);
        }
    }
    
    

    public void move()
    {
        movingToOtherPnt = !movingToOtherPnt;
    }
    public void power()
    {
        isPowered = true;
    }
    public void powerOff()
    {
        isPowered = false;
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
