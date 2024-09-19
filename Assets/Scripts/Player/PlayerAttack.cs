using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    float timeBtwAttack;
    [SerializeField] float startTimeBtwAttack;

    [SerializeField] Transform attackPos;
    [SerializeField] LayerMask whatIsEnemies;
    [SerializeField] float attackRange;
    [SerializeField] int damage;

    [SerializeField] private Animator attackAnimation;
    public Timer timer;
    public Animator attackSlashAnimation;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                timeBtwAttack = startTimeBtwAttack;
                timer.StartTimer();
                audioManager.PlaySFX(audioManager.playerAttack);
                attackAnimation.Play("AttackAnimation");
                attackSlashAnimation.Play("PlayerAttacks");
                Collider2D[] eniemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < eniemiesToDamage.Length; i++)
                {
                    eniemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                }
            }
            if(Input.GetKeyDown(KeyCode.P) == false)
                timer.StopTimer();
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPos.position, attackRange);
    }

    // Method to change various attributes
    public void ChangeAttribute(string attributeName, float newValue)
    {
        switch (attributeName)
        {
            case "damage":
                damage = (int)newValue;
                break;
            case "attackRange":
                attackRange = newValue;
                break;
            default:
                Debug.LogWarning("Unknown attribute: " + attributeName);
                break;
        }
    }
}
