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
    public int startingPoint;
    public Transform[] points;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if (Vector2.Distance(transform.position, points[i].position) < .02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }
       transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }
    

    public void changeState()
    {
        isOn = !isOn;
        if (isOn)
        {
            animator.SetBool("isOn", true);
            
        }
        else
        {
            animator.SetBool("isOn", false);
            
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
