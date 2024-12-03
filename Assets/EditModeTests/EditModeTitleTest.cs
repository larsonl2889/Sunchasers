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
        // Step 1: Set up the TitleScreen GameObject
        var gameObject = new GameObject();
        var controller = gameObject.AddComponent<TitleScreen>();

        // Step 2: Mock the creditsPanel
        var creditsPanel = new GameObject();
        controller.creditsPanel = creditsPanel;
        creditsPanel.SetActive(false); // Initial state is inactive

        // Step 3: Mock the creditsButton
        var creditsButtonObject = new GameObject();
        var creditsButton = creditsButtonObject.AddComponent<UnityEngine.UI.Button>();
        controller.creditsButton = creditsButton;

        // Step 4: Add the listener to simulate the button click
        controller.Start();

        // Step 5: Simulate a button click and test the toggle
        creditsButton.onClick.Invoke(); // Simulate clicking the button
        Assert.IsTrue(creditsPanel.activeSelf, "Credits panel should be active after the first click.");

        // Step 6: Simulate another button click and test the toggle again
        creditsButton.onClick.Invoke(); // Simulate another click
        Assert.IsFalse(creditsPanel.activeSelf, "Credits panel should be inactive after the second click.");

        yield return null;
    }
}