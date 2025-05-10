using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDices : MonoBehaviour
{
    [SerializeField] List<GameObject> currentDiceSet;
    [SerializeField] List<DiceController> dices;

    [SerializeField] StageUI stageUI;
    [SerializeField] CalculationManager calculationManager;
   
    [SerializeField] PlayerState playerState;
    [SerializeField] ItemContainer playerDiceContainer;

    [SerializeField] GameFlowManager gameFlowManager;
    [SerializeField] StageFlowController stageFlowController;

    [SerializeField] GameManager gameManager;
    
    public delegate void DiceDelegate();
    public event DiceDelegate OnDiceSelected, OnDiceDeSelected;

    public void SetCurrentDices( List<GameObject> startingDices )
    {
        currentDiceSet = new List<GameObject>();
        currentDiceSet.AddRange( startingDices );
    }
    
    public void AddNewDice( GameObject newDice )
    {
        currentDiceSet.Add( newDice );
        NewDiceAdded( newDice );
    }

    void NewDiceAdded( GameObject newDice )
    {
        if ( !gameFlowManager.IsInState( GameFlowState.Play ) )
        {
            return;
        }
        GameObject go = Instantiate( newDice, Vector3.zero, Quaternion.identity );
        DiceController d = go.GetComponent<DiceController>();
        d.SetupItem();
        AddDice( d );
        playerDiceContainer.NewItemAdded( go );
    }

    void AddDice( DiceController newDice )
    {
        dices.Add( newDice );
        newDice.OnSelected += DiceSelected;
        newDice.OnUnselected += DiceUnselected;
    }

    public int DiceCount()
    {
        return dices.Count;
    }

    private void OnEnable()
    {
        stageFlowController.OnStageStart += SetupPlayerAtStageStart;
        stageFlowController.OnDicesRolled += RollSelectedDices;
        stageFlowController.OnDicesReseted += ResetDices;

        foreach ( DiceController d in dices )
        {
            d.OnSelected += DiceSelected;
            d.OnUnselected += DiceUnselected;
        }
    }

    private void Start()
    {
        FlowState runSettingsState = gameFlowManager.GetFlowState( GameFlowState.RunSettings );
        runSettingsState.OnClose += SetupPlayerAtRunStart;
    }

    private void OnDisable()
    {
        FlowState runSettingsState = gameFlowManager.GetFlowState( GameFlowState.RunSettings );
        runSettingsState.OnClose -= SetupPlayerAtRunStart;
    }

    public void SetupPlayerAtRunStart()
    {
        
    }

    public void SetupPlayerAtStageStart()
    {
        SpawnDices();
    }

    [ContextMenu( "Respawn Dices" )]
    void RespawnDices()
    {
        SpawnDices();
    }

    void CleanCurrentDices()
    {
        for ( int i = 0 ; i< dices.Count ; i++ )
        {
            dices[i].OnSelected -= DiceSelected;
            dices[i].OnUnselected -= DiceUnselected;
        }
        playerDiceContainer.DestroyItems();
        dices.Clear();
    }

    void SpawnDices()
    {
        CleanCurrentDices();
        StartCoroutine( SetDicesToPlayer() );
    }
    [SerializeField] float spawnTime = 0.5f;

    IEnumerator SetDicesToPlayer()
    {
        dices = new List<DiceController>();
        int dicesToSpawn = currentDiceSet.Count;
        for ( int i = 0 ; i < dicesToSpawn ; i++ )
        {
            NewDiceAdded( currentDiceSet[ i ] );
            yield return new WaitForSeconds( spawnTime );
        }
        yield return null;
    }

    void DiceSelected()
    {
        OnDiceSelected?.Invoke();
    }

    public bool CanSelectDice()
    {
        
        if ( !stageFlowController.CanInteractWithDices() )
        {
            return false;
        }
        if ( AmountOfDiceSelected() >= playerState.GetDicesPerRoll() )
        {
            return false;
        }
        return  true;
    }

    public int AmountOfDiceSelected()
    {
        return dices.Where( d => d.GetState() == DiceState.HandSelected ).Count();
    }

    public bool HaveDiceSelected()
    {
        return AmountOfDiceSelected() > 0;
    }

    void DiceUnselected()
    {
        OnDiceDeSelected?.Invoke();
    }

    void RollSelectedDices()
    {
        stageUI.DicesRolled();
        foreach ( DiceController d in dices )
        {
            d.SendRolling();
        }
    }

    void ResetDices()
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

    int dicesRolled = 0;
    int sumOfDices = 0;
    List<int> diceValues = new List<int>();
    public void CalculateDicesRolled()
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

    public int GetDicesRolled()
    {
        return dicesRolled;
    }
    public int GetSumOfDices()
    {
        return sumOfDices;
    }
    public List<int> GetDicesValues()
    {
        return diceValues;
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

    public int GetDiceValue( int diceIndex )
    {
        return dices[ diceIndex ].GetValue();
    }

    public BETS[] GetDiceFlags()
    {
        int index = calculationManager.CalculationData.indexOfCurrentDice;
        return dices[ index ].GetFlags();
    }
}
