using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cam : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject followTarget;
    private CinemachineVirtualCamera cam;
    private CinemachineCameraOffset camOffset;
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        camOffset = GetComponent<CinemachineCameraOffset>();    
        cam.Follow = followTarget.transform;
        camOffset.m_Offset = followTarget.GetComponent<PlayerController_Willliam>().camOffset;
        cam.m_Lens.OrthographicSize = followTarget.GetComponent<PlayerController_Willliam>().zoom;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeFollowTarget(GameObject newTarget)
    {
        // Store the current target position and the new target position
        Vector3 currentPosition = cam.transform.position;
        Vector3 newPosition = newTarget.transform.position;

        // Tween to move smoothly to the new target's position
        DOTween.To(() => currentPosition,x => cam.transform.position = x,newPosition,.1f).SetEase(Ease.InOutQuad).OnComplete(() =>
               {
                   // After reaching the new target, set the Follow property
                   cam.Follow = newTarget.transform;
               });


        if (newTarget.CompareTag("Player"))
        {
            updateOffset(newTarget.GetComponent<PlayerController_Willliam>().camOffset);
            updateZoom(newTarget.GetComponent<PlayerController_Willliam>().zoom);
        }
        else
        {
            updateOffset(newTarget.GetComponent<BuildingArea_Riley>().camOffset);
            updateZoom(newTarget.GetComponent<BuildingArea_Riley>().camZoom);
        }



    }
    public void updateOffset(Vector3 offset)
    {
        DOTween.To(() => camOffset.m_Offset, x => camOffset.m_Offset = x, offset, 1f).SetEase(Ease.InOutQuad);
    }
    public void updateZoom(float targetZoom)
    {
        DOTween.To(() => cam.m_Lens.OrthographicSize, x => cam.m_Lens.OrthographicSize = x,targetZoom, 1f).SetEase(Ease.InOutQuad);
        

    }
}
