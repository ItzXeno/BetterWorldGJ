using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawning : MonoBehaviour
{
    [Header("Behaviour")]
    [SerializeField, Range(0.0f, 200.0f)] private float outerSpawnRadius = 10.0f;
    [SerializeField, Range(0.0f, 100.0f)] private float innerSpawnRadius = 5.0f;
    [SerializeField] private bool adjustInnerRadiusWithBase = true;
    [SerializeField, Range(0.0f, 100.0f)] private float upwardOffset = 50.0f;
    [SerializeField] private int wave;

    [Header("Properties")]
    [SerializeField] private GameObject enemyPrefab;
    public Transform baseTransform;
    public BaseScript baseScript;


    [Space][Header("Debugger")]
    [SerializeField] private Color outerSpawnColour = Color.red;
    [SerializeField] private Color innerSpawnColour = Color.blue;


    private void Start()
    {
        transform.position = baseTransform.position + new Vector3(0.0f, 2.0f, 0.0f);

        if(adjustInnerRadiusWithBase)
        {
            innerSpawnRadius = baseScript.placementRadius;
        }
    }

    private void Update()
    {
        if (adjustInnerRadiusWithBase)
        {
           innerSpawnRadius = baseScript.placementRadius;
        }
    }


    public void Spawn()
    {
        float locationOffsetX = Random.Range(innerSpawnRadius, outerSpawnRadius);
        float locationOffsetZ = Random.Range(innerSpawnRadius, outerSpawnRadius);

        print("X: " + locationOffsetZ + ", Z: " + locationOffsetX);

        //to distribute the object in other quadrant not only ++ quadrant
        int offsetXDir = Random.Range(-1, 2);
        int offsetZDir = Random.Range(-1, 2);

        if(offsetXDir == 0 && offsetZDir == 0)
        {
            int rnd = Random.Range(0, 100);
            if(rnd % 2 == 0)
            {
                offsetXDir = 1;
            }
            else
            {
                offsetZDir = 1;
            }

        }

        locationOffsetX *= offsetXDir;         
        locationOffsetZ *= offsetZDir;

        Vector3 locationOffset = new Vector3(locationOffsetX, 0.0f, locationOffsetZ);
        Vector3 spawnLocation = locationOffset + transform.position;


        //check if the enemy is within outerSpawnRadius 
        //if not recalculate closest boundary to the spawnLocation
        if(Vector3.Distance(spawnLocation, transform.position) > outerSpawnRadius)
        {
            float damping = 1.5f; // to push the object further in so not at the edge of the boundary
            float extraDistance = (Vector3.Distance(spawnLocation, transform.position) - outerSpawnRadius) + damping;
            Vector3 directionToCenter = transform.position - spawnLocation;
            Vector3 newPosition = spawnLocation + (directionToCenter.normalized * extraDistance);
            spawnLocation = newPosition;
        }

        //modify the upward offset
        spawnLocation = new Vector3(spawnLocation.x, spawnLocation.y + upwardOffset, spawnLocation.z);

        GameObject spawnEnemy = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnLocation, out hit, 10.0f, NavMesh.AllAreas))
        {
            spawnEnemy.transform.position = hit.position;
        }
        spawnEnemy.transform.parent = transform;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = outerSpawnColour;    
        Gizmos.DrawWireSphere(transform.position, outerSpawnRadius);

        Gizmos.color = innerSpawnColour;
        Gizmos.DrawWireSphere(transform.position, innerSpawnRadius);

        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0.0f, upwardOffset, 0.0f));
    }
}
