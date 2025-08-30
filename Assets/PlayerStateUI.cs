using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateUI : MonoBehaviour
{
    [SerializeField] GameObject Money;
    [SerializeField] GameObject Dices;
    [SerializeField] GameObject Chips;

    [SerializeField] DicesUI dicesUI;
    public void ShowUI()
    {
        Money.SetActive( true );
        Dices.SetActive( true );
        Chips.SetActive( true );
    }
    public void HideUI()
    {
        Money.SetActive( false );
        Dices.SetActive( false );
        Chips.SetActive( false );
    }

    public void ShowDices()
    {
        dicesUI.ToggleCurrentDicesCollection();
    }

}
