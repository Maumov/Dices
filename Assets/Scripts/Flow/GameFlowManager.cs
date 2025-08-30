using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameFlowState
{
    None,
    Intro,
    MainMenu,
    RunSettings,
    StageSelect,
    Play,
    PostPlay,
    Shop,
    GameOver
}
[System.Serializable]
public class FlowState
{
    public GameFlowState state;
    public UnityEvent OnCloseScenario, OnOpenScenario;

    public void CloseScenario()
    {
        OnCloseScenario?.Invoke();
    }

    public void OpenScenario()
    {
        OnOpenScenario?.Invoke();
    }
}

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] GameFlowState currentGameFlowState = GameFlowState.None;
    public FlowState[] Scenarios;
    Dictionary<GameFlowState, FlowState> states = new Dictionary<GameFlowState, FlowState>();

    [SerializeField] LevelManager levelManager;

    bool init = false;
    public FlowState GetFlowState( GameFlowState gameFlowState )
    {
        if (!init)
        {
            Init();
        }
        return states[gameFlowState];
    }

    void Init()
    {
        for ( int i = 0 ; i < Scenarios.Length ; i++ )
        {
            states.Add( Scenarios[ i ].state, Scenarios[ i ] );
        }
        init = true;
    }

    public void GoTo( GameFlowState nextFlowState )
    {
        if ( nextFlowState == currentGameFlowState )
        {
            return;
        }
        StartCoroutine( StateChanged( nextFlowState ) );
        
    }

    IEnumerator StateChanged( GameFlowState nextFlowState )
    {
        states[currentGameFlowState].CloseScenario();
        StateChangedEvaluator( ref nextFlowState );
        currentGameFlowState = nextFlowState;
        states[ currentGameFlowState ].OpenScenario();
        yield return null;
    }

    void StateChangedEvaluator( ref GameFlowState nextState )
    {
        switch ( nextState )
        {
            case GameFlowState.None:
                break;
            case GameFlowState.MainMenu:
                break;
            case GameFlowState.RunSettings:
                break;
            case GameFlowState.StageSelect:
                break;
            case GameFlowState.Play:
                break;
            case GameFlowState.PostPlay:
                if ( levelManager.playerWon() )
                {
                    nextState = GameFlowState.Shop;
                }
                else
                {
                    nextState = GameFlowState.GameOver;
                }
                break;
        }
    }


    public GameFlowState GetCurrentFlowState()
    {
        return currentGameFlowState;
    }

    public bool IsInState( GameFlowState state )
    {
        return currentGameFlowState == state;
    }
}
