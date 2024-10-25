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
    private GameObject Slot;
    internal BuildAreaTest buildArea;
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
        buildArea = gameObject.GetComponentInParent<BuildAreaTest>();
        buildMat = gameObject.GetComponent<BuildMat>();
        GameObject testObject = Slot;
        part = testObject.GetComponent<Part>();
        if(part != null)
        {
            Debug.Log("Part Exists");
            Slot.GetComponent<Part>().FormTable();
            realPart = part.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public void setSlot(GameObject Slot)
    {
        this.Slot = Slot;
    }

    public void Build()
    {
        //set the area that can be built in by reading the mouse position.
        position = Mouse.current.position.ReadValue();
        WorldPos = Camera.main.ScreenToWorldPoint(position);
        int xPos = (int)WorldPos.x;
        int yPos = (int)WorldPos.y;
        float minX = buildMat.xPos - ((float)buildArea.scale / 2);
        float minY = buildMat.yPos - ((float)buildArea.scale / 2);
        float maxX = buildMat.xPos + ((float)buildArea.scale / 2);
        float maxY = buildMat.yPos + ((float)buildArea.scale / 2);
        if (xPos >= minX && xPos < maxX && yPos >= minY && yPos < maxY)
        {
            if (buildArea.CanMerge(part, new Vector2(xPos, yPos)))
            {
                Vector2 Spawnplace = new Vector2(xPos + 0.5f, yPos + 0.5f);
                SlotHolder.Push(Instantiate(realPart, Spawnplace, Slot.transform.rotation));
                //realPart.GetComponent<Part>().FormTable();
                buildArea.MergeTables(realPart, Spawnplace);
                Part test = realPart.GetComponent<Part>();
                for(int i = 0; i < test.tableSize; i++)
                {
                    for(int j = 0; j < test.tableSize; j++)
                    {
                        if(!test.table.Get(i, j).isEmpty)
                        {
                            Cell testCell = test.table.Get(i, j);
                            Debug.Log("Cell At X = " + testCell.xPos + " Y = " + testCell.yPos);
                            Block testBlock = testCell.GetBlock().GetComponent<Block>();
                            if(testBlock != null)
                            {
                                Debug.Log("Cell Has A Block");
                                Cell cellTwo = testBlock.GetCell();
                                if(cellTwo != null)
                                {
                                    Debug.Log("Block Contains Cell At X = " + cellTwo.xPos + " Y = " + cellTwo.yPos);
                                }
                            }
                        }
                    }
                }
            }
        }
        
        
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
