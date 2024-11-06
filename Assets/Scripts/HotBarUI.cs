using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HotBarUI : MonoBehaviour
{
    private int UIindex = new int();
    private GameObject UISlot;
    internal HotBar hotbar;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            UISlot = hotbar.bar[i];

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetUISlot(int index)
    {
        UIindex=index;
        UISlot = hotbar.bar[hotbar.index];
    }
    void SetSlots()
    {

    }
    //public GameObject slot;
    //public GameObject UIParent;
    //
    //GameObject instantiated = Instantiate(slot, transform.position, transform.rotation) as GameObject;
    //instantiated.transform.SetParent(UIParent.transform, false);
}
