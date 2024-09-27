using System.Collections;
using UnityEngine;

public class Phase3Crow : MonoBehaviour
{
    public phaseController pc;  // Reference to the phase controller that manages game phases
    public Transform branch1;    // Transform for the first branch where the bird can perch
    public Transform branch2;    // Transform for the second branch where the bird can perch
    public float perchHeight = 15f; // Height above the branches for perching (Y offset)
    public float swoopSpeed = 7f; // Speed at which the bird swoops down towards the player
    public float perchTime = 2f;   // Time the crow stays perched before swooping

    private Vector3 perchPosition; // The current position where the crow will perch
    private bool isSwooping = false; // Indicates whether the crow is currently swooping down
    private Transform player; // Reference to the player object

    private float perchTimer = 0f; // Timer to track how long the crow has been perched

    void Start()
    {
        // Find the player object in the scene by its tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Choose an initial perch position for the crow
        ChooseRandomPerchPosition();
        Debug.Log("Initial Perch Position: " + perchPosition); // Log the initial perch position for debugging
    }

    void Update()
    {
        // Check if the game is currently in phase 3
        if (pc.phase3)
        {
            // If the crow is not currently swooping
            if (!isSwooping)
            {
                perchTimer += Time.deltaTime; // Increment the perch timer based on the time passed since the last frame

                // Move the crow to the current perch position
                transform.position = perchPosition;

                // Log the current perch timer for debugging
                Debug.Log("Perch Timer: " + perchTimer);

                // Check if the perch timer has exceeded the designated perch time
                if (perchTimer >= perchTime)
                {
                    // Log that the crow is about to swoop towards the player's position
                    Debug.Log("Starting swoop to player at position: " + player.position);

                    // Start the coroutine to handle the swooping action
                    StartCoroutine(SwoopToPlayer());

                    // Reset the perch timer for the next cycle
                    perchTimer = 0f;
                }
            }
        }
    }

    private void ChooseRandomPerchPosition()
    {
        // Debug the positions of branch1 and branch2 to ensure they are different
        Debug.Log("Branch 1 Position: " + branch1.position);
        Debug.Log("Branch 2 Position: " + branch2.position);

        Vector3 newPerchPosition;
        do
        {
            // Calculate a random perch position between Branch1 and Branch2
            float randomX = Random.Range(branch1.position.x, branch2.position.x);
            newPerchPosition = new Vector3(randomX, branch1.position.y + perchHeight, 0);
            Debug.Log("Attempting to set New Perch Position: " + newPerchPosition);
        }
        while (newPerchPosition == perchPosition); // Repeat if the same as the current perch position

        perchPosition = newPerchPosition; // Set the new perch position
        Debug.Log("New Perch Position set: " + perchPosition);
    }


    private IEnumerator SwoopToPlayer()
    {
        isSwooping = true; // Set swooping state
        Vector3 startPosition = transform.position;

        // Define the target position directly based on the player's current position
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, startPosition.z);

        // Log swoop start
        Debug.Log("Swooping from: " + startPosition + " to player at: " + targetPosition);

        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float journeyTime = journeyLength / swoopSpeed;
        float startTime = Time.time;

        // Move towards the player's position (swoop down)
        while (Time.time < startTime + journeyTime)
        {
            float t = (Time.time - startTime) / journeyTime; // Calculate how far we are along the journey (0 to 1)
            transform.position = Vector3.Lerp(startPosition, targetPosition, t); // Move the bird toward the player's position
            yield return null; // Wait for the next frame
        }

        // Log when the bird reaches the player's position
        Debug.Log("Reached player position: " + targetPosition);

        // Choose a new perch position after swooping
        ChooseRandomPerchPosition(); // Choose a new random perch position

        // Log the new perch position
        Debug.Log("New Perch Position after swoop: " + perchPosition);

        // Move back up to the perch
        Vector3 returnPosition = perchPosition; // New perch position to return to
        journeyLength = Vector3.Distance(targetPosition, returnPosition);
        journeyTime = journeyLength / swoopSpeed;
        startTime = Time.time;

        Debug.Log("Returning to new perch at: " + returnPosition); // Log return details

        // Move back up to the perch
        while (Time.time < startTime + journeyTime)
        {
            float t = (Time.time - startTime) / journeyTime;
            transform.position = Vector3.Lerp(targetPosition, returnPosition, t); // Move towards the new perch
            yield return null; // Wait for the next frame
        }

        // Log when the bird returns to perch
        Debug.Log("Reached new perch at: " + returnPosition); // Log when the bird returns to perch
        isSwooping = false; // Reset swooping state for the next swoop
    }


}
