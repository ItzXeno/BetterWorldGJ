using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemySpawning))]
public class WaveManager : MonoBehaviour
{
    [SerializeField] private int waveNumber = 1;
    private int currentWave = 0;
    [SerializeField, Range(0.0f, 10.0f)] private float spawnEnemiesInterval = 2.0f;
    [SerializeField, Range(1, 20)] private int minEnemiesPerSpawn = 1;
    public int currentSpawnCount;
    private float spawnTimer = 0.0f;
    private EnemySpawning spawner;


    public int WaveNumber
    {
        get { return waveNumber; }
        set { waveNumber = value; }
    }


    private void Start()
    {
        spawner = GetComponent<EnemySpawning>();    
    }


    private void Update()
    {
        if(currentWave != waveNumber)
        {
            currentSpawnCount = DynamicEnemySpawnCount();
            currentWave = waveNumber;
        }


        if(spawner != null)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnEnemiesInterval)
            {
                StartCoroutine(SpawnEnemies());
                spawnTimer = 0.0f;
            }
        }

    }



    private IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < minEnemiesPerSpawn; i++)
        {
            spawner.Spawn();
            yield return new WaitForSeconds(0.7f);
        }
    }


    private int DynamicEnemySpawnCount()
    {
        //change the number of enemies per wave 
        int currentWave = waveNumber;
        int a = 0;
        int b = 0;

        if(minEnemiesPerSpawn % 2 == 1)
        {
            a = (minEnemiesPerSpawn - 1) / 2;
            b = a + 1;
        }
        else
            a = b = minEnemiesPerSpawn / 2;

        int dynamicSpawnCount = a * (currentWave) + b;
        return dynamicSpawnCount;
    }

}
