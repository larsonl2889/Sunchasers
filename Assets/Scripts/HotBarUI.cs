
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
    public GameObject[] HotBarNumSlots = new GameObject[9];
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
                    HotBarSlots[i].gameObject.GetComponent<Image>().color = Color.white;
                    //HotBarSlots[i].gameObject.GetComponent<Image>().enabled = true;
                }
                else
                {
                    HotBarSlots[i].gameObject.GetComponent<Image>().sprite = null;
                    HotBarSlots[i].gameObject.GetComponent<Image>().color = Color.clear;
                    //HotBarSlots[i].gameObject.GetComponent<Image>().enabled = false;
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
                HotBarSlots[i].gameObject.GetComponent<Image>().color = Color.clear;

                //this.gameObject.GetComponent<Image>.sprite = NewThing;
                //GameObject instantiated = Instantiate(slot, transform.position, transform.rotation) as GameObject;
                //instantiated.transform.SetParent(UIParent.transform, false);
            }
        }
        
    }
    void Update()
    {
        
    }
    //Sets the image of the object at the index to null when it is placed in the build mat.
    public void BuildUISlot()
    {
        HotBarSlots[hotbar.index].gameObject.GetComponent<Image>().sprite = null;
        HotBarSlots[hotbar.index].gameObject.GetComponent<Image>().color = Color.clear;
        //UIindex=index;
        //UISlot = hotbar.bar[hotbar.index];
    }
    //Places the image of the object being put back in the array back on the object.
    public void RemoveUISlot()
    {
        sprite = hotbar.bar[hotbar.Repairindex].GetComponent<Image>().sprite;
        HotBarSlots[hotbar.Repairindex].gameObject.GetComponent<Image>().sprite = sprite;
        HotBarSlots[hotbar.Repairindex].gameObject.GetComponent<Image>().color = Color.white;
    }



}