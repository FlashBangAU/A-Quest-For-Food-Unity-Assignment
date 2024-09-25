using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Crow : MonoBehaviour
{
    public phaseController pc;
    public Rigidbody2D rb;
    private GameObject player;
    public GameObject hitBoxPhase1;
    [SerializeField] float attackRange;

    private float xBtw;
    private float yBtw;

    [SerializeField] float jumpHeight;
    [SerializeField] float jumpDistance;

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


        if(pc.phase1 == true)
        {
            if (hoppingMode && !peckCycle)
            {
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

                xBtw = player.transform.position.x - hitBoxPhase1.transform.position.x;
                if(xBtw < 0)
                {
                    xBtw *= -1;
                }
                yBtw = player.transform.position.y - hitBoxPhase1.transform.position.y;
                if (yBtw < 0)
                {
                    yBtw *= -1;
                }

                if (xBtw < attackRange && yBtw < attackRange)
                {
                    peckMode = true;
                    hoppingMode = false;
                    peckCycle = true;
                    timeBtwPeck = 0f;
                    nextPeck = Random.Range(minTimeBtwPeck, maxTimeBtwPeck);
                }
            }
            else if (peckMode && onGround)
            {
                //play peck animation
                
                if(nextPeck <= timeBtwPeck && peckCounter < 3)
                {
                    Peck();
                    Debug.Log("Pecked at Player");

                    nextPeck = Random.Range(minTimeBtwPeck, maxTimeBtwPeck);
                    timeBtwPeck = 0f;
                    peckCounter++;
                } else if (peckCounter == 3)
                {
                    peckCounter = 0;
                    Stuck();
                }
                
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !onGround)
        {
            onGround = true;
        }
    }

    private void Peck()
    {

    }

    private void Stuck()
    {
        peckMode = false;
        peckCycle = false;
        hoppingMode = true;
    }
}
