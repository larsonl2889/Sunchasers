using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Parts;
using Cells;
using Blocks;
using UnityEngine.UIElements;
using Cinemachine;

public class BuildingArea_Riley : MonoBehaviour
{
    public GameObject Slot;
    public GameObject UISlotsHolder;
    internal GameObject buildArea;
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

    }


    public void build()
    {
        //set the area that can be built in by reading the mouse position.
        position = Mouse.current.position.ReadValue();
        if (virtualCamera != null) {
            position.z = Mathf.Abs(virtualCamera.transform.position.z);
        }
        else
        {
            position.z = 9.9f;
        }
        
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
        gameObject.GetComponent<HotBar>().repairArray(instantiated);
        part.GetComponentInParent<Part>().Extract();
        SFXManager.instance.playSound(deletePartSound, part.transform, .5f);

        Debug.LogWarning("BuildingArea_Riley.delete(): updating steam");
        // Update steam
        GetComponentInParent<BuildAreaTest>().UpdateSteam();
    }
}
