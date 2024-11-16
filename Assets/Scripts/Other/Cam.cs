using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
            updateZoom(newtarget.GetComponent<PlayerController_Willliam>().zoom);
        }
        else
        {
            updateOffset(newtarget.GetComponent<BuildingArea_Riley>().camOffset);
            updateZoom(newtarget.GetComponent<BuildingArea_Riley>().camZoom);
        }
        
    }
    public static void updateOffset(Vector3 offset)
    {
        camOffset.m_Offset = offset;
    }
    public static void updateZoom(float targetZoom)
    {
        DOTween.To(() => cam.m_Lens.OrthographicSize, x => cam.m_Lens.OrthographicSize = x,targetZoom, 1f).SetEase(Ease.InOutQuad);

    }
}
