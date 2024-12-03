using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TitleScreen : MonoBehaviour
{
    public string gameScene; // The scene to switch to when playing the game
    public Button creditsButton; // Credits button
    public TextMeshProUGUI splashText; // Text UI element for the splash text
    public string[] splashTexts; // Array of possible splash texts
    public GameObject creditsPanel; // A panel or canvas for displaying credits

    private void Start()
    {
        // Assign the button listeners
        //playButton.onClick.AddListener(PlayGame);
        creditsButton.onClick.AddListener(ShowCredits);

        // Make sure credits are hidden at the start
        if (creditsPanel == true)
            creditsPanel.SetActive(false);
        else
            creditsPanel.SetActive(false);

        // Display random splash text
        RandomSplashText();
    }

    // Old code for the play button - not used with the new loading screen
    // Called when Play is clicked
    /*public void PlayGame()
    {
        SceneManager.LoadScene(gameScene);
       
    }*/

    // Called when Credits is clicked
    public void ShowCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }

    // Show random splash text
    private void RandomSplashText()
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
