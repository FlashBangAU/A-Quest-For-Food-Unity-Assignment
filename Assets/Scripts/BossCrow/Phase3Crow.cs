using System.Collections;
using UnityEngine;

public class Phase3Crow : MonoBehaviour
{
    Rigidbody2D rb;

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

    public AttackPlayerBoss attackPlayerBoss;

    private AudioManager audioManager; // Reference for AudioManager

    // Used to connect the AudioManager reference to the existing AudioManager object
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Find the player object in the scene by its tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Choose an initial perch position for the crow
        //ChooseRandomPerchPosition();
        //Debug.Log("Initial Perch Position: " + perchPosition); // Log the initial perch position for debugging
    }


    private bool hasInitializedPerch = false;  // A flag to track if the initial perch has been set

    void Update()
    {
        // Check if the game is currently in Phase 3 (boss fight phase)
        if (pc.phase3)
        {
            rb.gravityScale = 0;

            // Check if the crow has already initialized its perch
            if (!hasInitializedPerch)
            {
                // Set an initial random perch position only once at the start of Phase 3
                //ChooseRandomPerchPosition();

                // Start the coroutine to smoothly move the crow to the chosen perch position
                StartCoroutine(MoveToPerch(perchPosition));

                // Mark the perch as initialized so this block runs only once
                hasInitializedPerch = true;
            }

            // If the crow is not swooping (i.e., perched)
            if (!isSwooping && hasInitializedPerch)
            {
                // Increment the perch timer based on the time passed since the last frame
                perchTimer += Time.deltaTime;

                // If the crow has perched long enough, start the swooping behavior
                if (perchTimer >= perchTime)
                {
                    // Start swooping down towards the player after the perch time has elapsed
                    StartSwoop();
                }
            }
        }
    }


    // Smoothly move to the first perch when Phase 3 starts
    private IEnumerator MoveToPerch(Vector3 targetPerchPosition)
    {
        // Store the current position of the crow (where it's starting from)
        Vector3 startPosition = transform.position;

        // Calculate the total distance from the current position to the target perch
        float journeyLength = Vector3.Distance(startPosition, targetPerchPosition);

        // Calculate how long it will take to reach the perch based on the defined swoop speed
        // Using the swoopSpeed for consistency in movement speed, whether swooping or perching
        float journeyTime = journeyLength / swoopSpeed;

        // Record the time when the movement starts
        float startTime = Time.time;

        // Loop that moves the crow smoothly towards the target perch position
        while (Time.time < startTime + journeyTime)
        {
            // Calculate the normalized time (t) for the movement from 0 to 1
            float t = (Time.time - startTime) / journeyTime;

            // Smoothly move the crow from the start position to the target perch using linear interpolation (Lerp)
            transform.position = Vector3.Lerp(startPosition, targetPerchPosition, t);

            // Yield to the next frame to allow smooth animation across multiple frames
            yield return null;
        }

        // Log a message to the console indicating that the crow has reached its perch
        Debug.Log("Crow has smoothly reached the perch at: " + targetPerchPosition);
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
        //Debug.Log("Branch 1 Position: " + branch1.position);
        //Debug.Log("Branch 2 Position: " + branch2.position);

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

        // Play crow call to signal swoop
        audioManager.PlaySFX(audioManager.bossCall);

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
            attackPlayerBoss.PlayerGotHit();
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
