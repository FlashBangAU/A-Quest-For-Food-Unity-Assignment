using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Phase2Crow : MonoBehaviour
{
    Rigidbody2D rb;

    public GameObject branch1;
    public GameObject branch2;
    public GameObject hitBoxPhase2;
    public GameObject stick;
    public GameObject beak;

    private Vector2 stickOffset;
    [SerializeField] float stickXOffset;
    [SerializeField] float stickYOffset;

    public phaseController pc;
    [SerializeField] stemborerStick sba;

    [SerializeField] float flySpeed;

    private bool stickHeld = false;
    [SerializeField] float detectRange;
    [SerializeField] LayerMask whatIsStick;
    bool stickOnGround;

    [SerializeField] float flyingHeight;
    float nextFlyPosX;
    Vector3 nextFlyPos;
    [SerializeField] float minXDistanceFlyPos;

    private float timeTillNextDrop;
    [SerializeField] private float timeToPickup;
    private float waitToPickup;
    bool timerOn = false;

    public bool isFacingRight = false;

    [SerializeField] private Animator animator;

    private AudioManager audioManager; // Reference for AudioManager

    // Used to connect the AudioManager reference to the existing AudioManager object
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SelectBranchPos();
    }

    // Update is called once per frame
    void Update()
    {
        stickOnGround = sba.onGround;

        stickOffset = new Vector2(stick.transform.position.x + stickXOffset, stick.transform.position.y + stickYOffset);


        timeTillNextDrop -= Time.deltaTime;
        if (timerOn)
        {
            waitToPickup += Time.deltaTime;
        }

        if (pc.phase2 == true)
        {
            //pick up stemborer stick
            if (!stickHeld && waitToPickup >= timeToPickup && stickOnGround)
            {
                Debug.Log("pickup stick");
                PickUpStick();
            }
            //fly down to pickup stick
            else if (!stickHeld && stickOnGround)
            {
                Debug.Log("fly down to stick");
                Collider2D[] stickToCollect = Physics2D.OverlapCircleAll(beak.transform.position, detectRange, whatIsStick);
                transform.position = Vector3.MoveTowards(transform.position, stickOffset, flySpeed * Time.deltaTime);
                if (stickToCollect.Length != 0  && stickOnGround)
                {
                    //start wait time to pickup stick
                    rb.gravityScale = 1;
                    timerOn = true;
                    hitBoxPhase2.SetActive(true);
                    animator.Play("peckAnimation");
                }
                else
                {
                    animator.Play("flyingAnimation");
                }
                if (transform.position.x > stick.transform.position.x && isFacingRight || transform.position.x < stick.transform.position.x && !isFacingRight)
                {
                    FlipSprite();
                }
            }

            //drop stemborer stick
            else if (timeTillNextDrop <= 0f && stickHeld)
            {
                Debug.Log("Drop stick");
                waitToPickup = 0f;
                DropStick();
            }

            else if (!stickHeld && !stickOnGround)
            {
                animator.Play("flyingAnimation");

                transform.position = Vector2.MoveTowards(transform.position, nextFlyPos, flySpeed * Time.deltaTime);
                if (transform.position == nextFlyPos)
                {
                    Debug.Log("flying to new position");
                    SelectBranchPos();
                }
            }

            //fly to random location between branches after picking up stick
            else if (stickHeld && rb.transform.position.y <= flyingHeight || stickHeld && rb.transform.position.y >= flyingHeight)
            {
                animator.Play("flyingAnimation");
                transform.position = Vector2.MoveTowards(transform.position, nextFlyPos, flySpeed * Time.deltaTime);
                if (transform.position == nextFlyPos)
                {
                    Debug.Log("flying to new position");
                    SelectBranchPos();
                }
                else
                {
                    if (transform.position.x > nextFlyPos.x && isFacingRight || transform.position.x < nextFlyPos.x && !isFacingRight)
                    {
                        FlipSprite();
                    }
                }
            }
        }
        else
        {
            stickHeld = false;
            sba.isHeld = false;
        }
    }

    public void PickUpStick()
    {
        timeTillNextDrop = 25f;
        stickHeld = true;
        sba.isHeld = true;
        rb.gravityScale = 0;
        hitBoxPhase2.SetActive(false);
        audioManager.PlaySFX(audioManager.bossCall);
    }

    private void DropStick()
    {
        stickHeld = false;
        sba.isHeld = false;
        timerOn = false;
        audioManager.PlaySFX(audioManager.bossCall);
    }

    private void SelectBranchPos()
    {
        nextFlyPosX = Random.Range(branch1.transform.position.x, branch2.transform.position.x);
        if (nextFlyPosX < rb.transform.position.x - minXDistanceFlyPos || nextFlyPosX > rb.transform.position.x + minXDistanceFlyPos)
        {
            SelectBranchPos();
        }
        nextFlyPos = new Vector3(nextFlyPosX, flyingHeight, 0);
    }

    private void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        Vector2 ls = transform.localScale;
        ls.x *= -1f;
        transform.localScale = ls;

        stickXOffset *= -1;
    }
}
