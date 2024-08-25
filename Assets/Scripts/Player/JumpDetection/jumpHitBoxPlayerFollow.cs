using UnityEngine;

//the goal of this script is to prevent the jump triggers to flip with character sprite this will be done by a unattached gameobject copying the player position
public class JumpChecker : MonoBehaviour
{
    private GameObject player;
    private Vector2 offset = new Vector2(0.32458f, -0.80198f);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Get the player's position and apply the offset
            Vector2 playerPosition = player.transform.position;
            transform.position = playerPosition + offset;
        }
    }
}