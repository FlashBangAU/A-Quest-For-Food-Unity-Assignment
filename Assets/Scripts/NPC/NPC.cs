using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index = 0;

    public float wordSpeed;
    public bool playerIsClose;
    private bool isTyping = false;
    private bool lastLine = false;

    public TextMeshProUGUI promptUI;
    public TextMeshProUGUI npcNameUI;
    [SerializeField] string npcName;

    public Upgrades upgrades;

    private Coroutine typingCoroutine;

    public Image locationNPCImage;
    public Sprite npcDialogueSprite;

    void Start()
    {
        RemoveText();
        npcNameUI.text = npcName;
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if(lastLine)
            {
                index = 0;
                RemoveText();
            }
           else if (Input.GetKeyDown(KeyCode.E) && playerIsClose && isTyping == false)
            {
                lastLine = false;
                npcNameUI.text = npcName;
                if (!dialoguePanel.activeInHierarchy)
                {
                    dialoguePanel.SetActive(true);
                    isTyping = true;
                    StartTyping();
                }
                else if (dialogueText.text == dialogue[index])
                {
                    isTyping = true;
                    NextLine();
                }
                dialoguePanel.SetActive(true);
                if (npcDialogueSprite != null)
                {
                    locationNPCImage.sprite = npcDialogueSprite;
                }
                StartTyping();
            }
            if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
            {
                isTyping = false;
                RemoveText();
            }
        } catch (MissingReferenceException e)
        {
            Debug.LogException(e);
        }
    }

    public void RemoveText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
    }

    void StartTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        dialogueText.text = "";
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        typingCoroutine = null;
        isTyping = false;
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            StartTyping();
        }
        else
        {
            if (upgrades != null)
            {
                upgrades.UpdateVariable();
            }
            lastLine = true;
            RemoveText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            promptUI.text = "[E]";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTyping = false;
            playerIsClose = false;
            promptUI.text = "";
            lastLine = false;
            RemoveText();
        }
    }
}