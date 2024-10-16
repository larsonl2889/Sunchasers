using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NPC : MonoBehaviour
// Known Issues:
// When first interacting with an NPC, all the dialogues for every NPC appears on screen
// When the player walks in and out of the NPC interaction area and spams E, some text from the previous interaction carries over
{
    public GameObject dialoguePanel; // This is used to set the "Dialogue Box"
    public TextMeshProUGUI dialogueText; // This is used to set the "Dialogue Text"
    public string[] dialogue; // Sets an string array for the programmer to set the dialogue
    private int index = 0; // Sets index to 0, and is used to print out each character one by one from the string
    public float wordSpeed; // Sets the word speed for how fast the text appears on screen (the lower, the faster).
    private bool playerIsClose; // Determines if the player is close enough to the NPC or not

    void Start()
    {
        dialogueText.text = "";
        RemoveText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose) // Only allows the dialogue to be activated if the player is close enough to the npc
        {
            if (!dialoguePanel.activeInHierarchy) // Enables the dialogue to appear on screen if it isn't already
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogue[index])
            {
                NextLine();
            }
        }
    }

    public void RemoveText() // Resets the text back to a default/empty state
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing() // Prints the the dialogue character by character from an array at the rate that 'wordSpeed' is set to
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            RemoveText();
        }
    }

    // If the player is close enough to the NPC, playerIsClose is set to true allowing for the player to interact with the NPC
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    // If the player is too far from the NPC, playerIsClose is set to false, which turns off the dialogue box
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            RemoveText();
        }
    }
}