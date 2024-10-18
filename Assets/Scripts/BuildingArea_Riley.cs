using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Parts;
using Cells;
using Blocks;
using UnityEngine.UIElements;

public class BuildingArea_Riley : MonoBehaviour
{
    public GameObject Slot;
    internal GameObject instantiated;
    //internal BuildAreaTest buildArea;
    internal BuildMat buildMat;
    internal Part part;
    internal GameObject realPart;
    private bool isInRange;
    private Vector2 position;
    private Vector2 WorldPos;
    Stack<GameObject> SlotHolder;
    
    // Start is called before the first frame update
    void Start()
    {
        SlotHolder = new Stack<GameObject>();
        //buildArea = gameObject.GetComponentInParent<BuildAreaTest>();
        buildMat = gameObject.GetComponent<BuildMat>();
        GameObject testObject = Slot;
        part = testObject.GetComponent<Part>();
        if(part != null)
        {
            Debug.Log("Part Exists");
            realPart = part.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void Build(BuildAreaTest buildArea, Vector2 pos)
    {
        //set the area that can be built in by reading the mouse position.
        position = Mouse.current.position.ReadValue();
        WorldPos = Camera.main.ScreenToWorldPoint(position);
        int xPos = (int)WorldPos.x;
        int yPos = (int)WorldPos.y;
        //Vector2 spawnPlace = new Vector2(xPos + 0.5f, yPos + 0.5f);
        //SlotHolder.Push(Instantiate(realPart, spawnPlace, Slot.transform.rotation));
        Part test = Slot.GetComponent<Part>();
        GameObject moreTest = test.gameObject;
        instantiated = Instantiate(moreTest, new Vector2(-20, -20), Slot.transform.rotation);
        instantiated.GetComponent<Part>().FormTable();
        if (instantiated.GetComponent<Part>() != null)
        {
            Debug.Log("NOOOO");
        }
        if (buildArea.CanMerge(instantiated, pos))
        {
            Destroy(instantiated.gameObject);
            Vector2 spawnPlace = new Vector2(xPos + 0.5f, yPos + 0.5f);
            instantiated = Instantiate(Slot, spawnPlace, Slot.transform.rotation);
            buildArea.MergeTables(instantiated, pos);
            return;
        }
        Destroy(instantiated.gameObject);
        /*
        if (buildArea.CanMerge(part, new Vector2(xPos, yPos)))
        {
            Vector2 Spawnplace = new Vector2(xPos + 0.5f, yPos + 0.5f);
            SlotHolder.Push(Instantiate(realPart, Spawnplace, Slot.transform.rotation));
            //realPart.GetComponent<Part>().FormTable();
            buildArea.MergeTables(realPart, Spawnplace);
            Part test = realPart.GetComponent<Part>();
        }
        */

        //Instantiate(AnimalPrefabs[AnimalIndex], spawnPos, AnimalPrefabs[AnimalIndex].transform.rotation);
        //Instantiate(SlotPrefab,transform.position,Quaternion.identity);
        //Renderer.enabled = false;



    }
    //Deletes all the slots from the scene 
    public void Delete()
    {
        
        if (SlotHolder.Count > 0)
        {
            SlotHolder.Pop();
            
            
        }
        
    }
}
