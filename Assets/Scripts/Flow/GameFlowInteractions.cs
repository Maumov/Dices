using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowInteractions : MonoBehaviour
{
    [SerializeField] MainMenuController mainMenuController;
    
    public void GoToMainMenu()
    {
        mainMenuController.Show();
    }
    public void GoToRunSettings()
    {
        //gameFlowController.GoTo( GameFlowState.RunSettings );
    }
    public void GoToStageSelect()
    {
        //gameFlowController.GoTo( GameFlowState.StageSelect );
    }
    public void GoToPlay()
    {
        //gameFlowController.GoTo( GameFlowState.Play );
    }
    public void GoToShop()
    {
        //gameFlowController.GoTo( GameFlowState.Shop );
    }
    public void GoToPostPlay()
    {
        //gameFlowController.GoTo( GameFlowState.PostPlay );
    }
    public void GoToNextLevel()
    {
        //gameFlowController.GoTo( GameFlowState.StageSelect );
    }
}
