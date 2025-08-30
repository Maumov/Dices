using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public enum StageFlowState
{
    None,
    Play,
    Rolling,
    Calculating,
    End
}

public class RoundFlowController : MonoBehaviour
{
    public StageFlowState currentState;
    [SerializeField] GameFlowInteractions gameFlowInteractions;
    [SerializeField] PlayerDices dicesManager;
    [SerializeField] PlayerState playerState;
    [SerializeField] CalculationManager calculationManager;
    [SerializeField] LevelManager levelManager;
    [SerializeField] PlayerState playerStateManager;

    public delegate void RoundFlowEvent();
    public event RoundFlowEvent OnStageStart, OnDicesRolled, OnDicesReseted, OnStageEnd;
    public event RoundFlowEvent OnDicesFinishedRolling;

    private void OnEnable()
    {
        
    }

    public bool CanInteractWithChips()
    {
        return currentState == StageFlowState.Play;
    }
    public bool CanInteractWithDices()
    {
        return currentState == StageFlowState.Play;
    }

    public void StartStage()
    {
        OnStageStart?.Invoke();
        currentState = StageFlowState.Play;
    }

    public void DicesRolled()
    {
        currentState = StageFlowState.Rolling;
        OnDicesRolled?.Invoke();
        StartCoroutine( ManageRoll() );
    }

    IEnumerator ManageRoll()
    {
        yield return new WaitForSeconds( 1f );
        while ( !dicesManager.CheckAllDicesFinishedRolling() )
        {
            yield return new WaitForSeconds( 0.5f );
        }
        OnDicesFinishedRolling?.Invoke();
        StartRollCalculation();
    }

    void StartRollCalculation()
    {
        StartCoroutine( AfterRollFlow() );
    }

    IEnumerator AfterRollFlow()
    {
        currentState = StageFlowState.Calculating;

        yield return calculationManager.StartCalculation();

        yield return calculationManager.FinishCalculation();

        /*
         * Return everything to its place...
         * 
        */

        /*
         * Finish calculation and check for winnings
         * 
         */
        calculationManager.ResetTotal();
        yield return levelManager.AddScore( calculationManager.CalculationData.currentTotal, 1f );

        //yield return levelManager.ResolvePostCalculation();
        /*
         * Here we should show the round's statistics?
         * 
         * 
         */



        /*
         * Then we show the rest of the flow of winning or losing
         * */
        if ( levelManager.playerWon() )
        {
            yield return levelManager.Won();

            FlushEarningsToPlayer();

            currentState = StageFlowState.End;// end stage...

            gameFlowInteractions.GoToPostPlay();
        }
        else
        {
            if ( !playerState.HasRollsRemaining() )
            {
                yield return levelManager.Lose();

                currentState = StageFlowState.End;

                gameFlowInteractions.GoToPostPlay();
            }
            else
            {

                GoNextRoll();
            }
        }

    }

    void GoNextRoll()
    {
        currentState = StageFlowState.Play;
        OnDicesReseted?.Invoke();
    }

    public void FlushEarningsToPlayer()
    {
        playerStateManager.AddCoins( levelManager.GetFinalEarnings() );
        levelManager.FlushEaningsToPlayer();
    }

}
