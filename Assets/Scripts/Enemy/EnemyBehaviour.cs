using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform primaryTarget;
    [SerializeField] private Transform secondaryTarget;
    [SerializeField] private Transform player;

    [Header("Behaviour")][Space]
    [SerializeField] private Transform newTarget;
    [SerializeField] private Transform currentTarget;
    [SerializeField, Range(0.0f, 10.0f)] private float playerInProximity;
    [SerializeField, Range(0.0f, 15.0f)] private float generatorProximity;    //proximity to make generator a priority
    [SerializeField, Range(0.0f, 20.0f)] private float attackRange = 5.0f;
    [SerializeField, Range(0.0f, 1.0f)] private float agentStoppingAttackRatio = 0.8f;
    [SerializeField] private bool canAttack;

    private NavMeshAgent agent;
    private Animator animate;
    public bool attack;
    public GameObject hand;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animate = GetComponent<Animator>();
        //hand.SetActive(false); 


        //New New New 
        player = GameObject.FindGameObjectWithTag("Player").transform;
        primaryTarget = GameObject.FindGameObjectWithTag("Base").transform;
        agent.SetDestination(primaryTarget.position);

    }


    private void Update()
    {
        print(agent.destination);

        if(!attack)
        {
            newTarget = UpdateTarget();
            //if(currentTarget == newTarget && IsGrounded())
            //{
            //    ChangeTarget(newTarget);    
            //}
        }

        

        if (currentTarget != newTarget)
        {
            ChangeTarget(newTarget);
        }

        attack = CanAttack();

        if(attack)
        {
            TryFacingAttackee();
        }

        animate.SetBool("Attack", attack);


        if (currentTarget == primaryTarget)
            agent.stoppingDistance = attackRange * 0.95f;
        else
            agent.stoppingDistance = attackRange * 0.8f;

    }



    private Transform UpdateTarget()
    {
        Transform mainTarget = primaryTarget;

        if(Vector3.Distance(transform.position, player.position) < playerInProximity)
        {
            return player;
        }


        Collider[] hitColliders = Physics.OverlapSphere(transform.position, generatorProximity);

        if (hitColliders.Length > 0)
        {

            List<Transform> potentialTargets = new List<Transform>();

            for(int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].CompareTag("Generator"))
                {
                    potentialTargets.Add(hitColliders[i].transform);
                }
            }

            //foreach (Collider collider in hitColliders)
            //{
            //    if (collider.CompareTag("Generator"))
            //    {

            //        return collider.transform;
            //    }
            //}

            

            if (ClosestTarget(potentialTargets) != null)
            {
                return ClosestTarget(potentialTargets);
            }
        }


        return mainTarget;
    }

    private void ChangeTarget(Transform newTarget)
    {
        agent.SetDestination(newTarget.position);

        currentTarget = newTarget;
    }


    private bool CanAttack()
    {
        bool readyToAttack = false;

        if(currentTarget != null)
        {
            if(Vector3.Distance(transform.position, currentTarget.position) < attackRange)
            {
                readyToAttack = true;
            }
        }

        return readyToAttack;
    }


    //NEW NEW NEW 
    private void TryFacingAttackee()
    {
        Vector3 directionTowards = currentTarget.position - transform.position;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, directionTowards, 0.4f, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    private Transform ClosestTarget(List<Transform> value)
    {
        //if(value.Count < 0)
        //    return null;

        Transform closest = null;

        float distance = Mathf.Infinity;

            foreach (Transform t in value)
            {
                float tDistance = Vector3.Distance(transform.position, t.position);
                if (tDistance < distance)
                {
                    closest = t;
                }
            }

        //print(closest);

        return closest;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerInProximity);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, generatorProximity);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
