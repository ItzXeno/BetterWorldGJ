using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    // Configurable properties
    public float damage = 10f;
    public float fireRate = 0.5f;
    public int maxAmmo = 30; // The maximum ammo capacity
    public float reloadTime = 1.5f; // Time it takes to reload
    public float recoilDistance = 0.1f;
    public float recoilSpeed = 5f;
    public float projectileSpeed = 20f; // Speed at which the projectile will move
    public GameObject projectilePrefab; // The projectile prefab to be shot
    public Transform gunEnd; // The end of the gun barrel
    public Transform gunBody; // The main body of the gun to tilt when reloading

    private float nextTimeToFire = 0f;
    private Vector3 originalPosition;
    private int currentAmmo;
    private bool isReloading = false;

    void Start()
    {
        originalPosition = gunEnd.localPosition;
        currentAmmo = maxAmmo; // Start with a full magazine
    }

    void OnEnable()
    {
        isReloading = false; // Make sure we're not set to reloading when the gun is enabled
    }

    void Update()
    {
        // Check if the gun needs to reload
        if (currentAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
            return; // Don't try to shoot while we're reloading
        }

        // Check for fire input and if it's time to fire again based on fireRate
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && !isReloading)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (isReloading)
            return; // Exit if the gun is currently reloading

        // Simulate recoil
        //StartCoroutine(Recoil());

        currentAmmo--; // Deduct an ammo count every time we shoot

        // Instantiate the projectile at the gunEnd
        GameObject projectile = Instantiate(projectilePrefab, gunEnd.position, gunEnd.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        if (projectileRb != null)
        {
            projectileRb.velocity = -transform.forward * projectileSpeed; // Shoot forward
        }
        else
        {
            Debug.LogError("Projectile prefab does not have a Rigidbody component.");
        }
    }

    IEnumerator Recoil()
    {
        // Move the gun back
        while (Vector3.Distance(gunEnd.localPosition, originalPosition + Vector3.back * recoilDistance) > 0.01f)
        {
            gunEnd.localPosition = Vector3.Lerp(gunEnd.localPosition, originalPosition + Vector3.back * recoilDistance, recoilSpeed * Time.deltaTime);
            yield return null;
        }

        // Return the gun to its original position
        while (Vector3.Distance(gunEnd.localPosition, originalPosition) > 0.01f)
        {
            gunEnd.localPosition = Vector3.Lerp(gunEnd.localPosition, originalPosition, recoilSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        Transform old = gunBody.transform;
        // Point the gun down to indicate reloading
        gunBody.Rotate(-90f, 0f, 0f);

        yield return new WaitForSeconds(reloadTime);

        // Reset ammo count and gun position after reloading
        currentAmmo = maxAmmo;
        gunBody.transform.position = old.position;
        gunBody.Rotate(90f, 0f, 0f);

        isReloading = false;
    }
    
}
