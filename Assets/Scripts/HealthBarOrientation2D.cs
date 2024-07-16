using UnityEngine;

public class HealthBarOrientation2D : MonoBehaviour
{
    bool isFacingRight = true;
    [SerializeField] bool isPlayer;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.transform.position.x > transform.position.x && !isPlayer)
        {
            FlipSprite();
            isFacingRight = true;
        }
        else if (!isPlayer)
        {
            isFacingRight = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FlipSprite();
    }

    public void FlipSprite()
    {
        if (isFacingRight && Input.GetKey(KeyCode.A) && isPlayer || !isFacingRight && Input.GetKey(KeyCode.D) && isPlayer)
        {
            isFacingRight = !isFacingRight;
            Vector2 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
        else if (!isPlayer && player.transform.position.x < transform.position.x && !isFacingRight || !isPlayer && player.transform.position.x > transform.position.x && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector2 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        } 
    }
}
