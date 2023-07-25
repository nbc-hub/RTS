using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTS : MonoBehaviour {

    [SerializeField] private UnitSelectionManager unitSelectionManager;

    private Camera mainCamera;

    private void Awake() {
        mainCamera = Camera.main;

        Application.targetFrameRate = 100;
    }

    private void Update() {
        // Test Orders
        if (Input.GetMouseButtonDown(1)) {
            // Right Mouse Button Click
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit)) {
                // Raycast hit something
                // Normal move action
                Action<RTSUnit> unitAction = (RTSUnit unit) => unit.NormalMoveTo(Mouse3D.GetMouseWorldPosition());

                // Test Resource Node Order
                if (raycastHit.collider.TryGetComponent<ResourceNode>(out ResourceNode resourceNode)) {
                    
                    unitAction = (RTSUnit unit) => {
                        if (unit.TryGetComponent<GatheringUnitBehaviour>(out GatheringUnitBehaviour gatheringUnitBehaviour)) {
                            resourceNode.transform.Find("Selected").gameObject.SetActive(true);
                            StartCoroutine(selectedIE(resourceNode.transform.Find("Selected").gameObject));
                            gatheringUnitBehaviour.SetGatherResources(resourceNode);
                        }
                    };
                }

                // Test Building Construction Order
                if (raycastHit.collider.TryGetComponent(out BuildingConstruction buildingConstruction)) {
                    unitAction = (RTSUnit unit) => {
                        if (unit.TryGetComponent<ConstructionUnitBehaviour>(out ConstructionUnitBehaviour constructionUnitBehaviour)) {
                            constructionUnitBehaviour.SetBuildingConstruction(buildingConstruction);
                        }
                    };
                }

                // Test Attack Enemy Order
                if (raycastHit.collider.TryGetComponent<RTSUnit>(out RTSUnit targetChopChopUnit)) {
                    if (targetChopChopUnit.IsEnemy()) {
                        unitAction = (RTSUnit unit) => {
                            if (unit.TryGetComponent<AttackingUnitBehaviour>(out AttackingUnitBehaviour attackingUnitBehaviour)) {
                                attackingUnitBehaviour.SetEnemyTarget(targetChopChopUnit);
                            }
                        };
                    }
                }

                // Execute Action
                foreach (RTSUnit chopChopUnit in unitSelectionManager.GetSelectedUnitList()) {
                    if (chopChopUnit.IsDead()) continue;
                    unitAction(chopChopUnit);
                }
            }
        }
    }

    IEnumerator selectedIE(GameObject selectedObject){
        yield return new WaitForSeconds(0.3f);
        selectedObject.SetActive(false);

    }
}
