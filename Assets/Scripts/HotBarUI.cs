using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class HotBarUI : MonoBehaviour
{
    private int UIindex = new int();
    private GameObject hotbarUISlot;        // 
    internal HotBar hotbar;                 // Makes an objec to hotbar
    public TextMeshProUGUI hotbarUINum;     // Hotbar number UI
    public GameObject[] hotbarSlots;        // Creates an array of all hotbar slots
    public GameObject slot;                 // Slot in UI
    public GameObject UIParent;             // UI Parent (Hotbar)
    //public GameObject hotbar;

    // Start is called before the first frame update
    void Start()
    {
        GameObject instantiated = Instantiate(slot, transform.position, transform.rotation) as GameObject;
        instantiated.transform.SetParent(UIParent.transform, false);
        // Attempts to put the image of the item into the slot
        for (int i = 0; i < 9; i++)
        {
            hotbarUISlot = hotbar.bar[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetUISlot(int index)
    {
        UIindex=index; //sets the index to be the same as the index of hotbar
        hotbarUISlot = hotbar.bar[hotbar.index]; //sets the object to the index
        //hotbarUINum
    }
    void SetSlots()
    {

    }
    
}
