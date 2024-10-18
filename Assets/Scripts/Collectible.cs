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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Pellet")
        {
            Destroy(other.gameObject);
        }

    }
}

