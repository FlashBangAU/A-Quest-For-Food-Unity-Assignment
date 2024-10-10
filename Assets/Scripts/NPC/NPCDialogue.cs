using UnityEngine;
using TMPro; // Make sure you're using TextMeshPro for UI

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;  // The UI panel for dialogue
    public TextMeshProUGUI dialogueText; // The TextMeshPro component for the dialogue text
    public string npcDialogue = "Hey, this is a nice place to set up camp. You’ve got a long journey ahead, but I know you’ll pull through for the colony!";

    private bool isPlayerNearby = false;

    // Detect when player enters the trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            OpenDialogue();
        }
    }

    // Detect when player leaves the trigger zone
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            CloseDialogue();
        }
    }

    // Opens the dialogue panel and shows the NPC's dialogue
    private void OpenDialogue()
    {
        dialoguePanel.SetActive(true); // Show the dialogue panel
        dialogueText.text = npcDialogue; // Display the dialogue text
    }

    // Closes the dialogue panel
    private void CloseDialogue()
    {
        dialoguePanel.SetActive(false); // Hide the dialogue panel
    }

    // Optional: You can add a key press to interact with NPC (like pressing "E" to close the dialogue)
    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Q))
        {
            CloseDialogue(); // Player can press 'Q' to close the dialogue
        }
    }
}
