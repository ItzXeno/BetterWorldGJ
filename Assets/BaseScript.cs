using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    public float placementRadius = 0.1f; // Starting radius
    public GameObject raidusIndicator;
    public Material terrainMaterial;
    public GameObject building;

    public static int credits = 300;
    // Method to increase the radius
    private void Update()
    {
        UpdateRadiusVisual();
    }
    public void IncreasePlacementRadius(float amount)
    {
        placementRadius += amount;
        UpdateRadiusVisual();

    }

    

    private void UpdateRadiusVisual()
    {
       raidusIndicator.transform.localScale = new Vector3(placementRadius, placementRadius / 100, placementRadius);
        terrainMaterial.SetFloat("_Radius", placementRadius/5);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(building.transform.position, placementRadius * 100);
    }
}
