using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealth : MonoBehaviour
{

    public float health;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void takeDamage(float damage)
    {
        health -= damage;
    }
}
