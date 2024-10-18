using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Parts;
using UnityEngine.UIElements;

public class BuildingArea_Riley : MonoBehaviour
{
    public GameObject Slot;
    internal BuildAreaTest buildArea;
    internal BuildMat buildMat;
    internal Part part;
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
        if(Slot != null)
        {
            Debug.Log("Slot Exists");
        }
    }

    // Update is called once per frame
    void Update()
    {
     
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
            if(buildArea.CanMerge(part, new Vector2(xPos, yPos)))
            {
                Vector2 Spawnplace = new Vector2(xPos + 0.5f, yPos + 0.5f);
                SlotHolder.Push(Instantiate(Slot, Spawnplace, Slot.transform.rotation));
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
            Destroy(SlotHolder.Pop());
            
        }
        
    }
}
