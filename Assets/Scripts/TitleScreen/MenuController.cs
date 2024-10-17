using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    // CREDITS SCREEN
    public Button creditsBackButton;
    public GameObject titlePanel; // A panel or canvas for displaying credits
    // TITLE SCREEN
    public Button playButton;
    public Button creditsButton;
    public TextMeshProUGUI splashText; // Text UI element for the splash text
    public string[] splashTexts; // Array of possible splash texts
    public GameObject creditsPanel; // A panel or canvas for displaying credits

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
