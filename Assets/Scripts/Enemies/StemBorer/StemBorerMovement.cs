using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemBorerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float chaseHeight;
    [SerializeField] float chaseDistance;

    private GameObject player;
    Vector2 tempPos;

    bool isFacingRight = true;
    bool flyOnRight;
    bool currentXNotNeg;

    [SerializeField] Transform detectPos;
    [SerializeField] float detectRange;
    [SerializeField] LayerMask whatIsPlayer;

    //public HealthBarOrientation2D hBO2;

    Vector2 checkPoint;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        checkPoint = transform.position;
        if (player.transform.position.x < transform.position.x)
        {
            FlipSprite();
            //hBO2.FlipSprite();
            isFacingRight = true;
            currentXNotNeg = true;
        }
        else
        {
            isFacingRight = false;
            currentXNotNeg = false;
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

        Collider2D[] playerTofight = Physics2D.OverlapCircleAll(detectPos.position, detectRange, whatIsPlayer);
        if (playerTofight.Length != 0)
        {
            Chase();
        } else if (playerTofight.Length == 0)
        {
            Neutral();
        }
    }

    private void Chase()
    {
        tempPos = player.transform.position;

        if (flyOnRight)
        {
            tempPos.x += chaseDistance;
        } else if (!flyOnRight)
        {
            tempPos.x += (chaseDistance * -1);
        }

        if (currentXNotNeg && !flyOnRight)
        {
            FlipSprite();
            currentXNotNeg = false;
        }
        else if (!currentXNotNeg && flyOnRight)
        {
            FlipSprite();
            currentXNotNeg = true;
        }

        tempPos.y += chaseHeight;

        transform.position = Vector2.MoveTowards(transform.position, tempPos, speed * Time.deltaTime);
    }

    private void Neutral()
    {
        transform.position = Vector2.MoveTowards(transform.position, checkPoint, speed * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(detectPos.position, detectRange);
    }

    void FlipSprite()
    {
        if (isFacingRight && !flyOnRight || !isFacingRight && flyOnRight)
        {
            isFacingRight = !isFacingRight;
            Vector2 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }
}
