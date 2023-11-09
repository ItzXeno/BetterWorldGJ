using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [Header("Behaviour")]
    [SerializeField, Range(0.0f, 200.0f)] private float outerSpawnRadius = 10.0f;
    [SerializeField, Range(0.0f, 100.0f)] private float inerSpawnRadius = 5.0f;
    [SerializeField] private int wave;

    [Header("Properties")]
    [SerializeField] private GameObject enemyPrefab;
    public Transform baseTransform;

    private float timer = 0.0f; 


    [Header("Debugger")][Space]
    [SerializeField] private Color outerSpawnColour = Color.red;
    [SerializeField] private Color inerSpawnColour = Color.blue;


    private void Start()
    {
        transform.position = baseTransform.position + new Vector3(0.0f, 2.0f, 0.0f);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > 3.0f)
        {
            Spawn();
            timer = 0.0f;
        }


    }

    private void Spawn()
    {
        float locationOffsetX = Random.Range(inerSpawnRadius, outerSpawnRadius);
        float locationOffsetZ = Random.Range(inerSpawnRadius, outerSpawnRadius);

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

        GameObject spawnEnemy = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
        spawnEnemy.transform.parent = transform;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = outerSpawnColour;    
        Gizmos.DrawWireSphere(transform.position, outerSpawnRadius);

        Gizmos.color = inerSpawnColour;
        Gizmos.DrawWireSphere(transform.position, inerSpawnRadius);
    }
}
