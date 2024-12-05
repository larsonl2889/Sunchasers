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

    public void fadeIn(GameObject exitObject)
    {
        Color startColor = new Color(0, 0, 0, 0);
        
        Color endColor = new Color(0, 0, 0, 1);
        DOTween.To(() => startColor, x => screen.color = x, endColor, 1f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            exitObject.GetComponent<Door>().switchScene();
        });
        
       
    }
}
