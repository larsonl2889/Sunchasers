using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Parts;
using Cells;
using Blocks;
using UnityEngine.UIElements;
using Cinemachine;
using DirectionOps;
using System;
using System.Linq;

public class BuildingArea_Riley : MonoBehaviour
{
    public GameObject Slot;
    public Sprite ErrorSprite;
    public Sprite SelectedSprite;
    public Sprite PlacedSprite;
    public GameObject UISlotsHolder;
    internal GameObject buildArea;
    internal HotBar hotbar;
    internal BuildMat buildMat;
    private bool isInRange;
    private Vector3 position;
    private Vector3 WorldPos;
    Stack<GameObject> SlotHolder;
    [SerializeField] private AudioClip placeSound;
    [SerializeField] private AudioClip deletePartSound;
    public Vector3 camOffset = Vector3.zero;
    public float camZoom = 0;
    public CinemachineVirtualCamera virtualCamera;
   

    //void RepairSlots()
    //{
    //    for 
    //}

    // Start is called before the first frame update
    void Start()
    {
        BuildAreaTest buildAreaMeta = gameObject.GetComponentInParent<BuildAreaTest>();
        buildArea = buildAreaMeta.gameObject;

        SlotHolder = new Stack<GameObject>();
        buildMat = gameObject.GetComponent<BuildMat>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Slot != null)
        {
            position = Mouse.current.position.ReadValue();
            position.z = Mathf.Abs(virtualCamera.transform.position.z);
            WorldPos = Camera.main.ScreenToWorldPoint(position);
            float xPos = WorldPos.x;
            float yPos = WorldPos.y;
            int minX = (int)buildMat.xPos - (int)((float)buildArea.GetComponent<BuildAreaTest>().scale / 2);
            int minY = (int)buildMat.yPos - (int)((float)buildArea.GetComponent<BuildAreaTest>().scale / 2);
            int maxX = (int)buildMat.xPos + (int)((float)buildArea.GetComponent<BuildAreaTest>().scale / 2) + 1;
            int maxY = (int)buildMat.yPos + (int)((float)buildArea.GetComponent<BuildAreaTest>().scale / 2) + 1;
            if (xPos > minX && xPos < maxX && yPos > minY && yPos < maxY)
            {
                    Vector2 Spawnplace = new Vector2((int)xPos + 0.5f, (int)yPos + 0.5f);
                    Slot.transform.position = Spawnplace;
            }
            if(xPos > maxX || xPos < minX || yPos > maxY || yPos < minY)
            {
                Slot.transform.localPosition = new Vector2(100, 100);
                return;
            }else
            {
                Slot.GetComponent<Part>().FormTable();
                if (!buildArea.GetComponent<BuildAreaTest>().CanMerge(Slot, new Vector2(xPos - minX, yPos - minY)))
                {
                    Cell[] cells = Slot.GetComponentsInChildren<Cell>();
                    for (int i = 0; i < cells.Length; i++)
                    {
                        if (!cells[i].isEmpty)
                        {
                            cells[i].gameObject.GetComponent<SpriteRenderer>().sprite = ErrorSprite;
                        }
                    }
                }
                else
                {
                    Cell[] cells = Slot.GetComponentsInChildren<Cell>();
                    for (int i = 0; i < cells.Length; i++)
                    {
                        if (!cells[i].isEmpty)
                        {
                            cells[i].gameObject.GetComponent<SpriteRenderer>().sprite = SelectedSprite;
                        }
                    }
                }
            }
        }
    }


    public void build()
    {
        //set the area that can be built in by reading the mouse position.
        position = Mouse.current.position.ReadValue();
        
        position.z = Mathf.Abs(virtualCamera.transform.position.z);
        
        
        
        WorldPos = Camera.main.ScreenToWorldPoint(position);
        Debug.LogWarning("Mouse " + position + " World " + WorldPos);
        float xPos = WorldPos.x;
        float yPos = WorldPos.y;
        int minX = (int)buildMat.xPos - (int)((float)buildArea.GetComponent<BuildAreaTest>().scale / 2);
        int minY = (int)buildMat.yPos - (int)((float)buildArea.GetComponent<BuildAreaTest>().scale / 2);
        int maxX = (int)buildMat.xPos + (int)((float)buildArea.GetComponent<BuildAreaTest>().scale / 2);
        int maxY = (int)buildMat.yPos + (int)((float)buildArea.GetComponent<BuildAreaTest>().scale / 2);
        if (xPos > minX && xPos < maxX && yPos > minY && yPos < maxY)
        {
            GameObject instantiated = Instantiate(Slot);

            instantiated.GetComponent<Part>().FormTable();
            if (buildArea.GetComponent<BuildAreaTest>().CanMerge(instantiated, new Vector2(xPos - minX, yPos - minY)))
            {

                gameObject.GetComponent<HotBar>().DeleteIndex();
                UISlotsHolder.GetComponent<HotBarUI>().BuildUISlot();
                Destroy(Slot);
                Slot = null;
                SFXManager.instance.playSound(placeSound, instantiated.transform, .5f);
                Vector2 Spawnplace = new Vector2((int)xPos + 0.5f, (int)yPos + 0.5f);
                buildArea.GetComponent<BuildAreaTest>().MergeTables(instantiated, new Vector2(xPos - minX, yPos - minY));
                // Try to update steam
                buildArea.GetComponent<BuildAreaTest>().UpdateSteam();
                Cell[] cells = instantiated.GetComponentsInChildren<Cell>();
                for (int i = 0; i < cells.Length; i++)
                {
                    if (!cells[i].isEmpty)
                    {
                        cells[i].gameObject.GetComponent<SpriteRenderer>().sprite = PlacedSprite;
                    }
                }
                instantiated.transform.position = Spawnplace;
                return;
            }
            Destroy(instantiated);


            //Instantiate(AnimalPrefabs[AnimalIndex], spawnPos, AnimalPrefabs[AnimalIndex].transform.rotation);
            //Instantiate(SlotPrefab,transform.position,Quaternion.identity);
            //Renderer.enabled = false;



        }
        //Deletes all the slots from the scene

        // Try to update steam
        buildArea.GetComponent<BuildAreaTest>().UpdateSteam();
        Debug.LogWarning("BuildingArea_Riley.build(): updating steam");
    }
    public void SetSlot(GameObject Slot)
    {
        this.Slot = Slot;
    }
    public void delete(GameObject part)
    {
        Part PlaceHolder = part.GetComponentInParent<Part>();
        GameObject realPart = PlaceHolder.gameObject;
        GameObject instantiated = Instantiate(realPart);
        instantiated.transform.localPosition = new Vector3(100, 100, 0);
        instantiated.GetComponent<Part>().FormTable();
        gameObject.GetComponent<HotBar>().SetBar(instantiated);
        gameObject.GetComponent<HotBar>().MoveBarIndex();
        part.GetComponentInParent<Part>().Extract();
        SFXManager.instance.playSound(deletePartSound, part.transform, .5f);

        Debug.LogWarning("BuildingArea_Riley.delete(): updating steam");
        // Update steam
        GetComponentInParent<BuildAreaTest>().UpdateSteam();
    }

    public void RotateGivenPart(Direction dir)
    {
        if (Slot != null)
        {
            GameObject tmp = DirectionOperator.RotatePart(Slot, dir);
            GameObject instantiated = Instantiate(tmp);
            Destroy(tmp);
            Slot = instantiated;
            instantiated.GetComponent<Part>().FormTable();
            gameObject.GetComponent<HotBar>().SetBar(instantiated);
            //GetComponent<BuildingArea_Riley>().build();
        }
    }
}
