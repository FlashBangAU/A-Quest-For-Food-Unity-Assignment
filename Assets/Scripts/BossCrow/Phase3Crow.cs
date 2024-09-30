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
    public float minDistanceFromPlayer = 5f; // Minimum distance to maintain from the player when choosing perch

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
    /*
    void Update()
    {
        // Check if the game is currently in phase 3
        if (pc.phase3)
        {
            // If the crow is not currently swooping
            if (!isSwooping)
            {
                // Increment the perch timer based on the time passed since the last frame
                perchTimer += Time.deltaTime;

                // Move the crow to the current perch position
                transform.position = perchPosition;

                // Check if the perch timer has exceeded the designated perch time
                if (perchTimer >= perchTime)
                {
                    // Start the swoop action and reset perch timer for the next cycle
                    StartSwoop();
                }
            }
        }
    }*/

    private bool hasInitializedPerch = false;

    void Update()
    {
        if (pc.phase3)
        {
            // Check if the crow has already initialized its perch
            if (!hasInitializedPerch)
            {
                ChooseRandomPerchPosition(); // Set initial perch position only once
                hasInitializedPerch = true; // Prevent re-initialization
            }

            // If the crow is not currently swooping
            if (!isSwooping)
            {
                perchTimer += Time.deltaTime;

                // Move the crow to the current perch position
                transform.position = perchPosition;

                if (perchTimer >= perchTime)
                {
                    StartSwoop(); // Start swooping after the perch time
                }
            }
        }
    }


    // Separated swoop initiation for readability and reusability
    private void StartSwoop()
    {
        // Log that the crow is about to swoop towards the player's position
        Debug.Log("Starting swoop to player at position: " + player.position);

        // Flip the sprite to face the player before swooping
        FlipSprite();

        // Start the coroutine to handle the swooping action
        StartCoroutine(SwoopToPlayer());

        // Reset the perch timer for the next cycle
        perchTimer = 0f;
    }

    private void ChooseRandomPerchPosition()
    {
        Debug.Log("Branch 1 Position: " + branch1.position);
        Debug.Log("Branch 2 Position: " + branch2.position);

        Vector3 newPerchPosition;
        bool isValidPosition = false;
        int maxAttempts = 70;  // To avoid infinite loops

        for (int attempt = 0; attempt < maxAttempts; attempt++) // Attempt to find a valid perch within 10 tries
        {
            float randomX = Random.Range(branch1.position.x, branch2.position.x);

            // Ensure the perch is a minimum distance from the player
            if (Mathf.Abs(randomX - player.position.x) >= minDistanceFromPlayer)
            {
                newPerchPosition = new Vector3(randomX, branch1.position.y + perchHeight, transform.position.z);
                Debug.Log("Attempting to set New Perch Position: " + newPerchPosition);

                // Check if the new perch position is sufficiently different from the current one
                if (Vector3.Distance(newPerchPosition, perchPosition) > 1f)
                {
                    perchPosition = newPerchPosition; // Set the new perch position
                    isValidPosition = true;
                    break;  // Exit loop as we've found a valid position
                }
            }
        }

        if (isValidPosition)
        {
            // Move the bird to the new perch
            transform.position = perchPosition;
            Debug.Log("New Perch Position set: " + perchPosition);
        }
        else
        {
            Debug.LogWarning("Could not find a valid new perch position after several attempts.");
        }
    }


    /*
     private void ChooseRandomPerchPosition()
     {
         Debug.Log("Branch 1 Position: " + branch1.position);
         Debug.Log("Branch 2 Position: " + branch2.position);

         Vector3 newPerchPosition;
         do
         {
             float randomX = Random.Range(branch1.position.x, branch2.position.x);
             int perchDirection = Random.Range(1, 2);
             // Ensure the perch position is a safe distance from the player
             if ((player.position.x - minDistanceFromPlayer) > branch1.position.x && perchDirection <= 1.5f)
             {
                 randomX = Random.Range(branch1.position.x, player.position.x - minDistanceFromPlayer);
             }
             else if ((player.position.x + minDistanceFromPlayer) < branch2.position.x && perchDirection > 1.5f)
             {
                 randomX = Random.Range(player.position.x + minDistanceFromPlayer, branch2.position.x);
             }
             else
             {
                 randomX = Random.Range(branch1.position.x, branch2.position.x);
             }

             newPerchPosition = new Vector3(randomX, branch1.position.y + perchHeight, transform.position.z); // Keep the z position
             Debug.Log("Attempting to set New Perch Position: " + newPerchPosition);
         }
         while (newPerchPosition == perchPosition); // Repeat if the same as the current perch position

         perchPosition = newPerchPosition; // Set the new perch position
         transform.position = perchPosition; // Immediately move to the new perch
         Debug.Log("New Perch Position set: " + perchPosition);
     }*/


    private void FlipSprite()
    {
        // Flip the sprite based on the player's position relative to the crow's position
        if (player.position.x < transform.position.x) // Player is to the left
        {
            transform.localScale = new Vector3(3.25477f, 3.166692f, 8.3274f); // Flip sprite left
        }
        else // Player is to the right
        {
            transform.localScale = new Vector3(-3.25477f, 3.166692f, 8.3274f); // Flip sprite right
        }
    }

    private IEnumerator SwoopToPlayer()
    {
        isSwooping = true; // Set swooping state
        Vector3 startPosition = transform.position;
        FlipSprite();

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

        // Flip the sprite to face the new perch position
        FlipSpriteForReturn(returnPosition);

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

    private void FlipSpriteForReturn(Vector3 returnPosition)
    {
        // Flip the sprite based on the return position relative to the crow's current position
        if (returnPosition.x < transform.position.x) // Return position is to the left
        {
            transform.localScale = new Vector3(3.25477f, 3.166692f, 8.3274f); // Flip sprite left
        }
        else // Return position is to the right
        {
            transform.localScale = new Vector3(-3.25477f, 3.166692f, 8.3274f); // Flip sprite right
        }
    }
}
