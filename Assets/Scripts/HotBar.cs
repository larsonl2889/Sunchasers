using Parts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBar : MonoBehaviour
{
    internal BuildingArea_Riley builder;
    internal BuildMat buildMat;
    public GameObject[] bar = new GameObject[9];
    private GameObject part;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BarIndex(3);
        PlaceBar();

    }
    void BarIndex(int index)
    {
        if(bar[index] != null)
        {
            part = bar[index];
        }
    }
    void PlaceBar()
    {
        builder.setSlot(part);
        builder.Build();
        //if(interact)
        //build index
        //move to next index
        builder.Build();
    }
}
