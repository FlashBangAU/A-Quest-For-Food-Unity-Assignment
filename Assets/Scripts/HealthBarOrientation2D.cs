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
        if (isFacingRight && Input.GetKey(KeyCode.A) || !isFacingRight && Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) { }
            else
            {

                isFacingRight = !isFacingRight;
                Vector2 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }
        }
    }
}
