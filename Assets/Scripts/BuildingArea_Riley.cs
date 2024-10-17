using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BuildingArea_Riley : MonoBehaviour
{
    public GameObject Slot;
    private bool isInRange;
    private Vector2 position;
    private Vector2 WorldPos;
    Stack<GameObject> SlotHolder;
    
    // Start is called before the first frame update
    void Start()
    {
        SlotHolder = new Stack<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void Slots()
    {
        //set the area that can be built in by reading the mouse position.
        position = Mouse.current.position.ReadValue();
        WorldPos = Camera.main.ScreenToWorldPoint(position);
        Vector2 Spawnplace = new Vector2(WorldPos.x, WorldPos.y);
        SlotHolder.Push(Instantiate(Slot, Spawnplace, Slot.transform.rotation));
        
        
        //Instantiate(AnimalPrefabs[AnimalIndex], spawnPos, AnimalPrefabs[AnimalIndex].transform.rotation);
        //Instantiate(SlotPrefab,transform.position,Quaternion.identity);
        //Renderer.enabled = false;
        


    }
    //Deletes all the slots from the scene 
    public void DeleteAll()
    {
        int count = SlotHolder.Count;
        if (SlotHolder.Count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                Destroy(SlotHolder.Pop());
            }
        }
        
    }
}
