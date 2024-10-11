using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingArea_Riley : MonoBehaviour
{
    public GameObject Slot;
    private bool isInRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void Slots()
    {
        //set the area that can be built in by having a random x and y range.
        Vector2 Spawnplace = new Vector2(Random.Range(7,15), Random.Range(2, 5));
            Instantiate(Slot, Spawnplace, Slot.transform.rotation);
        //Instantiate(AnimalPrefabs[AnimalIndex], spawnPos, AnimalPrefabs[AnimalIndex].transform.rotation);
        //Instantiate(SlotPrefab,transform.position,Quaternion.identity);
        //Renderer.enabled = false;


    }
}
