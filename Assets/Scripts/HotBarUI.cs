
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class HotBarUI : MonoBehaviour
{
    //private int[] UIindex = new int();(Bad causes Errors!)
    public HotBar hotbar;
    public Sprite sprite;
    public GameObject slot;
    public GameObject UIParent;
    public GameObject[] HotBarSlots = new GameObject[9];
    //public Sprite NewThing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void updateImages()
    {
        if (hotbar != null)
        {
            for (int i = 0; i < 9; i++)
            {
                Debug.Log(i);
                if (hotbar.bar[i] != null)
                {
                    sprite = hotbar.bar[i].GetComponent<Image>().sprite;
                    HotBarSlots[i].gameObject.GetComponent<Image>().sprite = sprite;
                }
                else
                {
                    HotBarSlots[i].gameObject.GetComponent<Image>().sprite = null;
                }
                

                //this.gameObject.GetComponent<Image>.sprite = NewThing;
                //GameObject instantiated = Instantiate(slot, transform.position, transform.rotation) as GameObject;
                //instantiated.transform.SetParent(UIParent.transform, false);
            }
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
               
                HotBarSlots[i].gameObject.GetComponent<Image>().sprite = null;

                //this.gameObject.GetComponent<Image>.sprite = NewThing;
                //GameObject instantiated = Instantiate(slot, transform.position, transform.rotation) as GameObject;
                //instantiated.transform.SetParent(UIParent.transform, false);
            }
        }
        
    }
    void Update()
    {
        
    }
    public void BuildUISlot()
    {
        HotBarSlots[hotbar.index].gameObject.GetComponent<Image>().sprite = null;
        //UIindex=index;
        //UISlot = hotbar.bar[hotbar.index];
    }
    public void RemoveUISlot()
    {
        sprite = hotbar.bar[hotbar.index].GetComponent<Image>().sprite;
        HotBarSlots[hotbar.index].gameObject.GetComponent<Image>().sprite = sprite;
    }



}