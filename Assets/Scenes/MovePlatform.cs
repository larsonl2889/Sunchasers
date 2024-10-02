using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlatform : MonoBehaviour
{
    [SerializeField]
    private Transform position1, position2;
    private float _speed = 3.0f;
    private bool _switch = false;
    GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.Find("button");
    }

    // Update is called once per frame

    void FixedUpdate()
    {

            if (Input.GetMouseButton(0)) // 0 is for left mouse button
            {
                // Create a ray from the camera to where the mouse is pointing
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Perform the raycast and check if it hits something with a collider
                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the object clicked on has a specific tag, or you can compare it to a specific object
                    if (hit.collider.gameObject == button)
                    {
                        // Your logic here, e.g., activate the object or trigger something
                        Debug.Log("Object clicked!");


                        if (_switch == false)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, position1.position,
                                _speed * Time.deltaTime);
                        }
                        else if (_switch == true)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, position2.position,
                                _speed * Time.deltaTime);
                        }

                        if (transform.position == position1.position)
                        {
                            _switch = true;
                        }
                        else if (transform.position == position2.position)
                        {
                            _switch = false;
                        }
                    }
                }
            }
        }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
