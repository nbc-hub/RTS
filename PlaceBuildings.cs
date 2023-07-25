using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlaceBuildings : MonoBehaviour {

    public static PlaceBuildings Instance { get; private set; }


    public event EventHandler OnBuildingTypeSOChanged;

    
    private BuildingTypeSO buildingTypeSO;

    private void Awake() {
        Instance = this;
    }

    public BuildingTypeSO GetBuildingTypeSO() {
        return buildingTypeSO;
    }

    public void ClearBuildingTypeSO() {
        SetBuildingTypeSO(null);
    }

    public void SetBuildingTypeSO(BuildingTypeSO buildingTypeSO) {
        this.buildingTypeSO = buildingTypeSO;
        OnBuildingTypeSOChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Update() {
        if (buildingTypeSO != null) {
            // Has a building selected
            if (Input.GetMouseButtonDown(0) && !UtilsClass.IsPointerOverUI()) {
                Vector3 buildPosition = Mouse3D.GetMouseWorldPosition();
                if (ResourceManager.Instance.TrySpendResourceAmount(buildingTypeSO.constructionResourceAmountCostList)) {
                    // Spent resources
                    BuildingConstruction.Create(buildPosition, buildingTypeSO);
                } else {
                    TooltipCanvas.ShowTooltip_Static("Cannot afford Construction Cost!\n" + 
                        ResourceAmount.GetTooltipString(buildingTypeSO.constructionResourceAmountCostList), 3f);
                }
            }

            if (Input.GetMouseButtonDown(1)) {
                // Deselect building type
                SetBuildingTypeSO(null);
            }
        }
    }

}
