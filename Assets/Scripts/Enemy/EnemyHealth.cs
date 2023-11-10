using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField, Range(0.0f, 10.0f)] private float maxHealth;
    [SerializeField] private float currentHealth = 0;   

    public float MaxHealth
    {
        set
        {
            maxHealth = value;
            currentHealth = maxHealth;
        }
    }


    private void Start()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(int value)
    {
        currentHealth -= value;

        if(currentHealth < 0)
        {
            Death();
        }
    }


    private void Death()
    {
        Destroy(gameObject);
    }
}
