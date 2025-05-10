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
    public GameObject stateObjects;
    public UnityEvent OnCloseScenario, OnOpenScenario;

    public delegate void GameFlowEvent();
    public event GameFlowEvent OnClose, OnOpen;
    public void CloseScenario()
    {
        OnCloseScenario?.Invoke();
        OnClose?.Invoke();
        stateObjects.SetActive(false);
    }

    public void OpenScenario()
    {
        OnOpenScenario?.Invoke();
        OnOpen?.Invoke();
        stateObjects.SetActive(true);
    }
}

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] GameFlowState currentGameFlowState = GameFlowState.None;
    public FlowState[] Scenarios;
    Dictionary<GameFlowState, FlowState> states = new Dictionary<GameFlowState, FlowState>();

    LevelManager levelManager;

    public FlowState GetFlowState( GameFlowState gameFlowState )
    {
        return states[gameFlowState];
    }

    private void OnEnable()
    {
        levelManager = FindObjectOfType<LevelManager>();
        states.Add( GameFlowState.None, Scenarios[ 0 ] );
        states.Add( GameFlowState.Intro, Scenarios[ 0 ] );
        states.Add( GameFlowState.MainMenu, Scenarios[ 1 ]  );
        states.Add( GameFlowState.RunSettings, Scenarios[ 2 ] );
        states.Add( GameFlowState.StageSelect, Scenarios[ 3 ] );
        states.Add( GameFlowState.Play, Scenarios[ 4 ] );
        states.Add( GameFlowState.Shop, Scenarios[ 5 ] );
        states.Add( GameFlowState.PostPlay, Scenarios[ 6 ] );
        states.Add( GameFlowState.GameOver, Scenarios[ 7 ] );
        CloseAllScenarios();
    }

    void CloseAllScenarios()
    {
        for ( int i = 0 ; i < Scenarios.Length ; i++ )
        {
            Scenarios[i].CloseScenario();
        }
    }


    private void Start()
    {
        GoTo( GameFlowState.Intro );
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
