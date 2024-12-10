using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class LevelFadeIn : MonoBehaviour
{
    private Image screen;
    public AudioSource music;
    Sequence sequence;
    

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
        sequence = DOTween.Sequence();
        Color startColor = new Color(0, 0, 0, 0);
        Color targetColor = Color.white;
        sequence
            .Join(DOTween.To(() => startColor, x => screen.color = x, targetColor, 1f).SetEase(Ease.InOutQuad))
            .Join(DOTween.To(() => music.volume, x => music.volume = x, 0, 1f).SetEase(Ease.InOutQuad));
        
        sequence.OnComplete(() =>
        {
            endTrigger.GetComponent<endTrigger>().switchScene();
        });
        sequence.Play();


    }
    public void fadeInDoor(GameObject exitObject)
    {
        sequence = DOTween.Sequence();
        Color startColor = new Color(0, 0, 0, 0);
        Color targetColor =Color.black;
        sequence
           .Join(DOTween.To(() => startColor, x => screen.color = x, targetColor, 1f).SetEase(Ease.InOutQuad))
           .Join(DOTween.To(() => music.volume, x => music.volume = x, 0, 1f).SetEase(Ease.InOutQuad));
        
        sequence.OnComplete(() =>
        {
            exitObject.GetComponent<Door>().switchScene();
        });
        sequence.Play();

    }


}
