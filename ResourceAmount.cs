using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

[System.Serializable]
public class ResourceAmount {

    public ResourceTypeSO resourceTypeSO;
    public int amount;


    public static string GetTooltipString(List<ResourceAmount> resourceAmountList) {
        string tooltipString = "";

        foreach (ResourceAmount resourceAmount in resourceAmountList) {
            tooltipString += "<color=#" + UtilsClass.GetStringFromColor(resourceAmount.resourceTypeSO.color) + ">" + 
                resourceAmount.resourceTypeSO.shortString + resourceAmount.amount + "</color> ";
        }

        return tooltipString;
    }

}
