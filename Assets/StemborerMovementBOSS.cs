using UnityEngine;

public class StemBorerMovementBOSS : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float chaseHeight;
    [SerializeField] float chaseDistance;

    private GameObject player;
    Vector2 tempPos;

    bool isFacingRight = true;
    bool flyOnRight;

    [SerializeField] Transform detectPos;
    [SerializeField] float detectRange;
    [SerializeField] LayerMask whatIsPlayer;

    //public HealthBarOrientation2D hBO2;

    Vector2 checkPoint;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.transform.position.x < transform.position.x)
        {
            FlipSprite();
            isFacingRight = true;
        }
        else
        {
            isFacingRight = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        if (player.transform.position.x <= transform.position.x)
        {
            flyOnRight = true;
        }
        else
        {
            flyOnRight = false;
        }

        Chase();

        if (isFacingRight && !flyOnRight || !isFacingRight && flyOnRight)
        {
            FlipSprite();
        }
    }

    private void Chase()
    {
        tempPos = player.transform.position;

        if (flyOnRight)
        {
            tempPos.x += chaseDistance;
        }
        else if (!flyOnRight)
        {
            tempPos.x += (chaseDistance * -1);
        }

        tempPos.y += chaseHeight;

        transform.position = Vector2.MoveTowards(transform.position, tempPos, speed * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(detectPos.position, detectRange);
    }

    void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        Vector2 ls = transform.localScale;
        ls.x *= -1f;
        transform.localScale = ls;
    }
}
