using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    float timeBtwAttack;
    [SerializeField] float startTimeBtwAttack;
    float timeBtwAttackSpecial;
    [SerializeField] float startTimeBtwAttackSpecial;

    [SerializeField] Transform attackPos;
    [SerializeField] LayerMask whatIsEnemies;
    [SerializeField] LayerMask whatIsBossHitBox;
    [SerializeField] float attackRange;
    [SerializeField] int damage;

    [SerializeField] private Animator attackAnimation;
    public Timer timerNormAtk;
    public Timer timerSpecialAtk;
    public Animator attackSlashAnimation;

    [SerializeField] bool isLevel5 = false;
    GameObject crowBoss;
    bool phase3;
    public GameObject projectileForBossFight;
    [SerializeField] Transform projPos;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (isLevel5)
        {
            crowBoss = GameObject.FindGameObjectWithTag("BossSpeicalHitBox");
            phase3 = crowBoss.GetComponent<phaseController>().phase3;
        }
    }

    void Update()
    {
        HandleNormalAttack();
        HandleSpecialAttack();

        if(isLevel5)
        {
            phase3 = crowBoss.GetComponent<phaseController>().phase3;
        }
    }

    private void HandleNormalAttack()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                timeBtwAttack = startTimeBtwAttack;
                timerNormAtk.StartTimer();
                audioManager.PlaySFX(audioManager.playerAttack);
                attackAnimation.Play("AttackAnimation");
                attackSlashAnimation.Play("PlayerAttacks");
                DamageEnemies(whatIsEnemies);
                DamageBoss(whatIsBossHitBox);
            }
            if (Input.GetKeyDown(KeyCode.P) == false)
            {
                timerNormAtk.StopTimer();
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void HandleSpecialAttack()
    {
        if (isLevel5)
        {
            if (timeBtwAttackSpecial <= 0 && phase3)
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    timeBtwAttackSpecial = startTimeBtwAttackSpecial;
                    timerSpecialAtk.StartTimer();
                    Instantiate(projectileForBossFight, projPos.position, Quaternion.identity);
                }
                if (Input.GetKeyDown(KeyCode.P) == false)
                {
                    timerSpecialAtk.StopTimer();
                }
            }else if (!phase3)
            {
                timerSpecialAtk.StopTimer();
            }
            else
            {
                timeBtwAttackSpecial -= Time.deltaTime;
            }
        }
    }

    private void DamageEnemies(LayerMask enemiesLayer)
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemiesLayer);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }

    private void DamageBoss(LayerMask bossLayer)
    {
        Collider2D[] bossesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, bossLayer);
        foreach (Collider2D boss in bossesToDamage)
        {
            boss.GetComponent<checkIfHit>().bossGotHit();
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
