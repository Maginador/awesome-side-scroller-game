using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour
{

    
    [SerializeField] private string fieldName;
    
    [SerializeField] private GameObject upgradePrefab;
    [SerializeField] private float initialXPosition;
    [SerializeField] private int upgradeLimit;

    [SerializeField] private int currentUpgrade;
    private List<Image> _nodeImageList;
    [SerializeField] private Color upgradedColor;

    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private float priceInitial;
    [SerializeField] private float priceMultiplier;

    private const int XVariation = 50;

    // Start is called before the first frame update
    void Start()
    {
        _nodeImageList = new List<Image>();
        GetUpgradeData();
        BuildUpgradeView();
    }


    public void DoUpgrade()
    {
        if (currentUpgrade == upgradeLimit) return;

        if (!DoBackendValidation()) return;
       
        currentUpgrade++;
        _nodeImageList[currentUpgrade].color = upgradedColor;
        priceText.text = (priceInitial * priceMultiplier * currentUpgrade).ToString();

    }

    private bool DoBackendValidation()
    {
        //TODO Validate Backend 
        return true;
    }
    private void BuildUpgradeView()
    {
        for (var i = 0; i <= upgradeLimit; i++)
        {
            var node = Instantiate(upgradePrefab, transform, true);
            var rectTransform = node.GetComponent<RectTransform>();
            rectTransform.anchoredPosition =
                new Vector2(initialXPosition + XVariation * i, 0);
            var image = node.GetComponentInChildren<Image>();
            _nodeImageList.Add(image);
            
            if (i <= currentUpgrade)
            {
                image.color = upgradedColor;
            }
        }

        priceText.text = (priceInitial * priceMultiplier * (currentUpgrade + 1)).ToString();
    }
    private void GetUpgradeData()
    {
        //TODO get upgrade data from backend (limit (global) and current (current user)/ initial price and price multiplier)
    }
}
