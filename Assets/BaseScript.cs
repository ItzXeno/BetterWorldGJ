using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    public float placementRadius = 0.1f; 
    public GameObject raidusIndicator;
    public Material terrainMaterial;
    public GameObject building;

    public static int credits = 3000;
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

    public void AddGeneratorRadius()
    {
        placementRadius += 0.3f;
        UpdateRadiusVisual();
        Debug.Log("Generator placed. New radius: " + placementRadius);
    }

    
    public void RemoveGeneratorRadius()
    {
        placementRadius -= 0.3f;
        UpdateRadiusVisual();
        Debug.Log("Generator destroyed. New radius: " + placementRadius);
    }
}
