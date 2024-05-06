using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;

   // public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIsClose == true)
        {
            Debug.Log("the player is close to the npc");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("the player pressed the F key");
        }

        if (Input.GetKeyDown(KeyCode.F) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if(dialogueText.text == dialogue[index])
        {
           // contButton.SetActive(true);
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
       // contButton.SetActive(false);    //disables skipping dialogue

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("the player is close to the npc");
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }

}