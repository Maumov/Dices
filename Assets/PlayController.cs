using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    [SerializeField] GameObject play;
    [SerializeField] IntroOutro introOutro;

    [SerializeField] Animator betButtonsAnimator, stageSelectAnimator, shopAnimator;

    [SerializeField] GameObject rollButton;

    [SerializeField] BetButtonsManager betButtonsManager;
    [SerializeField] PlayerChips playerChips;
    [SerializeField] PlayerDices playerDices;

    [SerializeField] RoundFlowController roundFlowController;

    readonly int showHash = Animator.StringToHash( "Show" );
    readonly int hideHash = Animator.StringToHash( "Hide" );
    public void Show()
    {
        StartCoroutine( SetupPlay() );
    }

    IEnumerator SetupPlay()
    {
        play.SetActive( true );
        introOutro.Intro( Init );
        yield return null;
    }

    void Init()
    {
        ShowStageSelect();

    }

    public void PlayStage()
    {
        StartCoroutine( StageSelectToPlayRound() );
    }

    IEnumerator StageSelectToPlayRound()
    {

        HideStageSelect();
        yield return new WaitForSeconds( 1f );
        roundFlowController.StartStage();
        
        betButtonsManager.SetupValuesToButtons();
        playerChips.SetupPlayerAtStageStart();
        playerDices.SetupPlayerAtStageStart();
        
        ShowBetButtons();
        yield return new WaitForSeconds( 1f );
        rollButton.SetActive( true );

    }

    void ShowBetButtons()
    {
        betButtonsAnimator.SetTrigger( showHash );
    }

    void ShowStageSelect()
    {
        stageSelectAnimator.SetTrigger( showHash );
    }

    void HideStageSelect()
    {
        stageSelectAnimator.SetTrigger( hideHash );
    }

    void ShowShop()
    {
        shopAnimator.SetTrigger( showHash );
    }
}
