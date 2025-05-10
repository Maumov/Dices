using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationManager : MonoBehaviour
{
    [SerializeField] PlayerDices dicesManager;
    [SerializeField] DebugUI uiManager;
    
    [SerializeField] BetButtonsManager betButtons;
    [SerializeField] Chances chances;
    [SerializeField] CalculationUI calculationUI;
    
    public CalculationData CalculationData;
        
    float seconds = 1f;
    float minSeconds;
    float maxSeconds;
    float evaluator = 0f;
    float evaluatorSpeed = 0.1f;
    public AnimationCurve waitTimeCurve;
    public IEnumerator StartCalculation()
    {
        ResetCalculationSpeed();
        //Start.
        //1. Setup All Data.
        
        //start with setup dices data
        dicesManager.CalculateDicesRolled();
        int dicesum = dicesManager.GetSumOfDices();
        int diceCount = dicesManager.DiceCount();
        List<int> diceValues = dicesManager.GetDicesValues();
        //and posible results
        Dictionary<BETS, bool> statistics = chances.GetHand( diceValues.ToArray() );
        //Show statistics on Debug UI
        uiManager.SetResultValues( dicesum, statistics );
        //setup flags for dices
        dicesManager.SetupDicesForCalculation( statistics );

        //2. Start Calculations with the dices.
        //Start Dices Additions and Propagation...
        //Add dices number to bet, if it counted...
        CalculationData = new CalculationData();
        CalculationData.currentPercentege = 0;
        CalculationData.poppedDiceCount = 0;

        for ( int i = 0 ; i < diceCount ; i++ )
        {
            CalculationData.indexOfCurrentDice = i;
            if ( dicesManager.DiceCounted() )
            {
                yield return ProcessDice();
            }
        }
        foreach ( KeyValuePair<BETS, bool> valuePair in statistics )
        {
            if ( valuePair.Value )
            {
                CalculationData.currentbetkey = valuePair.Key;

                if ( betButtons.BetButtonCounted() )
                {
                    yield return ProcessBet();
                }
            }
        }
        
    }

    IEnumerator ProcessDice()
    {
        yield return dicesManager.ProcessDice();
        yield return WaitForSeconds();
        CalculationData.poppedDiceCount++;
    }

    IEnumerator ProcessBet()
    {
        betButtons.ProcessBetButton( CalculationData.currentbetkey );
        yield return WaitForSeconds();
        yield return betButtons.ProcessBetEffect( CalculationData.currentbetkey );
        yield return betButtons.ProcessChipsOnBet( CalculationData.currentbetkey );
    }

    IEnumerator AfterProcessADice()
    {
        yield return ProcessDicesInHand();
    }

    IEnumerator ProcessDicesInHand()
    {
        int diceCount = dicesManager.DiceCount();
        for ( int i = 0 ; i < diceCount ; i++ )
        {
            CalculationData.indexOfCurrentDice = i;
            if ( !dicesManager.DiceCounted() )
            {
                yield return dicesManager.ProcessDice();
            }
        }
    }

    public IEnumerator FinishCalculation()
    {
        UpdateTotal();

        yield return WaitForSeconds();
    }

    public void ResetTotal()
    {
        calculationUI.ResetTotal();
    }

    public void AddBetValues( int amountBet, int multiplierToAdd )
    {
        CalculationData.currentBetAddition += amountBet;
        CalculationData.currentPercentege += multiplierToAdd;
        //CalculationData.currentBetAddition += amountBet;
        //CalculationUI.instance.SetBets( amountBet );
        calculationUI.SetBlueValue( CalculationData.currentBetAddition );
        calculationUI.SetRedValue( CalculationData.currentPercentege );
    }

    public void AddDiceValue(int value )
    {
        CalculationData.currentBetAddition += value;
        CalculationData.currentSumOfDice += value;
        calculationUI.SetBlueValue( CalculationData.currentBetAddition );
    }

    void UpdateTotal()
    {
        //CalculationData.currentTotal = CalculationData.currentBetAddition + (CalculationData.currentBetAddition * CalculationData.currentPercentege / 100 );
        CalculationData.currentTotal = CalculationData.currentBetAddition * CalculationData.currentPercentege;
        calculationUI.SetTotal( CalculationData.currentTotal );
    }

    void ResetCalculationSpeed()
    {
        minSeconds = waitTimeCurve.keys[ waitTimeCurve.length - 1 ].value;
        maxSeconds = waitTimeCurve[ 0 ].value;
        evaluator = 0f;
        seconds = waitTimeCurve.Evaluate( evaluator );
    }

    void UpdateCalculationSpeed()
    {
        evaluator += evaluatorSpeed;
        seconds = waitTimeCurve.Evaluate( evaluator );
        seconds = Mathf.Clamp( seconds, minSeconds, maxSeconds );
    }

    public IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds( seconds );
        //UpdateCalculationSpeed();
    }
}
public struct CalculationData
{
    //Calculation
    public int currentBetAddition;
    public int currentPercentege;
    public int currentTotal;

    //Dices
    public int indexOfCurrentDice;
    public int currentSumOfDice;

    public int poppedDiceCount;

    //Bet
    public int indexOfCurrentBet;
    public BETS currentbetkey;
}

