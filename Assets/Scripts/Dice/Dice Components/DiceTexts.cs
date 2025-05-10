using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceTexts : MonoBehaviour, ITexts
{
    string description;
    string title;
    [SerializeField] DiceController controller;

    void UpdateStrings()
    {
        title = controller.GetTitle();
        description = controller.GetDescription();
    }

    public string GetTitle()
    {
        UpdateStrings();
        //description = string.Format( "Dice Title" );
        return title;
    }

    public string GetDescription()
    {
        UpdateStrings();
        //description = string.Format( "Dice description" );
        return description;
    }
}
