using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonLabels : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI label;
    [SerializeField] TMPro.TextMeshProUGUI labelBet;
    [SerializeField] TMPro.TextMeshProUGUI labelPercentage;
    [SerializeField] Color titleColor;
    [SerializeField] Color betColor;
    [SerializeField] Color percentageColor;
    [SerializeField] BetButton betButton;

    private void OnEnable()
    {
        betButton.OnDataUpdated += UpdateLabels;
    }

    private void Start()
    {
        UpdateLabels();
    }

    void UpdateLabels()
    {
        BetButtonData betData = betButton.GetCurrentBetData();
        label.text = string.Format("<color={0}>{1}" , IEffect.ToHex( titleColor ), betButton.GetTitle());
        labelBet.text = string.Format( "<color={0}>{1}", IEffect.ToHex( betColor), betData.CurrentChips );
        labelPercentage.text = string.Format( "<color={0}>{1}", IEffect.ToHex( percentageColor ), betData.CurrentMultiplier );
    }
}
