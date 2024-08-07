using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Basic Patrol Enemy
 * Create Empty Game Objects and assign them to pointA and pointB.
 * Make sure pointA is on the left and pointB is on the right.
 * I imagine this enemy will just be used for basic side to side movement for now, but this code can be modified
 * for more than 2 points in the future if needed.
 */
public class EnemyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Moves caterpillar from its position to the set 'currentPoint' (Next waypoint)
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        //Changes currentPoint value to the next waypoint.
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
        }
    }

    //Function for flipping enemy sprite
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    //Makes the waypoints visible without having to select them in the editor
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position,  pointB.transform.position);
    }
}