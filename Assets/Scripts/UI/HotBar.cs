using JetBrains.Annotations;
using Parts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBar : MonoBehaviour
{
    public GameObject Anything;
    private GameObject[] bar = new GameObject[9];
    public GameObject[] Originalbar = new GameObject[9];
    public int index = new int();
    public GameObject badPartStorage;

    // Start is called before the first frame update
    void Start()
    {
        if (badPartStorage != null)
        {
            Anything = gameObject;
            for (int i = 0; i < 9; i++)
            {
                if (Originalbar[i] != null)
                {
                    bar[i] = Instantiate(Originalbar[i], badPartStorage.transform.position, transform.rotation);
                    bar[i].GetComponent<Part>().SetIndex(i);
                    bar[i].GetComponent<Part>().FormTable();
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
    //Used to move the current index to the selected index
    public void MoveBarIndex()
    {
        Anything.GetComponent<BuildingArea_Riley>().SetSlot(bar[index]);
    }
    public GameObject GetBar(int index)
    {
        return bar[index];
    }
    public void SetBar(GameObject bar)
    {
        index = bar.GetComponent<Part>().index;
        this.bar[index] = bar;
    }

    public void DeleteIndex()
    {
        bar[index] = null;
    }
}
