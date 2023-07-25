using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class UnitTypeSO : ScriptableObject {

    public Transform prefab;
    public Sprite icon;

    public float constructionTimerMax;
    public List<ResourceAmount> constructionResourceAmountCostList;

}