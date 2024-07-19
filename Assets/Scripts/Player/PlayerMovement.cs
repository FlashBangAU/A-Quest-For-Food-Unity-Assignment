using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
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

    public JumpBottom jumpBottom;
    public JumpLeft jumpLeft;
    public JumpRight jumpRight;

    Rigidbody2D rb;

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


        // Horizontal movement when touching ground
        if (!isJumping)
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
            if (Input.GetKey(KeyCode.A) && horizontalInput < moveSpeed && rb.velocity.x > -6)
            {
                horizontalInput = horizontalInput - moveSpeedInAir;
            }
            if (Input.GetKey(KeyCode.D) && horizontalInput < moveSpeed && rb.velocity.x < 6)
            {
                horizontalInput = horizontalInput + moveSpeedInAir;
            }
        }

        // Plays jump animation
        if (isJumping)
        {
            jumpAnimation.Play("JumpAnimation");
        }


        //Debug.Log("Can Jump: "+jumpLeft.canJump+"  "+jumpBottom.canJump+"  "+jumpRight.canJump);
        //ensures all colliders are false for jump animation
        if(jumpBottom.canJump == false && jumpLeft.canJump == false && jumpRight.canJump == false)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        //Debug.Log("Velocity X: " + rb.velocity.x);

        FlipSprite();
    }


    private void FixedUpdate()
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

        // Player Bottom Jump
        if (!isJumping && Input.GetKey(KeyCode.W) && jumpBottom.canJump == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;
        }

        // Player Left Jump
        if (!isJumping && Input.GetKey(KeyCode.W) && jumpLeft.canJump == true)
        {
            if (Input.GetKey(KeyCode.A))
            { 
                rb.velocity = new Vector2(0, jumpPower);
                isJumping = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(4, jumpPower);
                isJumping = true;
            }
        }

        // Player Right Jump
        if (!isJumping && Input.GetKey(KeyCode.W) && jumpRight.canJump == true)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-4, jumpPower);
                isJumping = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(0, jumpPower);
                isJumping = true;
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
    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    isJumping = false;
    //    Debug.Log("Collision with ground");
    //}

    // Prevents player from walking off platform and jumping
    public void OnCollisionExit2D(Collision2D collision)
    {
        isJumping = true;
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