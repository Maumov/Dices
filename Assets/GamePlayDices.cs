using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class GamePlayDices : MonoBehaviour
{
    [SerializeField] CalculationManager calculationManager;
    [SerializeField] PlayerState playerState;

    [SerializeField] List<DiceController> dices;
    [SerializeField] ItemContainer dicesContainer;

    public delegate void DiceDelegate();
    public event DiceDelegate OnDiceSelected, OnDiceDeSelected;
    /*
    [ContextMenu( "Respawn Dices" )]
    void RespawnDices()
    {
        SpawnDices();
    }
    */
    public int DiceCount()
    {
        return dices.Count;
    }
    void DiceUnselected()
    {
        OnDiceDeSelected?.Invoke();
    }
    void DiceSelected()
    {
        OnDiceSelected?.Invoke();
    }

    public void SpawnDices( List<GameObject> diceSet)
    {
        CleanCurrentDices();
        StartCoroutine( SetDicesToPlayer( diceSet ) );
    }

    void CleanCurrentDices()
    {
        for ( int i = 0 ; i < dices.Count ; i++ )
        {
            dices[ i ].OnSelected -= DiceSelected;
            dices[ i ].OnUnselected -= DiceUnselected;
        }
        dicesContainer.DestroyItems();
        dices.Clear();
    }
    
    [SerializeField] float spawnTime = 0.5f;

    IEnumerator SetDicesToPlayer( List<GameObject> diceSet )
    {
        dices = new List<DiceController>();
        int dicesToSpawn = diceSet.Count;
        for ( int i = 0 ; i < dicesToSpawn ; i++ )
        {
            NewDiceAdded( diceSet[ i ] );
            yield return new WaitForSeconds( spawnTime );
        }
        yield return null;
    }

    public void NewDiceAdded( GameObject newDice )
    {
        GameObject go = Instantiate( newDice, Vector3.zero, Quaternion.identity );
        DiceController d = go.GetComponent<DiceController>();
        d.SetupItem();
        AddDice( d );
        dicesContainer.NewItemAdded( go );
    }
    void AddDice( DiceController newDice )
    {
        dices.Add( newDice );
        newDice.OnSelected += DiceSelected;
        newDice.OnUnselected += DiceUnselected;
    }

    public void SpawnAllDices()
    {
        
    }

    public void AddDice()
    {

    }

    void SpawnDice()
    {

    }
    public bool CanSelectDice()
    {
        return AmountOfDiceSelected() < playerState.GetDicesPerRoll();   
    }
    public int AmountOfDiceSelected()
    {
        return dices.Where( d => d.GetState() == DiceState.HandSelected ).Count();
    }

    public bool HaveDiceSelected()
    {
        return AmountOfDiceSelected() > 0;
    }

    public void RollSelectedDices()
    {
        foreach ( DiceController d in dices )
        {
            d.SendRolling();
        }
    }

    public void ResetDices()
    {
        foreach ( DiceController d in dices )
        {
            d.BackToHand();
        }
    }

    public bool CheckAllDicesFinishedRolling()
    {
        foreach ( DiceController d in dices )
        {
            if ( !d.FinishedRolling() )
            {
                return false;
            }
        }
        return true;
    }
    public void CalculateDicesRolled( ref int dicesRolled, ref int sumOfDices, ref List<int> diceValues )
    {
        dicesRolled = 0;
        sumOfDices = 0;
        diceValues = new List<int>();
        for ( int i = 0 ; i < dices.Count ; i++ )
        {
            if ( dices[ i ].GetState() == DiceState.Rolled )
            {
                dicesRolled++;
                diceValues.Add( dices[ i ].GetValue() );
                sumOfDices += dices[ i ].GetValue();
            }
        }
    }
    public void SetupDicesForCalculation( Dictionary<BETS, bool> statistics )
    {
        SetFlagsToDices( statistics );
        SetCountedToDices( statistics );
    }
    void SetCountedToDices( Dictionary<BETS, bool> results )
    {
        for ( int i = 0 ; i < dices.Count ; i++ )
        {
            dices[ i ].SetCountedOnABet( results );
        }
    }

    void SetFlagsToDices( Dictionary<BETS, bool> results )
    {
        for ( int i = 0 ; i < dices.Count ; i++ )
        {
            DiceController dice = dices[ i ];
            int diceValue = dice.GetValue();
            dice.SetFlags();
            if ( diceValue == 0 )
            {
                continue;
            }

            if ( results[ ( ( ( BETS.Contains1 ) + diceValue - 1 ) ) ] )
            //if ( results[ ( ( BETS ) ( ( BETS.Contains1 ) + diceValue - 1 ) ).ToString() ] )
            {
                dice.AddFlag( BETS.Contains1 + diceValue - 1 );
            }
            if ( results[ ( ( ( BETS.Containspair1 ) + diceValue - 1 ) ) ] )
            {
                dice.AddFlag( BETS.Containspair1 + diceValue - 1 );
                dice.AddFlag( BETS.ContainsPair );
                if ( results[ BETS.ContainsDoublePair ] )
                {
                    dice.AddFlag( BETS.ContainsDoublePair );
                }
                if ( results[ BETS.IsFullHouse ] )
                {
                    dice.AddFlag( BETS.IsFullHouse );
                }
            }
            if ( results[ ( ( ( BETS.Containsthrice1 ) + diceValue - 1 ) ) ] )
            {
                dice.AddFlag( BETS.Containsthrice1 + diceValue - 1 );
                dice.AddFlag( BETS.ContainsThrice );
            }
            if ( results[ ( ( ( BETS.ContainsQuad1 ) + diceValue - 1 ) ) ] )
            {
                dice.AddFlag( BETS.ContainsQuad1 + diceValue - 1 );
                dice.AddFlag( BETS.ContainsQuad );
                dice.AddFlag( BETS.IsQuad );
            }
            if ( results[ ( ( ( BETS.ContainsPenta1 ) + diceValue - 1 ) ) ] )
            {
                dice.AddFlag( BETS.ContainsPenta1 + diceValue - 1 );
                dice.AddFlag( BETS.ContainsPenta );
                dice.AddFlag( BETS.IsPenta );
            }

        }
    }
    public bool DiceCounted()
    {
        int index = calculationManager.CalculationData.indexOfCurrentDice;
        return dices[ index ].Counted();
    }

    public IEnumerator ProcessDice()
    {
        int index = calculationManager.CalculationData.indexOfCurrentDice;
        yield return dices[ index ].ProcessDiceEffect();
    }
}
