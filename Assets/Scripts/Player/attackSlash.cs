using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attckSlash : MonoBehaviour
{
    [SerializeField] private Animator attackAnimation;
    // Start is called before the first frame update
    public void playAnimation()
    {
        attackAnimation.Play("PlayerAttacks");
    }
}
