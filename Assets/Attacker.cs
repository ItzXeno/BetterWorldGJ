using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform shootingPoint; 
    public float attackRange = 10f;
    public float fireRate = 1f;
    public float bulletSpeed = 20f;
    public float turnSpeed = 10f;
    public GameObject head;
    private float fireCooldown = 0f;

    void Update()
    {
       
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            
            RotateTowards(nearestEnemy.transform.position);

            
            if (fireCooldown <= 0f)
            {
                ShootAt(nearestEnemy);
                fireCooldown = 1f / fireRate; 
            }
        }

        
        if (fireCooldown > 0f)
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float minDistance = attackRange;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < minDistance)
            {
                nearestEnemy = enemy;
                minDistance = distanceToEnemy;
            }
        }
        return nearestEnemy;
    }

    void RotateTowards(Vector3 target)
    {
        /*Vector3 directionToTarget = target - head.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, -Vector3.right);*/
        head.transform.LookAt(target);
       /* head.transform.rotation = Quaternion.Slerp(head.transform.rotation, targetRotation, Time.deltaTime * turnSpeed);*/
    }

    void ShootAt(GameObject enemy)
    {
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
           
            Vector3 direction = (enemy.transform.position - shootingPoint.position).normalized;
            bulletRb.velocity = direction * bulletSpeed;
        }
    }
}
