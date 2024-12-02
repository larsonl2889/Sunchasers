using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    // Start is called before the first frame update
    public int fanForce;
    public bool isOn = false;
    private Animator animator;
    private AudioSource fanSound;
    
   
    void Start()
    {
        animator = GetComponent<Animator>();
        fanSound = GetComponent<AudioSource>();
        if (isOn)
        {
            animator.SetBool("isOn", true);
            fanSound.Play();

        }
        else
        {
            animator.SetBool("isOn", false);
            fanSound.Stop();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {


    }
    public void changeState()
    {
        isOn = !isOn;
        if (isOn)
        {
            animator.SetBool("isOn", true);
            fanSound.Play();
        }
        else
        {
            animator.SetBool("isOn", false);
            fanSound.Stop();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOn)
        {
            if (collision.CompareTag("Player"))
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(rb.velocity.x, fanForce), ForceMode2D.Impulse);
               
            }
        }
    }
    
}
    
