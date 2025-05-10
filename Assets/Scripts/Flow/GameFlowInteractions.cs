using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowInteractions : MonoBehaviour
{
    GameFlowManager gameFlowController;
    private void OnEnable()
    {
        gameFlowController = GetComponent<GameFlowManager>();
    }
    public void GoToMainMenu()
    {
        gameFlowController.GoTo( GameFlowState.MainMenu );
    }
    public void GoToRunSettings()
    {
        gameFlowController.GoTo( GameFlowState.RunSettings );
    }
    public void GoToStageSelect()
    {
        gameFlowController.GoTo( GameFlowState.StageSelect );
    }
    public void GoToPlay()
    {
        gameFlowController.GoTo( GameFlowState.Play );
    }
    public void GoToShop()
    {
        gameFlowController.GoTo( GameFlowState.Shop );
    }
    public void GoToPostPlay()
    {
        gameFlowController.GoTo( GameFlowState.PostPlay );
    }
    public void GoToNextLevel()
    {
        gameFlowController.GoTo( GameFlowState.StageSelect );
    }
}
