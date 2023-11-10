using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private float _initialHealth;
    private float _currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        _currentHealth = _initialHealth;
    }

    public void ApplyDamage(float damage)
    {
        if (_currentHealth <= 0) return;
        _currentHealth -= damage;


        if (_currentHealth <= 0)
        {
            Destruct();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void Destruct()
    {
        Destroy(gameObject);
    }
}
