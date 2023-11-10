using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    public EnemyBehaviour behaviour;
    private BoxCollider rightHandCol;

    [SerializeField, Range(0.0f, 50.0f)] private float damage = 20.0f;


    private void Start()
    {
        //rightHandCol = GetComponent<BoxCollider>();
    }


    public void DealDamage()
    {
        if (behaviour.Attacking)
        {

            if(behaviour.CurrentTarget)
            {
                //TurretHealth health = behaviour.CurrentTarget.GetComponent<TurretHealth>();
                if(TryGetComponent<TurretHealth>(out TurretHealth health))
                {
                    health.takeDamage(damage);
                }
                else if(behaviour.CurrentTarget.parent.TryGetComponent<Damagable>(out Damagable damagable))
                {
                    damagable.ApplyDamage(damage);
                }
                else
                {
                    Debug.Log("Target doesn't have the TurretHealth  OR the Damagable");
                }
            }

        }
    }






}
