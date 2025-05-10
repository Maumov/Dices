using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerState : MonoBehaviour
{
    [SerializeField] PlayerStart playerStart;
    [SerializeField] int startMoney;
    [SerializeField] int rolls = 3;
    [SerializeField] int maxDices;
    [SerializeField] int maxChips;

    [SerializeField] int selectedPlayerStart = 0;

    [SerializeField] int currentMoney;
    [SerializeField] int coinsOnBets;
    [SerializeField] int rollsRemaining;
    [SerializeField] int dicesPerRoll = 5;


    [SerializeField] GameFlowManager gameFlowController;
    [SerializeField] StageFlowController stageFlowController;
    [SerializeField] LevelUI levelUI;
    [SerializeField] GameManager gameManager;
    [SerializeField] RunSettingsManager runSettingsManager;
    [SerializeField] PlayerDices playerDices;
    [SerializeField] PlayerChips playerChips;
    public int SelectedStartSetup
    {
        get => selectedPlayerStart;
        set {
            selectedPlayerStart = value;
            selectedPlayerStart = selectedPlayerStart < 0 ? gameManager.PlayerStarts.Count-1 : value;
            selectedPlayerStart %= gameManager.PlayerStarts.Count;
            playerStart = gameManager.PlayerStarts[selectedPlayerStart].playerStart;
        }
    }

    public PlayerStart GetPlayerStart()
    {
        return playerStart;
    }

    List<GameObject> GetDiceSet()
    {
        return playerStart.diceSet.dices;
    }
    List<GameObject> GetChipSet()
    {
        return playerStart.chipsSet.Chips;
    }
    public List<BetButtonDataScriptableObject> GetBetButtonData()
    {
        return playerStart.betButtonsSet.AllBetButtons;
    }


    public delegate void MoneyDelegate();
    public event MoneyDelegate OnMoneyUpdated;


    private void OnEnable()
    {
        
        stageFlowController.OnStageStart += StageStarted;
        stageFlowController.OnStageEnd += StageEnded;
        stageFlowController.OnDicesRolled += DicesRolled;

        runSettingsManager.OnRunSettingsChange += SetRunSettingsForPlayer;
    }

    private void Start()
    {
        FlowState runSettingsState = gameFlowController.GetFlowState( GameFlowState.RunSettings );
        runSettingsState.OnClose += SetupPlayerStart;
        SelectedStartSetup = 0;
    }

    private void OnDisable()
    {
        FlowState runSettingsState = gameFlowController.GetFlowState( GameFlowState.RunSettings );
        runSettingsState.OnClose -= SetupPlayerStart;
    }

    void SetRunSettingsForPlayer()
    {
        playerDices.SetCurrentDices( GetDiceSet() );
        playerChips.SetCurrentChips( GetChipSet() );
        startMoney = playerStart.startMoney;
        rolls = playerStart.rolls;
        maxDices = playerStart.maxDices;
        maxChips = playerStart.maxChips;
    }

    void StageStarted()
    {
        rollsRemaining = rolls;
        levelUI.SetRolls( GetRollsRemaining(), GetRolls() );
    }
    void StageEnded()
    {
    }

    void DicesRolled()
    {
        rollsRemaining--;
        levelUI.SetRolls( GetRollsRemaining(), GetRolls() );
    }

    public bool HasRollsRemaining()
    {
        return rollsRemaining > 0;
    }

    public int GetRolls()
    {
        return rolls;
    }

    public int GetDicesPerRoll()
    {
        return dicesPerRoll;
    }

    public int GetRollsRemaining()
    {
        return rollsRemaining;
    }

    void SetupPlayerStart()
    {
        currentMoney = startMoney;
        
    }

    public void SetMoney( int amount)
    {
        currentMoney = amount;
        OnMoneyUpdated?.Invoke();
    }

    public void AddCoins( int amount )
    {
        currentMoney += amount;
        OnMoneyUpdated?.Invoke();
    }

    public void RemoveCoins( int amount )
    {
        currentMoney -= amount;
        OnMoneyUpdated?.Invoke();
    }

    public bool HasAmount( int amount )
    {
        return currentMoney >= amount;
    }
    public int GetCoins()
    {
        return currentMoney;
    }

    public int GetCoinsOnBet()
    {
        return coinsOnBets;
    }

    public int GetCoinValue()
    {
        return 0;
    }
}
