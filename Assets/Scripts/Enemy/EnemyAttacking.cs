using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    private EnemyBehaviour behaviour;
    public Transform rightHand;
    private BoxCollider rightHandCol;

    private bool attacking = false;
    private Transform target; 

    private void Start()
    {
        behaviour = GetComponent<EnemyBehaviour>();

        if (rightHand == null)
            rightHand = GameObject.FindGameObjectWithTag("RightHand").transform;

        rightHandCol = rightHand.GetComponent<BoxCollider>();

        if(rightHandCol == null)
        {
            rightHand.AddComponent<BoxCollider>();
            rightHandCol = rightHand.GetComponent<BoxCollider>();
        }

        rightHandCol.isTrigger = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(behaviour.Attacking)
        {
            if (other.CompareTag("Generator") || other.CompareTag("Base"))
            {
                if(other.transform == behaviour.CurrentTarget)
                {
                    GameObject targetObj = other.gameObject;
                    
                }
            }
        }


    }




}
