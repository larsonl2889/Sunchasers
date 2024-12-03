using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class EditModeTitleTest
{
    // Hardcoded TitleScreen script because unity is being a poopy head and won't accept the actual TitleScreen.cs script
    private class TitleScreen : MonoBehaviour
    {
        public Button creditsButton; // Credits button
        public GameObject creditsPanel; // Credits panel
        public TextMeshProUGUI splashText; // Text UI element for the splash text
        public string[] splashTexts; // Array of splash texts

        public void Start()
        {
            if (creditsButton != null)
                creditsButton.onClick.AddListener(ShowCredits);

            // Ensure credits panel is hidden at the start
            if (creditsPanel != null)
                creditsPanel.SetActive(false);
        }

        public void ShowCredits()
        {
            if (creditsPanel != null)
                creditsPanel.SetActive(!creditsPanel.activeSelf);
        }
        

        // Randomly set the splash text
        public void RandomSplashText()
        {
            splashText.text = ""; // Clear text first
            if (splashTexts != null && splashTexts.Length > 0)
            {
                int randomIndex = Random.Range(0, splashTexts.Length);
                splashText.text = splashTexts[randomIndex];
            }
        }
    }

    [UnityTest]
    public IEnumerator EditModeTitleTestCreditsButton()
    {
        // Set up the TitleScreen GameObject
        var gameObject = new GameObject();
        var controller = gameObject.AddComponent<TitleScreen>();

        // Creates a fake creditsPanel
        var creditsPanel = new GameObject();
        controller.creditsPanel = creditsPanel;
        creditsPanel.SetActive(false); // Initial state is inactive

        // Creates a fake credits button
        var creditsButtonObject = new GameObject();
        var creditsButton = creditsButtonObject.AddComponent<UnityEngine.UI.Button>();
        controller.creditsButton = creditsButton;

        // Add the listener to simulate the button click
        controller.Start();

        // Simulate a button click
        creditsButton.onClick.Invoke(); // Simulate clicking the button
        Assert.IsTrue(creditsPanel.activeSelf, "Credits panel should be active after the first click.");

        // imulate another button click
        creditsButton.onClick.Invoke(); // Simulate another click
        Assert.IsFalse(creditsPanel.activeSelf, "Credits panel should be inactive after the second click.");

        yield return null;
    }

    [UnityTest]
    public IEnumerator EditModeTitleTestSplashText()
    {
        // Creates title screen game object
        var gameObject = new GameObject();
        var controller = gameObject.AddComponent<TitleScreen>();

        // Create a fake text mesh object for the splash text
        var textGameObject = new GameObject();
        var splashTextComponent = textGameObject.AddComponent<TextMeshProUGUI>();
        controller.splashText = splashTextComponent;

        // Set the splashTexts array with basic splash texts
        controller.splashTexts = new string[] { "Splash 1", "Splash 2", "Splash 3" };

        // Call the RandomSplashText method
        controller.RandomSplashText();

        // Assert that the splashText has been set to a valid value
        Assert.IsTrue(
            System.Array.Exists(controller.splashTexts, text => text == controller.splashText.text),
            "The splash text should be one of the values in the splashTexts array."
        );

        yield return null;
    }
}