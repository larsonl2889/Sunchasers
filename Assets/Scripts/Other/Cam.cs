using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject followTarget;
    private static CinemachineVirtualCamera cam;
    private static CinemachineCameraOffset camOffset;
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        camOffset = GetComponent<CinemachineCameraOffset>();    
        // cam.Follow = followTarget.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void changeFollowTarget(GameObject newtarget)
    {
        cam.Follow = newtarget.transform;
        if (newtarget.CompareTag("Player"))
        {
            updateOffset(newtarget.GetComponent<PlayerController_Willliam>().camOffset);
            updateZoom(4);
        }
        else
        {
            updateOffset(new Vector3(0,-1,0));
            updateZoom(5);
        }
        
    }
    public static void updateOffset(Vector3 offset)
    {
        camOffset.m_Offset = offset;
    }
    public static void updateZoom(float size)
    {
        cam.m_Lens.OrthographicSize = size;
    }
}
