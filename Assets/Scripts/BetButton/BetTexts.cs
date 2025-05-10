using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BetTexts : MonoBehaviour, ITexts
{

    string description;
    string title;

    [SerializeField] BetButton buttonController;
    void OnEnable()
    {
        buttonController.OnDataUpdated += UpdateStrings;
    }

    void UpdateStrings()
    {
        title = buttonController.GetTitle();
        description = buttonController.GetDescription();
    }

    public string GetTitle()
    {
        title = buttonController.GetTitle();
        return title;
    }

    public string GetDescription()
    {
        BetButtonData buttonData = buttonController.GetCurrentBetData();
        description = string.Format( buttonController.GetDescription(), buttonData.CurrentChips, buttonData.CurrentMultiplier, buttonData.CurrentMoney );
        return description;
    }
}
