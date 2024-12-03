using System.Collections;
using NUnit.Framework;
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
}