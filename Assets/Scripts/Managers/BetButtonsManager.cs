using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetButtonsManager : MonoBehaviour
{
    [SerializeField] BetButton[] buttons;
    Dictionary<BETS, BetButton> buttonDictionary;
    [SerializeField] PlayerState playerState;
    [SerializeField] CalculationManager calculationManager;
    List<BetButtonDataScriptableObject> betButtonsData;

    bool betButtonsHasChips;

    public delegate void BetButtonsManagerDelegate();
    public event BetButtonsManagerDelegate OnBetAdded, OnBetRemoved;

    private void OnEnable()
    {
        foreach (BetButton button in buttons)
        {
            button.OnChipAdded += ChipAddedToBet;
            button.OnChipRemoved += ChipRemovedFromBet;
        }
    }
    void ChipAddedToBet()
    {
        betButtonsHasChips = true;
        OnBetAdded?.Invoke();
    }

    void ChipRemovedFromBet()
    {
        betButtonsHasChips = false;
        foreach ( BetButton button in buttons )
        {
            if ( button.HasBets() )
            {
                betButtonsHasChips = true;
            }
        }
        OnBetRemoved?.Invoke();
    }

    public bool ButtonHasChips()
    {
        return betButtonsHasChips;
    }

    private void OnDisable()
    {
        foreach ( BetButton button in buttons )
        {
            button.OnChipAdded -= ChipAddedToBet;
            button.OnChipRemoved -= ChipRemovedFromBet;
        }
    }

    void ClearBetButtonsChips()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].ClearChips();
        }
        betButtonsHasChips = false;

    }

    public void SetupValuesToButtons()
    {
        betButtonsData = new List<BetButtonDataScriptableObject>();
        betButtonsData.AddRange( playerState.GetBetButtonData() );

        for ( int i = 0 ; i < buttons.Length ; i++ )
        {
            buttons[ i ].SetupValues( betButtonsData[i] );
        }

        buttonDictionary = new Dictionary<BETS, BetButton> ();
        for ( int i = 0 ; i< buttons.Length ; i++ )
        {
            buttonDictionary.Add( buttons[i].GetKey(), buttons[i] );
        }
    }

    public void ProcessBetButton( BETS key )
    {
        buttonDictionary[ key ].ProcessBetButton();
    }

    public IEnumerator ProcessBetEffect( BETS key )
    {
        yield return buttonDictionary[ key ].ProcessBetEffect();
    }
    public IEnumerator ProcessChipsOnBet( BETS key )
    {
        yield return buttonDictionary[ key ].ProcessChips();
    }
    public bool BetButtonHasChips( BETS key )
    {
        if ( buttonDictionary.ContainsKey( key ) )
        {
            return buttonDictionary[ key ].HasBets();
        }
        return false;
    }

    public bool BetButtonCounted()
    {
        BETS key = calculationManager.CalculationData.currentbetkey;
        return BetButtonHasChips( key );
    }

}
