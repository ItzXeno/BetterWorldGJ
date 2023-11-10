    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using static UnityEditor.ObjectChangeEventStream;

    public class buildManager : MonoBehaviour
    {
        public GameObject buildUI; 
        public GameObject generatorHologramPrefab; 
        public GameObject attackerHologramPrefab;
        public TowerLogic towerLogic;
        private GameObject currentHologram;
        private Camera playerCamera;
        private const int generatorCost = 150;
        private const int attackerCost = 200;
        void Start()
        {
            playerCamera = Camera.main;
            buildUI.SetActive(false);
            towerLogic = GetComponent<TowerLogic>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Debug.Log("Toggled build mode.");
                ToggleBuildMode();
            }

            if (buildUI.activeSelf)
            {
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
                {
                    if (currentHologram != null)
                    {
                        currentHologram.transform.position = hit.point;
                    }

                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        Debug.Log("Q pressed for generator.");
                        StartBuilding(generatorHologramPrefab, generatorCost);
                    }
                    else if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("E pressed for attacker.");
                        StartBuilding(attackerHologramPrefab, attackerCost);
                    }
                    else if (Input.GetMouseButtonDown(0) && currentHologram != null)
                    {
                        Debug.Log("Attempting to place turret.");
                        PlaceTurret();
                    }
                }
                else
                {
                    Debug.Log("Raycast did not hit the ground layer.");
                }
            }
        }

        void ToggleBuildMode()
        {
            bool isBuildUIActive = buildUI.activeSelf;
            Destroy(currentHologram);
            buildUI.SetActive(!isBuildUIActive);

        
            /*if (!isBuildUIActive || (isBuildUIActive && currentHologram != null))
            {
            
                
            
            }*/
        }

        public void StartBuilding(GameObject hologramPrefab, int cost)
        {
            if (BaseScript.credits >= cost)
            {
                if (currentHologram != null)
                {
                    Destroy(currentHologram);
                }
                currentHologram = Instantiate(hologramPrefab);
                currentHologram.GetComponent<Hologram>().SetCost(cost); // Assuming Hologram script has SetCost method
            }
            else 
            {
                Debug.Log("Not enough credits to build this turret.");
            }
        }

    public void PlaceTurret()
    {
        if (currentHologram != null)
        {
            if (towerLogic.CanPlaceTower(currentHologram.transform.position))
            {
                Hologram hologramScript = currentHologram.GetComponent<Hologram>();
                if (hologramScript != null)
                {
                    // This condition should check if the player has enough credits
                    if (BaseScript.credits >= hologramScript.Cost)
                    {
                        // Instantiate the actual turret at the hologram's position
                        Instantiate(hologramScript.turretPrefab, currentHologram.transform.position, Quaternion.identity);
                        // Deduct the cost of the turret
                        BaseScript.credits -= hologramScript.Cost;
                        Debug.Log($"Turret placed. Credits left: {BaseScript.credits}");

                        Destroy(currentHologram); // Destroy the hologram after placement
                    }
                    else
                    {
                        Debug.Log("Not enough credits to place this turret.");
                    }
                }
            }
            else
            {
                Debug.Log("Can't place tower here!");
            }
        }
    } 
}
