using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class hotbarbutton : MonoBehaviour
{
    public int index;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void test()
    {
        player.GetComponent<PlayerController_Willliam>().setSlotIndex(index);
    }
}
