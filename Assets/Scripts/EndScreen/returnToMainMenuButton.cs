using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.Device;

public class returnToMainMenuButton : MonoBehaviour
{
    // Start is called before the first frame update
    private Button button;
    private Image buttonImage;
    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        buttonImage.color = new Color(0, 0, 0, 0);
        button.enabled = false;
        StartCoroutine(delay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void returnToMainMenu()
    {
        SceneManager.LoadScene("Title");
    }

    private IEnumerator delay()
    {
        yield return new WaitForSeconds(2);
        Color startColor = new Color(0, 0, 0, 0);
        Color endColor = new Color(1, 1, 1, 1);
        DOTween.To(() => startColor, x => buttonImage.color = x, endColor, 2f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            
        });
        button.enabled = true;
        
    }
}
