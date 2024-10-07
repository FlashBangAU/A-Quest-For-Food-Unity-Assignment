using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpThroughPlatform : MonoBehaviour
{
    private GameObject player;
    public GameObject platform;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > transform.position.y)
        {
            platform.SetActive(true);
        }
        else
        {
            platform.SetActive(false);
        }
    }
}
