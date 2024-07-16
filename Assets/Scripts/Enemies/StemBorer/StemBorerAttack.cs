using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemBorerAttack : MonoBehaviour
{
    public GameObject projectile;
    [SerializeField] public Transform projPos;

    private float timer;
    private GameObject player;

    float timeAmount;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeAmount = nextShotTime();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 8)
        {
            timer += Time.deltaTime;

            if (timer > timeAmount)
            {
                timer = 0;
                shoot();
                timeAmount = nextShotTime();
            }
        }
    }

    void shoot()
    {
        if(projectile != null) 
        {
            Instantiate(projectile, projPos.position, Quaternion.identity);
        }
    }

    float nextShotTime()
    {
        return Random.Range(3, 6);
    }
}
