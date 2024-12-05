using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Device;
public class LevelFadeOut : MonoBehaviour
{
    // Start is called before the first frame update
    private Image screen;
    void Start()
    {
        screen = GetComponent<Image>();
        Color startColor = new Color(0, 0, 0, 1);

        Color endColor = new Color(0, 0, 0, 0);
        DOTween.To(() => startColor, x => screen.color = x, endColor, 1f).SetEase(Ease.InOutQuad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
