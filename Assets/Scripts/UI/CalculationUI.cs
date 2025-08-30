using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CalculationUI : MonoBehaviour
{
    [SerializeField] int currentBets;
    [SerializeField] int currentPercentage;
    [SerializeField] int currentTotal;
    [SerializeField] TMPro.TextMeshProUGUI currentBetsValueLabel;
    [SerializeField] TMPro.TextMeshProUGUI currentPercentageValueLabel;
    [SerializeField] TMPro.TextMeshProUGUI currentTotalValueLabel;

    Animator betsAnimator;
    Animator percentageAnimator;
    Animator totalAnimator;
    readonly int popNameHash = Animator.StringToHash("Pop");

    [SerializeField] GameFlowManager gameFlowController;
    [SerializeField] RoundFlowController stageFlowController;
    private void Awake()
    {
        betsAnimator = currentBetsValueLabel.GetComponent<Animator>();
        percentageAnimator = currentPercentageValueLabel.GetComponent<Animator>();
        totalAnimator = currentTotalValueLabel.GetComponent<Animator>();
        ResetValues();

    }

    public void ResetValues()
    {
        currentBets = 0;
        currentPercentage = 0;
        currentTotal = 0;
        UpdateAllLabels( false );
    }

    public void SetBlueValue( int value )
    {
        currentBets = value;
        UpdateBets( true );
    }

    public void SetRedValue( int value)
    {
        currentPercentage = value;
        UpdatePercentage( true);
    }

    public void SetTotal( int value )
    {
        currentBets = 0;
        UpdateBets( false );
        currentPercentage = 0;
        UpdatePercentage( false );
        currentTotal = value;
        UpdateTotal(true);
    }

    public void ResetTotal()
    {
        currentTotal = 0;
        UpdateTotal( false );
    }

    void UpdateAllLabels( bool shouldAnimate )
    {
        UpdateBets(shouldAnimate); 
        UpdatePercentage( shouldAnimate );
        UpdateTotal( shouldAnimate );
    }

    void UpdateBets( bool shouldAnimate )
    {
        currentBetsValueLabel.text = currentBets.ToString();
        if ( shouldAnimate )
        {
            betsAnimator.SetTrigger( popNameHash );
        }
    }

    void UpdatePercentage( bool shouldAnimate )
    {
        string s = string.Format( "{0}", currentPercentage );
        currentPercentageValueLabel.text = s;
        if ( shouldAnimate )
        {
            percentageAnimator.SetTrigger( popNameHash );
        }
    }

    void UpdateTotal( bool shouldAnimate )
    {
        currentTotalValueLabel.text = currentTotal.ToString();
        if ( shouldAnimate )
        {
            totalAnimator.SetTrigger( popNameHash );
        }
    }

}
