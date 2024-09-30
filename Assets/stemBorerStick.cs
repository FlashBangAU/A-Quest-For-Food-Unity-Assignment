using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stemBorerStick : MonoBehaviour
{
    Rigidbody2D rb;

    public bool isHeld = false;
    public GameObject Beak;

    public bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            Vector2 beakPos = Beak.transform.position;
            rb.transform.position = beakPos;
            rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}
