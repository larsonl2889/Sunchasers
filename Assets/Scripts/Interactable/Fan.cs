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
    void Update()
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
                woosh(collision.gameObject, fanForce);
            }
        }
        
    }
    private void woosh(GameObject player, int force)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(rb.velocity.x, fanForce), ForceMode2D.Impulse);
    }
}
