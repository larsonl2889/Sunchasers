using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class LevelFadeIn : MonoBehaviour
{
    private Image screen;
    

    // Start is called before the first frame update
    void Start()
    {
        screen = GetComponent<Image>();
        screen.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadeInEnd(GameObject endTrigger)
    {
        Color startColor = new Color(0, 0, 0, 0);
        Color targetColor = Color.white;
        DOTween.To(() => startColor, x => screen.color = x, targetColor, 1f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            endTrigger.GetComponent<endTrigger>().switchScene();
        });


    }
    public void fadeInDoor(GameObject exitObject)
    {
        Color startColor = new Color(0, 0, 0, 0);
        Color targetColor =Color.black;

        DOTween.To(() => startColor, x => screen.color = x, targetColor, 1f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            exitObject.GetComponent<Door>().switchScene();
        });

    }


}
