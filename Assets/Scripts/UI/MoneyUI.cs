using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI CurrentMoney;
    [SerializeField] TMPro.TextMeshProUGUI MoneyBet;

    [SerializeField] PlayerState playerStateManager;
    private void OnEnable()
    {
        playerStateManager = FindObjectOfType<PlayerState>();
        playerStateManager.OnMoneyUpdated += UpdateUI;
    }

    void UpdateUI()
    {
        CurrentMoney.text = playerStateManager.GetCoins().ToString();
        MoneyBet.text = playerStateManager.GetCoinsOnBet().ToString();
    }
}
