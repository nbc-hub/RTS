using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BuildingTypeSO : ScriptableObject {

    public Transform prefab;
    public Transform visual;

    public float constructionProgressMax;
    public float constructionDistanceOffset;
    public List<ResourceAmount> constructionResourceAmountCostList;
}