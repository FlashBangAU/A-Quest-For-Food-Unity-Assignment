using UnityEngine;

public class BackGroundMovement : MonoBehaviour
{
    GameObject player;
    Rigidbody2D playerRigidbody;

    public GameObject closeMountain;
    public GameObject farMountain;

    // Fraction of player's speed
    [SerializeField] float closeMovementFractionY;
    [SerializeField] float closeMovementFractionX;
    [SerializeField] float farMovementFractionY;
    [SerializeField] float farMovementFractionX;

    Vector2 respawnPositionClose;
    Vector2 respawnPositionFar;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        respawnPositionClose = closeMountain.transform.position;
        respawnPositionFar = farMountain.transform.position;
    }

    void Update()
    {
        // Calculate the velocity of the player
        Vector2 playerVelocity = playerRigidbody.velocity;

        // Apply a fraction of the player's velocity to the mountains
        Vector3 closeMountainVelocity = new Vector3(playerVelocity.x * closeMovementFractionX, playerVelocity.y * closeMovementFractionY);
        closeMountain.transform.position += closeMountainVelocity * Time.deltaTime;

        Vector3 farMountainVelocity = new Vector3(playerVelocity.x * farMovementFractionX, playerVelocity.y * farMovementFractionY);
        farMountain.transform.position += farMountainVelocity * Time.deltaTime;
    }

    public void NewRespawn()
    {
        respawnPositionClose = closeMountain.transform.position;
        respawnPositionFar = farMountain.transform.position;
    }

    public void Respawn()
    {
        closeMountain.transform.position = respawnPositionClose;
        farMountain.transform.position = respawnPositionFar;
    }
}
