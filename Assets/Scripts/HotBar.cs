using Parts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBar : MonoBehaviour
{
    internal GameObject anything;
    public GameObject[] bar = new GameObject[9];
    public GameObject[] bar2 = new GameObject[9];
    public int index = new int();

    // Start is called before the first frame update
    void Start()
    {
        anything = gameObject;
        for (int i = 0; i < 9; i++)
        {
            bar[i] = Instantiate(bar2[i], transform.position, transform.rotation);
        }
    }

    // Update is called once per frame

    void Update()
    {
       
        //BarIndex(index);

    }
    public void SetIndex(int index)
    {
       this.index = index;
    }
    public int GetIndex()
    {
        return index;
    }
    public void repairArray(GameObject passedBar)
    {
        for(int i = 0; i < 9; i++)
        {
            if (bar[i] == null)
            {
                bar[i] = passedBar;
                return;
            }
        }
    }
    public void setBar()
    {
        if (bar[index] != null)
        {
            anything.GetComponent<BuildingArea_Riley>().SetSlot(bar[index]);
        }
    }
    public void DeleteIndex()
    {
        bar[index] = null;
    }
}
