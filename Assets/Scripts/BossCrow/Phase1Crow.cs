using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Crow : MonoBehaviour
{
    public phaseController pc;
    public Rigidbody2D rb;
    private GameObject player;
    private Rigidbody2D playerRb; // Player's Rigidbody for applying knockback
    public GameObject hitBoxPhase1;
    [SerializeField] float attackRange;

    private float xBtw;
    private float yBtw;

    [SerializeField] float jumpHeight;
    [SerializeField] float jumpDistance;

    [SerializeField] bool runAway;
    [SerializeField] bool hoppingMode;
    [SerializeField] bool peckMode;

    [SerializeField] float maxTimeBtwPeck;
    [SerializeField] float minTimeBtwPeck;
    [SerializeField] private float timeBtwPeck;
    [SerializeField] private float nextPeck;
    [SerializeField] private int peckCounter;

    private float tStuck;
    public float timeStuck;

    [SerializeField] bool onGround;
    private bool hoppingRight;

    private bool isKnockedBack = false;

    [SerializeField] float knockbackForceX = 10f; // Force of knockback in the X direction
    [SerializeField] float knockbackForceY = 5f;  // Force of knockback in the Y direction
    /*void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hitBoxPhase1.SetActive(false);
    }*/


    void Start()
    {
        // Find the player object in the scene by tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Check if the player was found and assign the player's Rigidbody2D
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component from the player
        }
        else
        {
            Debug.LogError("Player object not found! Ensure the player has the 'Player' tag.");
        }

        // Ensure the hitbox is initially disabled
        hitBoxPhase1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (player.transform.position.x < hitBoxPhase1.transform.position.x)
        {
            hoppingRight = false;
        }
        else
        {
            hoppingRight = true;
        }

        timeBtwPeck += Time.deltaTime;
        tStuck += Time.deltaTime;


        if (pc.phase1 == true)
        {
            //bird runs to the right of screen
            if (runAway && onGround)
            {
                //play hop animation
                rb.velocity = new Vector2(jumpDistance, jumpHeight);
                onGround = false;
                if (tStuck > 3f)
                {
                    runAway = false;
                    hoppingMode = true;
                }
            }
            else if (hoppingMode)
            {
                //player hopping towards player
                if (onGround && hoppingRight)
                {
                    //play hop animation
                    rb.velocity = new Vector2(jumpDistance, jumpHeight);
                    onGround = false;
                }
                else if (onGround && !hoppingRight)
                {
                    //play hop anmiation
                    rb.velocity = new Vector2(-jumpDistance, jumpHeight);
                    onGround = false;
                }

                //gets range away from player for peck
                xBtw = player.transform.position.x - hitBoxPhase1.transform.position.x;
                if(xBtw < 0)
                {
                    xBtw *= -1;
                }
                //gets range away from player for peck
                yBtw = player.transform.position.y - hitBoxPhase1.transform.position.y;
                if (yBtw < 0)
                {
                    yBtw *= -1;
                }

                //checks if player is in range for peck
                if (xBtw < attackRange && yBtw < attackRange)
                {
                    peckMode = true;
                    hoppingMode = false;
                    timeBtwPeck = 0f;
                    nextPeck = Random.Range(minTimeBtwPeck, maxTimeBtwPeck);
                }
            }
            else if (peckMode && onGround)
            {
                //play peck animation 3 times then will become stuck
                
                if(nextPeck <= timeBtwPeck && peckCounter < 3)
                {
                    Peck();
                    Debug.Log("Pecked at Player");

                    nextPeck = Random.Range(minTimeBtwPeck, maxTimeBtwPeck);
                    timeBtwPeck = 0f;
                    peckCounter++;
                } else if (peckCounter == 3)
                {
                    Stuck();
                }
                
            }
        }
        if (playerRb != null && Mathf.Abs(playerRb.velocity.x) > 0.1f)
        {
            isKnockedBack = true;
            Debug.Log("Player is being knocked back!");
        }
        else
        {
            isKnockedBack = false;
        }

    }

    //checks if boss has contact with ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !onGround)
        {
            onGround = true;
        }
    }


    // Simulate peck knockback
    private void Peck()
         
    {
        // Log player's velocity before applying knockback

        Debug.Log("Player velocity before knockback: " + playerRb.velocity);

        // Declare knockbackDirection here inside the Peck() method
        Vector2 knockbackDirection;
        if (player.transform.position.x > transform.position.x)
        {
            knockbackDirection = new Vector2(knockbackForceX, knockbackForceY); // Knock player to the right
        }
        else
        {
            knockbackDirection = new Vector2(-knockbackForceX, knockbackForceY); // Knock player to the left
        }

        // Apply knockback to player's Rigidbody2D
       // playerRb.velocity = Vector2.zero; // Reset velocity
        playerRb.AddForce(knockbackDirection, ForceMode2D.Impulse);

        Debug.Log("Applying knockback in direction: " + knockbackDirection);
        

    }




    //will make boss vulnrable for a period of time
    private void Stuck()
    {

        if (hitBoxPhase1.active == false)
        {
            tStuck = 0f;
            hitBoxPhase1.SetActive(true);
        }

        if (tStuck > timeStuck)
        {
            unStuck();
        }
    }

    //used when boss is damaged in phase 1 hitbox
    public void unStuck()
    {
        hoppingMode = true;
        peckMode = false;
        hitBoxPhase1.SetActive(false);
        runAway = true;
        peckCounter = 0;
        tStuck = 0f;
    }
}
