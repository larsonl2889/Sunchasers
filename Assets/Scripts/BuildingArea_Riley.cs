using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingArea_Riley : MonoBehaviour
{
    private bool isInRange;

    public Renderer slot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Slots()
    {
       //Instantiate(SlotPrefab,transform.position,Quaternion.identity);
       Renderer.enabled = false;

       
    }
}
