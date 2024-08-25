using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemBorerAttack : MonoBehaviour
{
    public GameObject projectile;
    [SerializeField] private Transform projPos;
    [SerializeField] private float shootDistance = 8f; // Adjustable distance for shooting

    private float timer;
    private GameObject player;
    private float timeBetweenShots;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject not found. Please make sure it has the 'Player' tag.");
        }

        timeBetweenShots = Random.Range(3, 6); // Set the initial time between shots
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return; // Exit if the player is not found
        }

        // Increment the timer
        timer += Time.deltaTime;

        // Calculate distance to the player
        float distance = Vector2.Distance(transform.position, player.transform.position);

        // Check if the player is within shooting distance and time to shoot has passed
        if (distance < shootDistance && timer >= timeBetweenShots)
        {
            shoot();
            timer = 0f; // Reset the timer
            timeBetweenShots = Random.Range(3, 6); // Get a new time for the next shot
        }
    }

    void shoot()
    {
        if (projectile != null && projPos != null)
        {
            Instantiate(projectile, projPos.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Projectile or projPos is not assigned.");
        }
    }
}
