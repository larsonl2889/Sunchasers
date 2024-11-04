using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NPC : MonoBehaviour {
    public GameObject dialoguePanel; // This is used to set the "Dialogue Box"
    public TextMeshProUGUI dialogueText; // This is used to set the "Dialogue Text"
    public string[] dialogue; // Sets an string array for the programmer to set the dialogue
    private int index = 0; // Sets index to 0, and is used to print out each character one by one from the string
    public float wordSpeed; // Sets the word speed for how fast the text appears on screen (the lower, the faster).

    void Start()
    {
        dialoguePanel.SetActive(false);
        RemoveText();
        StopAllCoroutines();
    }
    public void Interact()
    {
        if (!dialoguePanel.activeInHierarchy)
        {
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
        }
        else if (dialogueText.text == dialogue[index])
        {
            NextLine();
            wordSpeed = 0.05f;
        }
        wordSpeed = wordSpeed / 2;
    }
    
    public void Enter()
    {
        RemoveText();
        StopAllCoroutines();
    }

    public void Exit()
    {
        RemoveText();
        if (dialoguePanel.activeInHierarchy)
        {
            dialoguePanel.SetActive(false);
        }
        StopAllCoroutines();
        wordSpeed = 0.05f;
    }

    // Resets the text back to a default/empty state
    private void RemoveText()
    {
        StopCoroutine(Typing());
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    // Sets the dialouge to the next line in the array
    private void NextLine()
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

    // Prints the the dialogue character by character from an array at the rate that 'wordSpeed' is set to
    IEnumerator Typing() 
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }


    /*
Old code to check if the player could interact with the NPC
public void StartDialogue() {
    // Enables the dialogue to appear on screen if it isn't already
    if (!dialoguePanel.activeInHierarchy) 
    {
        dialoguePanel.SetActive(true);
        StartCoroutine(Typing());
    }
    else if (dialogueText.text == dialogue[index])
    {
        NextLine();
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
*/
}