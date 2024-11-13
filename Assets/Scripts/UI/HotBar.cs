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
    public int Repairindex;
    public GameObject badPartStorage;
    public GameObject[] PartStorage = new GameObject[9];
    // Start is called before the first frame update
    void Start()
    {
        if (badPartStorage != null)
        {
            anything = gameObject;
            for (int i = 0; i < 9; i++)
            {
                if (bar2[i] != null)
                {
                    bar[i] = Instantiate(bar2[i], badPartStorage.transform.position, transform.rotation);
                }
                else
                {
                    bar[i] = null;
                }

            }
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
        bar[index]= PartStorage[index];
        //for(int i = 0; i < 9; i++)
        //{
        //    if (bar[i] == null)
        //    {
        //        bar[i] = passedBar;
        //        Repairindex = i;
        //        return ;
        //    }
        //}
    }
    public void PartStore()
    {
        PartStorage[index] = bar[index];
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
