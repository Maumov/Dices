using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunSettingsUI : MonoBehaviour
{
    [SerializeField] RunSettingsManager runSettingsManager;

    [SerializeField] TextMeshProUGUI diceSetName;
    [SerializeField] TextMeshProUGUI diceSetDescription;
    
    [SerializeField] TextMeshProUGUI chipSetName;
    [SerializeField] TextMeshProUGUI chipSetDescription;

    [SerializeField] TextMeshProUGUI startMoney;
    [SerializeField] TextMeshProUGUI rolls;

    [SerializeField] TextMeshProUGUI maxDices;
    [SerializeField] TextMeshProUGUI maxChips;
    private void OnEnable()
    {
        runSettingsManager.OnRunSettingsChange += UpdateUI;
    }

    private void OnDisable()
    {
        runSettingsManager.OnRunSettingsChange -= UpdateUI;
    }


    private void Start()
    {
        UpdateUI();
    }


    void UpdateUI() {

        PlayerStart ps = runSettingsManager.GetPlayerStart();
        diceSetName.text = ps.DiceSetName;
        diceSetDescription.text = ps.DiceSetDescription;

        //chipSetName.text = ps.chipsSet.ChipSetName;
        //chipSetDescription.text = ps.chipsSet.ChipSetDescription;

        startMoney.text = "Money: $"    + ps.startMoney.ToString();
        rolls.text =      "Rolls: "     + ps.rolls.ToString();
        maxDices.text =   "Max Dices: " + ps.maxDices.ToString();
        maxChips.text =   "Max Chips: " + ps.maxChips.ToString();
    }

    public void Next()
    {
        runSettingsManager.Next();
    }
    public void Prev()
    {
        runSettingsManager.Previous();
    }
}
