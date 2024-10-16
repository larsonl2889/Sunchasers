using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreditsScreenController : MonoBehaviour
{
    public Button creditsBackButton;
    public GameObject titlePanel; // A panel or canvas for displaying credits

    private void Start()
    {
        // Assign the button listeners
        creditsBackButton.onClick.AddListener(ShowTitle);

        // Make sure credits are hidden at the start
        if (titlePanel == true)
        {
            titlePanel.SetActive(false);
        }
        else
        {
            titlePanel.SetActive(false);
        }
    }

    // Called when Back is clicked
    public void ShowTitle()
    {
        titlePanel.SetActive(!titlePanel.activeSelf);
        titlePanel.SetActive(!titlePanel.activeSelf);
    }

    // Scrolls text through the credits
    private void CreditsScroll()
    {

    }
}