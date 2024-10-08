using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index = 0;
    public float wordSpeed;
    public bool playerIsClose;

    void Start()
    {
        dialogueText.text = "";
        RemoveText(); // Removes the text from the dialogue box, just incase there is any
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))

        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            RemoveText();
        }
    }
}