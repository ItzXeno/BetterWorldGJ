using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLogic : MonoBehaviour
{
    public BaseScript gameManager; // Assign this in the inspector
    [SerializeField] float value;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // On left click
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 pointToPlace = hit.point;
                if (CanPlaceTower(pointToPlace))
                {
                    PlaceTower(pointToPlace);
                }
                else
                {
                    Debug.Log("Can't place tower here!");
                }
            }
        }
    }

    bool CanPlaceTower(Vector3 position)
    {
        Vector3 basePosition = gameManager.building.transform.position; 
        return Vector3.Distance(basePosition, position) <= gameManager.placementRadius * value; // Check the distance against placementRadius
    }

    void PlaceTower(Vector3 position)
    {
        print("Can Place Tower Here");
        
    }
}
