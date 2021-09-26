﻿using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class TopMenuController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI softCurrencyText, hardCurrencyText;
    // Start is called before the first frame update

    private void OnEnable()
    {
        Game.GameController.Instance.AddCurrencyChangedListener(CurrencyUpdate);
        CurrencyUpdate();
    }

    private void OnDisable()
    {
        Game.GameController.Instance.RemoveCurrencyChangedListener(CurrencyUpdate);

    }

    // Update is called once per frame
    private void CurrencyUpdate()
    {
        softCurrencyText.text = Game.GameController.Instance.GetCurrency("SC").ToString();
        hardCurrencyText.text = Game.GameController.Instance.GetCurrency("HC").ToString();
    }
}