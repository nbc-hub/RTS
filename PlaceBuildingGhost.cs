using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuildingGhost : MonoBehaviour {

    private Transform visual;
    private BuildingTypeSO buildingTypeSO;

    private void Start() {
        RefreshVisual();

        PlaceBuildings.Instance.OnBuildingTypeSOChanged += PlaceBuildings_OnBuildingTypeSOChanged;
    }

    private void PlaceBuildings_OnBuildingTypeSOChanged(object sender, System.EventArgs e) {
        RefreshVisual();
    }

    private void LateUpdate() {
        Vector3 targetPosition = Mouse3D.GetMouseWorldPosition();
        targetPosition.y = .3f;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 30f);

        //transform.rotation = Quaternion.Lerp(transform.rotation, GridBuildingSystem3D.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
    }

    private void RefreshVisual() {
        if (visual != null) {
            Destroy(visual.gameObject);
            visual = null;
        }

        BuildingTypeSO buildingTypeSO = PlaceBuildings.Instance.GetBuildingTypeSO();

        if (buildingTypeSO != null) {
            visual = Instantiate(buildingTypeSO.visual, Vector3.zero, Quaternion.identity);
            visual.parent = transform;
            visual.localPosition = Vector3.zero;
            visual.localEulerAngles = Vector3.zero;
            SetLayerRecursive(visual.gameObject, 15);
        }
    }

    private void SetLayerRecursive(GameObject targetGameObject, int layer) {
        targetGameObject.layer = layer;
        foreach (Transform child in targetGameObject.transform) {
            SetLayerRecursive(child.gameObject, layer);
        }
    }

}
