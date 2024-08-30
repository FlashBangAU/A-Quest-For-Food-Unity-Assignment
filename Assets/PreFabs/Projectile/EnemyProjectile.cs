using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private PolygonCollider2D pc;

    [SerializeField] public float speed;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

        pc = GetComponent<PolygonCollider2D>();
        




    }

    // Update is called once per frame
    void Update()
    {
        bool isfullycovered = CheckIfFullyCovered();

        timer += Time.deltaTime;

        if (timer > 15)
        {
            Destroy(gameObject);
        }
        
        if (isfullycovered == true)
        {
            Debug.Log("fullycovered");
            Destroy(gameObject);
        }
    }

    private bool CheckIfFullyCovered()
    {
        LayerMask Ground = LayerMask.NameToLayer("Ground");
        foreach (var Point in pc.points)
        {
            Vector3 testpoint = new Vector3(Point.x, Point.y);
            Vector3 pointtocheck = transform.position + testpoint;

            Collider2D pathCollider = Physics2D.OverlapPoint(pointtocheck, 1 << Ground);

            if(pathCollider == null)
            {
                return false;
            }
        }
        
        return true;
        

    }






    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().health -= 1;
            Destroy(gameObject);
        }
    }
}