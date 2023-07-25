using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManagerUI : MonoBehaviour {

    private TextMeshProUGUI resourceAmountText;
    private Dictionary<ResourceTypeSO, TextMeshProUGUI> resourceTextDic;

    private void Awake() {
        resourceTextDic = new Dictionary<ResourceTypeSO, TextMeshProUGUI>();
    }

    private void Start() {
        resourceTextDic[Assets.Instance.resourceTypeSO_Refs.wood] = transform.Find("Wood").Find("Text").GetComponent<TextMeshProUGUI>();
        resourceTextDic[Assets.Instance.resourceTypeSO_Refs.stone] = transform.Find("Stone").Find("Text").GetComponent<TextMeshProUGUI>();
        resourceTextDic[Assets.Instance.resourceTypeSO_Refs.iron] = transform.Find("Iron").Find("Text").GetComponent<TextMeshProUGUI>();

        ResourceManager.Instance.OnResourceAmountChanged += Instance_OnResourceAmountChanged;

        UpdateResourceAmounts();
    }

    private void Instance_OnResourceAmountChanged(object sender, System.EventArgs e) {
        UpdateResourceAmounts();
    }

    private void UpdateResourceAmounts() {
        foreach (ResourceTypeSO resourceTypeSO in Assets.Instance.resourceTypeArray) {
            resourceTextDic[resourceTypeSO].text = ResourceManager.Instance.GetResourceAmount(resourceTypeSO).ToString();
        }
    }


}
