using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] Button RollButton;

    PlayerDices dicesManager;
    BetButtonsManager betButtonsManager;

    StageFlowController stageFlowController;

    private void OnEnable()
    {
        RollButton.interactable = false;

        stageFlowController = FindObjectOfType<StageFlowController>();
        stageFlowController.OnDicesReseted += ResetResultValues;

        dicesManager = FindObjectOfType<PlayerDices>();
        dicesManager.OnDiceSelected += UpdateRollButtonInteraction;
        dicesManager.OnDiceDeSelected += UpdateRollButtonInteraction;

        betButtonsManager = FindObjectOfType<BetButtonsManager>();
        betButtonsManager.OnBetAdded += UpdateRollButtonInteraction;
        betButtonsManager.OnBetRemoved += UpdateRollButtonInteraction;
    }
    
    void UpdateRollButtonInteraction()
    {
        bool interactable = true;
        if ( !dicesManager.HaveDiceSelected() )
        {
            interactable = false;
        }
        if ( !betButtonsManager.ButtonHasChips() )
        {
            interactable = false;
        }
        if ( stageFlowController.currentState != StageFlowState.Play )
        {
            interactable = false;
        }
        RollButton.interactable = interactable;
    }

    public void DicesRolled()
    {
        RollButton.interactable = false;
    }
    void ResetResultValues()
    {
        //ResetResultValues( 0 );
    }
}
