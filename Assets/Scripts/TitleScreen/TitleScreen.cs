using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class TitleScreenController : MonoBehaviour
{
    public Button playButton;
    public Button creditsButton;
    public TextMeshProUGUI splashText; // Text UI element for the splash text
    public string[] splashTexts; // Array of possible splash texts
    public GameObject creditsPanel; // A panel or canvas for displaying credits

    private void Start()
    {
        // Assign the button listeners
        playButton.onClick.AddListener(PlayGame);
        creditsButton.onClick.AddListener(ShowCredits);

        // Make sure credits are hidden at the start
        if (creditsPanel == true)
        {
            creditsPanel.SetActive(false);
        }
        else
        {
            creditsPanel.SetActive(false);
        }

        // Display random splash text
        ShowRandomSplashText();
    }

    // Called when Play is clicked
    public void PlayGame()
    {
        SceneManager.LoadScene("TestArea"); // Can be changed later to load the real level of the game
    }

    // Called when Credits is clicked
    public void ShowCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }

    // Show random splash text
    private void ShowRandomSplashText()
    {
        splashText.text = ""; // Sets the text to nothing upon loading
        if (splashTexts.Length > 0)
        {
            // Picks a random index from the array
            int randomIndex = Random.Range(0, splashTexts.Length);
            splashText.text = splashTexts[randomIndex];
        }
    }
}
