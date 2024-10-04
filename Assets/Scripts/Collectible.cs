using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject onCollectionEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(1, rotationSpeed, 0);
        transform.Rotate(-1, rotationSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Destroy the collectable
            Destroy(gameObject);

            // Instantiate the particle effect
            Instantiate(onCollectionEffect, transform.position, transform.rotation);
        }

    }
}

