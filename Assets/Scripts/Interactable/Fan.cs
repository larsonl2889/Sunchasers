using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    // Start is called before the first frame update
    public int fanForce;
    public bool isOn = false;
    void Start()
    {
        Debug.Log("WHAT");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changeState()
    {
        isOn = !isOn;
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
