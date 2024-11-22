
using Parts;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
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
    public GameObject SlotUIHolder;
    public GameObject player;
    public Sprite[] sprites = new Sprite[9];
    public GameObject HotBarImage;
    //public Sprite NewThing;
    // Start is called before the first frame update
    void Start()
    {
        SlotUIHolder.gameObject.GetComponent<Canvas>().enabled = false;
        for (int i = 0; i < HotBarSlots.Length; i++)
        {
            HotBarSlots[i].GetComponent<hotbarbutton>().index = i + 1;
            HotBarSlots[i].GetComponent<hotbarbutton>().player = player;
        }
    }

    // Update is called once per frame
    public void updateImages()
    {
        if (hotbar != null)
        {
            for (int i = 0; i < 9; i++)
            {
                Debug.Log(i);
                if (hotbar.GetBar(i) != null)
                {
                    sprite = hotbar.GetBar(i).GetComponent<Image>().sprite;
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
    public void RemoveUISlot(GameObject part)
    {
        if(part == null)
        {
            Debug.Log("part is null");
        }
        int index = part.GetComponentInParent<Part>().index;
        sprite = hotbar.Originalbar[index].GetComponent<Image>().sprite;
        HotBarSlots[index].gameObject.GetComponent<Image>().sprite = sprite;
        HotBarSlots[index].gameObject.GetComponent<Image>().color = Color.white;
        Debug.Log("Test");
    }
    public void UpdateUI(int index)
    {
        sprite = sprites[index];
        HotBarImage.gameObject.GetComponent<Image>().sprite = sprite;
    }

}