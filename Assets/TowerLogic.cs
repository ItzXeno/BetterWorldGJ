using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TowerLogic : MonoBehaviour
{
    public BaseScript gameManager; 
    [SerializeField] float value;
    buildManager buildMan;
    private void Start()
    {
        buildMan = gameObject.GetComponent<buildManager>();
    }
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
                    buildMan.PlaceTurret();
                }
                else
                {
                    Debug.Log("Can't place tower here!");
                }
            }
        }
    }

    public bool CanPlaceTower(Vector3 position)
    {
        Vector3 basePosition = gameManager.building.transform.position; 
        return Vector3.Distance(basePosition, position) <= gameManager.placementRadius * value; 
    }

   /* public void PlaceTower(Vector3 position, GameObject towerPrefab)
    {
        if (CanPlaceTower(position))
        {
            Instantiate(towerPrefab, position, Quaternion.identity);
            Debug.Log("Tower placed.");
        }
        else
        {
            Debug.Log("Can't place tower here!");
        }
    }*/
}
