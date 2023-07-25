using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Barracks : MonoBehaviour {

    private static List<Barracks> instanceList;

    private List<UnitConstruction> unitConstructionList;

    private void Awake() {
        if (instanceList == null) {
            instanceList = new List<Barracks>();
        }

        instanceList.Add(this);
        unitConstructionList = new List<UnitConstruction>();
    }

    private void Update() {
        if (unitConstructionList.Count > 0) {
            // Do progress on Unit construction
            UnitConstruction unitConstruction = unitConstructionList[0];
            unitConstruction.Update();

            if (unitConstruction.IsProgressComplete()) {
                Transform unitTransform = Instantiate(unitConstruction.GetUnitTypeSO().prefab, transform.position, Quaternion.identity);
                Vector3 movePosition = transform.position + UtilsClass.GetRandomDirXZ() * Random.Range(2f, 5f);
                RTSUnit chopChopUnit = unitTransform.GetComponent<RTSUnit>();
                UtilsClass.ActionNextFrame(() => {
                    chopChopUnit.NormalMoveTo(movePosition);
                });
                unitConstructionList.Remove(unitConstruction);
            }
        }
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void AddUnitToQueue(UnitTypeSO unitTypeSO) {
        unitConstructionList.Add(new UnitConstruction(unitTypeSO));
    }

    public List<UnitConstruction> GetUnitConstructionList() {
        return unitConstructionList;
    }


    public class UnitConstruction {

        private UnitTypeSO unitTypeSO;
        private float constructionTime;

        public UnitConstruction(UnitTypeSO unitTypeSO) {
            this.unitTypeSO = unitTypeSO;
        }

        public void Update() {
            constructionTime += Time.deltaTime;
        }

        public float GetProgress() {
            return constructionTime / unitTypeSO.constructionTimerMax;
        }

        public UnitTypeSO GetUnitTypeSO() {
            return unitTypeSO;
        }

        public bool IsProgressComplete() {
            return constructionTime >= unitTypeSO.constructionTimerMax;
        }

    }

}