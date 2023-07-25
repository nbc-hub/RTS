using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class BuildingSelectionManager : MonoBehaviour {

    public static BuildingSelectionManager Instance { get; private set; }


    private Camera mainCamera;

    private void Awake() {
        Instance = this;

        mainCamera = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonUp(0)) {
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit)) {
                if (raycastHit.collider.TryGetComponent<Barracks>(out Barracks barracks)) {
                    BarracksUI.Instance.Show(barracks);
                }
            }
        }
    }

}
