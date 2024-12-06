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
    Sequence sequence;
    
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        camOffset = GetComponent<CinemachineCameraOffset>();    
        cam.Follow = followTarget.transform;
        camOffset.m_Offset = followTarget.GetComponent<PlayerController_Willliam>().camOffset;
        cam.m_Lens.OrthographicSize = followTarget.GetComponent<PlayerController_Willliam>().zoom;
        sequence = DOTween.Sequence();
    }

    // Update is called once per frame
    
    public void changeFollowTarget(GameObject newTarget)
    {
        Vector3 currentPosition = cam.transform.position;
        Vector3 newPosition = newTarget.transform.position;
        Vector3 offset;
        float targetZoom;
        sequence = DOTween.Sequence();

        

        if (newTarget.CompareTag("Player"))
        {
            offset = newTarget.GetComponent<PlayerController_Willliam>().camOffset;
            targetZoom = newTarget.GetComponent<PlayerController_Willliam>().zoom;

            
        }
        else
        {
            offset = newTarget.GetComponent<BuildingArea_Riley>().camOffset;
            targetZoom = newTarget.GetComponent<BuildingArea_Riley>().camZoom;
            
        }
        sequence
            .Join(DOTween.To(() => currentPosition, x => cam.transform.position = x, newPosition, .1f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                cam.Follow = newTarget.transform;
            }))
            .Join(DOTween.To(() => camOffset.m_Offset, x => camOffset.m_Offset = x, offset, 1f).SetEase(Ease.InOutQuad))
            .Join(DOTween.To(() => cam.m_Lens.FieldOfView, x => cam.m_Lens.FieldOfView = x, targetZoom, 1f).SetEase(Ease.InOutQuad));

        sequence.Play();


    }
    
}
