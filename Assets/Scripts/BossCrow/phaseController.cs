using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class phaseController : MonoBehaviour
{
    [SerializeField] public GameObject deadSprite;

    [SerializeField] private GameObject completeLevel;

    [SerializeField] public GameObject healthBarGO;
    [SerializeField] public HealthBar healthBar;
    [SerializeField] int maxHealth;

    [SerializeField] public int health;
    [SerializeField] public bool phase0;
    [SerializeField] public bool phase1;
    [SerializeField] public bool phase2;
    [SerializeField] public bool phase3;
    bool isDefeated = false;

    void Start()
    {
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.UpdateHealth(health);
        }

        healthBarGO.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (phase1)
        {
            healthBarGO.SetActive(true);
        }
        else if (phase2)
        {

        }else if (phase3)
        {

        }else if (isDefeated)
        {
            completeLevel.transform.position = new Vector2(44, 1.5f);

            deadSprite.SetActive(true);
            deadSprite.transform.position = gameObject.transform.position;

            healthBarGO.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void removehealth()
    {
        health --;
        if(health <= 0)
        {
            phase3 = false;
            isDefeated = true;
        }
        else if (health <= 4)
        {
            phase2 = false;
            phase3 = true;
        }
        else if (health <= 8)
        {
            phase1 = false;
            phase2 = true;
        }

        healthBar.UpdateHealth(health);
    }
}
