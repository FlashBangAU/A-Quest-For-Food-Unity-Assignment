using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointAnimator : MonoBehaviour
{
    private bool playerEntered = false;
    [SerializeField] private Animator litAnimation;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            if (!playerEntered) {
                //updates checkpoint animation to lit
                playerEntered = true;
                litAnimation.Play("Lit");
            }
        }
    }
}
