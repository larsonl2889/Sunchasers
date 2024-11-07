using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HotBarUI : MonoBehaviour
{
    //private int[] UIindex = new int();
    internal HotBar hotbar;
    public Sprite sprite;
    public GameObject slot;
    public GameObject UIParent;
    public GameObject[] HotBarSlots = new GameObject[9];
    public Sprite NewThing;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            //sprite = hotbar.index[i].GetComponent<image>.sprite;
            HotBarSlots[i].transform.gameobject.GetComponent<Image>().m_Sprite;

            //this.gameObject.GetComponent<Image>.sprite = NewThing;
            //GameObject instantiated = Instantiate(slot, transform.position, transform.rotation) as GameObject;
            //instantiated.transform.SetParent(UIParent.transform, false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    void SetUISlot(int index)
    {
        //UIindex=index;
        //UISlot = hotbar.bar[hotbar.index];
    }
    void SetSlots()
    {

    }


   
}
