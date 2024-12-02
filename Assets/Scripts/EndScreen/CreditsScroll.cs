using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsScroll : MonoBehaviour {
    public TextMeshProUGUI credits;
    public void Start()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float duration = 2f; //Fade out over 2 seconds.
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, currentTime / duration);
            credits.color = new Color(credits.color.r, credits.color.g, credits.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }
}
