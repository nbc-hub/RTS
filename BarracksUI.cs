using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class BarracksUI : MonoBehaviour {

    public static BarracksUI Instance { get; private set; }



    private Barracks barracks;
    private Transform queueContainer;
    private Transform queueTemplate;

    private void Awake() {
        Instance = this;

        transform.Find("CloseBtn").GetComponent<Button_UI>().ClickFunc = () => {
            Hide();
        };

        SetupUnitButtons();

        queueContainer = transform.Find("QueueContainer");
        queueTemplate = queueContainer.Find("Template");
        queueTemplate.gameObject.SetActive(false);

        Hide();
    }

    private void Update() {
        UpdateQueue();
    }

    public void Show(Barracks barracks) {
        this.barracks = barracks;
        gameObject.SetActive(true);

        UpdateQueue();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void SetupUnitButtons() {
        Transform unitButtonContainer = transform.Find("UnitButtonContainer");
        Transform unitButtonTemplate = unitButtonContainer.Find("Template");
        unitButtonTemplate.gameObject.SetActive(false);
        List<UnitTypeSO> barracksUnitTypeList = new List<UnitTypeSO>() { 
            Assets.Instance.unitTypeSO_Refs.villager,
            Assets.Instance.unitTypeSO_Refs.melee,
            Assets.Instance.unitTypeSO_Refs.ranged,
        };

        foreach (UnitTypeSO unitTypeSO in barracksUnitTypeList) {
            Transform unitButtonTransform = Instantiate(unitButtonTemplate, unitButtonContainer);
            unitButtonTransform.gameObject.SetActive(true);

            unitButtonTransform.Find("Text").GetComponent<TextMeshProUGUI>().text = unitTypeSO.name;
            unitButtonTransform.Find("Image").GetComponent<Image>().sprite = unitTypeSO.icon;

            UnitTypeSO buildUnitTypeSO = unitTypeSO;
            unitButtonTransform.GetComponent<Button_UI>().ClickFunc = () => {
                if (barracks == null) return; // No Barracks Selected

                if (ResourceManager.Instance.TrySpendResourceAmount(buildUnitTypeSO.constructionResourceAmountCostList)) {
                    barracks.AddUnitToQueue(buildUnitTypeSO);
                } else {
                   // TooltipCanvas.ShowTooltip_Static("Cannot afford Unit Cost!", 3f);
                }
            };

            
            unitButtonTransform.GetComponent<Button_UI>().MouseOverOnceTooltipFunc = () => { 
                TooltipCanvas.ShowTooltip_Static(unitTypeSO.name + "\n" + ResourceAmount.GetTooltipString(buildUnitTypeSO.constructionResourceAmountCostList)); 
            };
            unitButtonTransform.GetComponent<Button_UI>().MouseOutOnceTooltipFunc = () => { 
                TooltipCanvas.HideTooltip_Static(); 
            };
        }
    }

    private void UpdateQueue() {
        if (barracks == null) return; // No Barracks Selected

        UtilsClass.DestroyChildren(queueContainer, "Template");

        foreach (Barracks.UnitConstruction unitConstruction in barracks.GetUnitConstructionList()) {
            Transform queueTransform = Instantiate(queueTemplate, queueContainer);
            queueTransform.gameObject.SetActive(true);
            //queueTransform.Find("Text").GetComponent<TextMeshProUGUI>().text = unitConstruction.GetUnitTypeSO().name;
            queueTransform.Find("Image").GetComponent<Image>().sprite = unitConstruction.GetUnitTypeSO().icon;
            queueTransform.Find("Mask").Find("Bar").GetComponent<Image>().fillAmount = unitConstruction.GetProgress();
            queueTransform.GetComponent<Button_UI>().ClickFunc = () => { 
                // Cancel Unit Queue
            };
        }
    }

}
