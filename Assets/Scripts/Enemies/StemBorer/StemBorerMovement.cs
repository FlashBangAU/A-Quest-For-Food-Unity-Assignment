using UnityEngine;

public class StemBorerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float chaseHeight;
    [SerializeField] private float chaseDistance;

    [SerializeField] private Transform detectPos;
    [SerializeField] private float detectRange;
    [SerializeField] private LayerMask whatIsPlayer;

    private GameObject player;
    private Vector2 checkPoint;

    private bool isFacingRight = true;
    private bool flyOnRight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject not found. Please make sure it has the 'Player' tag.");
            return;
        }

        checkPoint = transform.position;
        UpdateFacingDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        // Determine the direction to move based on player's position
        flyOnRight = player.transform.position.x > transform.position.x;

        // Check for nearby players
        Collider2D[] playerToFight = Physics2D.OverlapCircleAll(detectPos.position, detectRange, whatIsPlayer);
        if (playerToFight.Length > 0)
        {
            Chase();
        }
        else
        {
            Neutral();
        }
    }

    private void Chase()
    {
        Vector2 targetPos = player.transform.position;

        // Adjust target position based on direction
        targetPos.x += (flyOnRight ? chaseDistance : -chaseDistance);
        targetPos.y += chaseHeight;

        // Move towards the target position
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // Flip sprite if necessary
        if (isFacingRight != flyOnRight)
        {
            FlipSprite();
        }
    }

    private void Neutral()
    {
        transform.position = Vector2.MoveTowards(transform.position, checkPoint, speed * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        if (detectPos != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(detectPos.position, detectRange);
        }
    }

    private void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void UpdateFacingDirection()
    {
        isFacingRight = player.transform.position.x >= transform.position.x;
        if (!isFacingRight)
        {
            FlipSprite();
        }
    }
}
