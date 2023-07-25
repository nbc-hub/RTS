using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlaceBuildingsUI : MonoBehaviour
{

    private Dictionary<BuildingTypeSO, GameObject> buildingTypeSelectedDic;

    private void Awake()
    {
        transform.Find("NoneBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            PlaceBuildings.Instance.ClearBuildingTypeSO();
        };
        /*
        transform.Find("StorageBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            PlaceBuildings.Instance.SetBuildingTypeSO(Assets.Instance.buildingTypeSO_Refs.storage);
        };*/
        transform.Find("BarracksBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            PlaceBuildings.Instance.SetBuildingTypeSO(Assets.Instance.buildingTypeSO_Refs.barracks);
        };

         buildingTypeSelectedDic = new Dictionary<BuildingTypeSO, GameObject>();
         buildingTypeSelectedDic[Assets.Instance.buildingTypeSO_Refs.none] = transform.Find("NoneBtn").Find("Selected").gameObject;
      //   buildingTypeSelectedDic[Assets.Instance.buildingTypeSO_Refs.storage] = transform.Find("StorageBtn").Find("Selected").gameObject;
         buildingTypeSelectedDic[Assets.Instance.buildingTypeSO_Refs.barracks] = transform.Find("BarracksBtn").Find("Selected").gameObject;

         AddTooltip(transform.Find("NoneBtn"), "None");
        // AddTooltip(transform.Find("StorageBtn"), "Storage\n" +
        //   ResourceAmount.GetTooltipString(Assets.Instance.buildingTypeSO_Refs.storage.constructionResourceAmountCostList));
         AddTooltip(transform.Find("BarracksBtn"), "Barracks\n" +
            ResourceAmount.GetTooltipString(Assets.Instance.buildingTypeSO_Refs.barracks.constructionResourceAmountCostList));
    }

    private void Start()
    {
        PlaceBuildings.Instance.OnBuildingTypeSOChanged += PlaceBuildings_OnBuildingTypeSOChanged;
        UpdateBuildingSelected();
    }

    private void PlaceBuildings_OnBuildingTypeSOChanged(object sender, System.EventArgs e)
    {
        UpdateBuildingSelected();
    }
    
    private void UpdateBuildingSelected()
    {
        foreach (BuildingTypeSO buildingTypeSO in buildingTypeSelectedDic.Keys)
        {
            buildingTypeSelectedDic[buildingTypeSO].SetActive(false);
        }

        if (PlaceBuildings.Instance.GetBuildingTypeSO() == null)
        {
            buildingTypeSelectedDic[Assets.Instance.buildingTypeSO_Refs.none].SetActive(true);
        }
        else
        {
            buildingTypeSelectedDic[PlaceBuildings.Instance.GetBuildingTypeSO()].SetActive(true);
        }
    }
    
    private void AddTooltip(Transform transform, string tooltipString) {
        transform.GetComponent<Button_UI>().MouseOverOnceTooltipFunc = () => {
            TooltipCanvas.ShowTooltip_Static(tooltipString);
        };
        transform.GetComponent<Button_UI>().MouseOutOnceTooltipFunc = () => {
            TooltipCanvas.HideTooltip_Static();
        };
    }
    
}
