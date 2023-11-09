using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{
    public GameObject turretPrefab; // The actual turret prefab to instantiate when placing

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Ground"))) // Ensure your ground has the "Ground" layer
        {
            this.transform.position = hit.point;
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal); // Align with ground normal
        }
    }
}
