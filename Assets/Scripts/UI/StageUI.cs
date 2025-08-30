using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] Button RollButton;

    [SerializeField] GamePlayDices dicesManager;
    [SerializeField] BetButtonsManager betButtonsManager;

    [SerializeField] RoundFlowController stageFlowController;

    private void OnEnable()
    {
        RollButton.interactable = false;

        stageFlowController.OnDicesReseted += ResetResultValues;

        dicesManager.OnDiceSelected += UpdateRollButtonInteraction;
        dicesManager.OnDiceDeSelected += UpdateRollButtonInteraction;

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
