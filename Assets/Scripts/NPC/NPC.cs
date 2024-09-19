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

    public TextMeshProUGUI promptUI;
    public TextMeshProUGUI npcNameUI;
    [SerializeField] string npcName;

    public Upgrades upgrades;

    private Coroutine typingCoroutine;

    void Start()
    {
        RemoveText();
        npcNameUI.text = npcName;
        dialogueText.text = "";
        promptUI.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            npcNameUI.text = npcName;
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                StartTyping();
            }
            else if (dialogueText.text == dialogue[index])
            {
                NextLine();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
        {
            RemoveText();
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
        index = 0;
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
            playerIsClose = false;
            promptUI.text = "";
            RemoveText();
        }
    }
}