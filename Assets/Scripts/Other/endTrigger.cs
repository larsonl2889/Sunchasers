using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public LevelFadeIn fadeInUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (fadeInUI != null)
            {
                fadeInUI.fadeInEnd(this.gameObject);
            }
        }
    }
    private void switchScene()
    {
        SceneManager.LoadScene("");
    }
}
