using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlatform : MonoBehaviour
{
    private Rigidbody2D rbody;

    private bool isOnPlatform;

    private Rigidbody2D platformRBody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (isOnPlatform)
        {
            rbody.velocity = rbody.velocity + platformRBody.velocity;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            platformRBody = col.gameObject.GetComponent<Rigidbody2D>();
            isOnPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            isOnPlatform = false;
            platformRBody = null;
        }
    }
}
