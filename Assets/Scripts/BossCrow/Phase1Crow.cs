using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Crow : MonoBehaviour
{
    public phaseController pc;
    public Rigidbody2D rb;
    private GameObject player;
    public GameObject hitBoxPhase1;
<<<<<<< HEAD
    public AttackPlayerBoss attackPlayerBoss;
=======
>>>>>>> sound-branch
    [SerializeField] float attackRange;

    private float xBtw;
    private float yBtw;

    [SerializeField] float jumpHeight;
    [SerializeField] float jumpDistance;

<<<<<<< HEAD
    bool runAway;
    bool hoppingMode = true;
    bool peckMode;

    [SerializeField] float maxTimeBtwPeck;
    [SerializeField] float minTimeBtwPeck;
    private float timeBtwPeck;
    private float nextPeck;
    private int peckCounter;

    private float tStuck;
    public float timeStuck;

    bool onGround;
    private bool hoppingRight;
    private bool weakPointActive = false;

    [SerializeField] private Animator animatior;

    private AudioManager audioManager; // Reference for AudioManager

    // Used to connect the AudioManager reference to the existing AudioManager object
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hitBoxPhase1.SetActive(false);
=======
    [SerializeField] bool hoppingMode;
    [SerializeField] bool peckMode;

    [SerializeField] bool peckCycle;
    [SerializeField] float maxTimeBtwPeck;
    [SerializeField] float minTimeBtwPeck;
    [SerializeField] private float timeBtwPeck;
    [SerializeField] private float nextPeck;
    [SerializeField] private int peckCounter;

    [SerializeField] bool onGround;
    private bool hoppingRight;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
>>>>>>> sound-branch
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
<<<<<<< HEAD
        tStuck += Time.deltaTime;


        if (pc.phase1 == true)
        {
            if (!onGround)
            {
                animatior.Play("hopAnimation");
            }

            //bird runs to the right of screen
            if (runAway && onGround)
            {
                //play hop animation
                rb.velocity = new Vector2(jumpDistance, jumpHeight);
                audioManager.PlaySFX(audioManager.bossJump);
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
=======


        if(pc.phase1 == true)
        {
            if (hoppingMode && !peckCycle)
            {
>>>>>>> sound-branch
                if (onGround && hoppingRight)
                {
                    //play hop animation
                    rb.velocity = new Vector2(jumpDistance, jumpHeight);
<<<<<<< HEAD
                    audioManager.PlaySFX(audioManager.bossJump);
=======
>>>>>>> sound-branch
                    onGround = false;
                }
                else if (onGround && !hoppingRight)
                {
                    //play hop anmiation
                    rb.velocity = new Vector2(-jumpDistance, jumpHeight);
<<<<<<< HEAD
                    audioManager.PlaySFX(audioManager.bossJump);
                    onGround = false;
                }

                //gets range away from player for peck
=======
                    onGround = false;
                }

>>>>>>> sound-branch
                xBtw = player.transform.position.x - hitBoxPhase1.transform.position.x;
                if(xBtw < 0)
                {
                    xBtw *= -1;
                }
<<<<<<< HEAD
                //gets range away from player for peck
=======
>>>>>>> sound-branch
                yBtw = player.transform.position.y - hitBoxPhase1.transform.position.y;
                if (yBtw < 0)
                {
                    yBtw *= -1;
                }

<<<<<<< HEAD
                //checks if player is in range for peck
=======
>>>>>>> sound-branch
                if (xBtw < attackRange && yBtw < attackRange)
                {
                    peckMode = true;
                    hoppingMode = false;
<<<<<<< HEAD
=======
                    peckCycle = true;
>>>>>>> sound-branch
                    timeBtwPeck = 0f;
                    nextPeck = Random.Range(minTimeBtwPeck, maxTimeBtwPeck);
                }
            }
            else if (peckMode && onGround)
            {
<<<<<<< HEAD
                //play peck animation 3 times then will become stuck
=======
                //play peck animation
>>>>>>> sound-branch
                
                if(nextPeck <= timeBtwPeck && peckCounter < 3)
                {
                    Peck();
<<<<<<< HEAD
                    //Debug.Log("Pecked at Player");
=======
                    Debug.Log("Pecked at Player");
>>>>>>> sound-branch

                    nextPeck = Random.Range(minTimeBtwPeck, maxTimeBtwPeck);
                    timeBtwPeck = 0f;
                    peckCounter++;
<<<<<<< HEAD
                } else if (peckCounter == 3 && nextPeck <= timeBtwPeck)
                {
=======
                } else if (peckCounter == 3)
                {
                    peckCounter = 0;
>>>>>>> sound-branch
                    Stuck();
                }
                
            }
        }
    }

<<<<<<< HEAD
    //will damage player
    private void Peck()
    {
        //play peck animation
        //Debug.Log("Peck action is played");
        attackPlayerBoss.PlayerGotHit();
        animatior.Play("peckAnimation");
        audioManager.PlaySFX(audioManager.peckGround);
    }

    //will make boss vulnrable for a period of time
    private void Stuck()
    {
        if (weakPointActive == false)
        {
            Peck();
            tStuck = 0f;
            hitBoxPhase1.SetActive(true);
            weakPointActive = true;
        }
        animatior.Play("peckAnimation");

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
        weakPointActive = false;
        runAway = true;
        peckCounter = 0;
        tStuck = 0f;
        audioManager.PlaySFX(audioManager.bossCall);
    }

    //checks if boss has contact with ground
=======
>>>>>>> sound-branch
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !onGround)
        {
            onGround = true;
        }
    }
<<<<<<< HEAD
=======

    private void Peck()
    {

    }

    private void Stuck()
    {
        peckMode = false;
        peckCycle = false;
        hoppingMode = true;
    }
>>>>>>> sound-branch
}
