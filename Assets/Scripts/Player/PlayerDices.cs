using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDices : MonoBehaviour
{
    [SerializeField] List<GameObject> currentDiceSet;
    //[SerializeField] List<DiceController> dices;
    [SerializeField] GamePlayDices gamePlayDices;


    [SerializeField] StageUI stageUI;
    [SerializeField] CalculationManager calculationManager;
   
    [SerializeField] PlayerState playerState;
    [SerializeField] ItemContainer playerDiceContainer;

    [SerializeField] RoundFlowController stageFlowController;

    [SerializeField] GameManager gameManager;

    public List<GameObject> GetCurrentDiceSet()
    {
        return currentDiceSet;
    }

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
        gamePlayDices.NewDiceAdded( newDice );
    }

    

    public int DiceCount()
    {
        return gamePlayDices.DiceCount();
    }

    private void OnDisable()
    {
      
    }

    public void SetupPlayerAtRunStart()
    {
        
    }

    public void SetupPlayerAtStageStart()
    {
        gamePlayDices.SpawnDices( currentDiceSet );
    }

    public bool CanSelectDice()
    {
        return gamePlayDices.CanSelectDice();
    }

    public bool HaveDiceSelected()
    {
        return gamePlayDices.HaveDiceSelected();
    }

    void RollSelectedDices()
    {
        stageUI.DicesRolled();
        gamePlayDices.RollSelectedDices();
    }

    void ResetDices()
    {
        gamePlayDices.ResetDices();
    }

    public bool CheckAllDicesFinishedRolling()
    {
        return gamePlayDices.CheckAllDicesFinishedRolling();
    }

    int dicesRolled = 0;
    int sumOfDices = 0;
    List<int> diceValues = new List<int>();
    public void CalculateDicesRolled()
    {
        gamePlayDices.CalculateDicesRolled( ref dicesRolled, ref sumOfDices, ref diceValues );
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
        gamePlayDices.SetupDicesForCalculation( statistics );
    }

}
