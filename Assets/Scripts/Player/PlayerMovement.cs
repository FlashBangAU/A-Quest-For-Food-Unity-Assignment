using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    [SerializeField] float moveSpeed = 5f;
    bool isFacingRight = true;
    [SerializeField]  float jumpPower = 8f;
    bool isJumping = false;
    [SerializeField] float moveSpeedInAir = 0.0007f;
    [SerializeField] float fallMultiplier = 2f;
    [SerializeField] float lowJumpMulitplier = 2f;

    float timeBtwSprint;
    [SerializeField] float startTimeBtwSprint;

    [SerializeField] private Animator jumpAnimation;
    [SerializeField] private Animator sprintAnimation;
    float jumpWait = 0;

    Rigidbody2D rb;
    private bool onLadder = false;

    //start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        jumpWait = jumpWait * Time.deltaTime;


        // Logging horizontal input
        //Debug.Log("Horizontal Input: " + horizontalInput);

        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);


        // Movement when on ladder
        if (onLadder == true)
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");
        }
        else if (!isJumping)// Horizontal movement when touching ground
        {
            horizontalInput = Input.GetAxis("Horizontal");
            if (timeBtwSprint < 0)
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    sprintAnimation.Play("SprintAnimation");
                    timeBtwSprint = startTimeBtwSprint;
                }
            }
            else
            {
                timeBtwSprint -= Time.deltaTime;
            }
        }
        // Slow control of character when airborne
        else
        {
            if (Input.GetKey(KeyCode.A) && horizontalInput < moveSpeed)
            {
                horizontalInput = horizontalInput - moveSpeedInAir;
            }
            if (Input.GetKey(KeyCode.D) && horizontalInput < moveSpeed)
            {
                horizontalInput = horizontalInput + moveSpeedInAir;
            }
        }

        // Plays jump animation
        if (isJumping)
        {
            jumpAnimation.Play("JumpAnimation");
        }

        FlipSprite();
    }


    private void FixedUpdate()
    {
        if (!onLadder)
        {
            // Player falls faster on descent
            if (rb.velocity.y < 0 && rb.velocity.y >= -5)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            // Player can control how high they jump with key hold
            else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMulitplier - 1) * Time.deltaTime;
            }

            // Logging rb.velocity.y
            //Debug.Log("Velocity Y: " + rb.velocity.y);

            // Player jumps
            if (Input.GetKey(KeyCode.W) && !isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                isJumping = true;
            }
        }
        else
        {//on ladder
            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector2(rb.velocity.x, verticalInput * moveSpeed);
            }
        }
    }


    //flip character
    void FlipSprite()
    {
        if(isFacingRight && Input.GetKey(KeyCode.A) || !isFacingRight && Input.GetKey(KeyCode.D))
        {
            isFacingRight = !isFacingRight;
            Vector2 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    // When player collides with ground, jump animation stops and player can jump again
    public void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
        //Debug.Log("Collision with ground");

        if (collision.gameObject.CompareTag("Ladder"))
        {
            onLadder = true;
            //Debug.Log("On Ladder");
        }
    }

    // Prevents player from walking off platform and jumping
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (jumpWait > 0.2)
        {
            isJumping = true;
            //Debug.Log("Left ground");
        }

        if (collision.gameObject.CompareTag("Ladder"))
        {
            onLadder = false;
            //Debug.Log("Off Ladder");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            onLadder = true;
            Debug.Log("On Ladder");
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            onLadder = false;
            Debug.Log("Off Ladder");
        }
    }

    // Method to change various attributes
    public void ChangeAttribute(string attributeName, float newValue)
    {
        switch (attributeName)
        {
            case "moveSpeed":
                moveSpeed = newValue;
                break;
            case "jumpPower":
                jumpPower = newValue;
                //lowJumpMulitplier = jumpPower - 6f;
               // fallMultiplier = 2.5f;
                break;
            default:
                Debug.LogWarning("Unknown attribute: " + attributeName);
                break;
        }
    }
}