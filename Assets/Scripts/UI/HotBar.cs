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
                    bar[i].GetComponent<Part>().SetIndex(i);
                    bar[i].GetComponent<Part>().FormTable();
                    Debug.Log("This is bar2 at i: "+bar2[i]+"This is bar:i:"+ bar[i]);
                }
                else
                {
                    bar[i] = null;
                }

            }
        }
        
    }

    // Update is called once per frame
    void HotBarFixer()
    {
        //if (badPartStorage != null)
        //{
        //    anything = gameObject;
        //    for (int i = 0; i < 9; i++)
        //    {
        //        if (bar2[i] != null)
        //        {
        //            bar[i] = Instantiate(bar2[i], badPartStorage.transform.position, transform.rotation);
        //            bar[i].GetComponent<Part>().SetIndex(i);
        //            bar[i].GetComponent<Part>().FormTable();
        //        }
        //        else
        //        {
        //            bar[i] = null;
        //        }

        //    }
        //}
    }
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
        index = passedBar.GetComponent<Part>().index;
        bar[index] = passedBar;
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
    
    public void setBar()
    {
            anything.GetComponent<BuildingArea_Riley>().SetSlot(bar[index]);
    }
    
    public void DeleteIndex()
    {
        bar[index] = null;
    }
}
