using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI level;
    [SerializeField] TMPro.TextMeshProUGUI points;
    [SerializeField] TMPro.TextMeshProUGUI target;
    [SerializeField] TMPro.TextMeshProUGUI roll;
    [SerializeField] TMPro.TextMeshProUGUI bonus;
    [SerializeField] TMPro.TextMeshProUGUI earnings;

    Animator levelAnimator;
    Animator pointsAnimator;
    Animator targetAnimator;
    Animator bonusAnimator;
    Animator earningsAnimator;
    Animator rollAnimator;

    readonly int popNameHash = Animator.StringToHash( "Pop" );

    private void OnEnable()
    {
        levelAnimator = level.GetComponent<Animator>();
        pointsAnimator = points.GetComponent<Animator>();
        targetAnimator = target.GetComponent<Animator>();
        rollAnimator = roll.GetComponent<Animator>();
        bonusAnimator = bonus.GetComponent<Animator>();
        earningsAnimator = earnings.GetComponent<Animator>();
    }


    public void SetLevel( int value, int value2 )
    {
        level.text = string.Format( "{0}.{1}", value, value2 );
    }
    public void SetPoints( int value )
    {
        points.text = value.ToString();
    }

    public void SetTarget( int value )
    {
        target.text = value.ToString();
    }
    public void SetRolls( int value, int max)
    {
        roll.text = value.ToString() + "/" + max.ToString();
    }
    public void SetEarnings( int value )
    {
        earnings.text = value.ToString();
    }

    public void SetBonus( int value )
    {
        bonus.text = value.ToString();
    }

    public delegate void OnUpdateLabel( int value );
    public IEnumerator AnimateAddingNumber( int startValue, int endValue, float duration, OnUpdateLabel updateLabel )
    {
        int difference = endValue - startValue;
        float elapsedTimePercentage = 0f;
        int value = startValue;

        while ( value < endValue )
        {
            elapsedTimePercentage += Time.deltaTime / duration;
            value = startValue + ( ( int ) ( difference * elapsedTimePercentage ) );
            value = Mathf.Clamp( value, 0, endValue );
            updateLabel( value );
            yield return null;
        }
        yield return null;
    }

    public IEnumerator SetPoints( int targetPoints, float duration ) {
        SetPoints( targetPoints );
        pointsAnimator.SetTrigger( popNameHash );
        yield return new WaitForSeconds( duration );
    }

    public IEnumerator AnimateSubstractingNumber( int startValue, int endValue, float duration, OnUpdateLabel updateLabel )
    {
        int difference = endValue - startValue;
        float elapsedTimePercentage = 0f;
        int value = startValue;

        while ( value > endValue )
        {
            elapsedTimePercentage += Time.deltaTime / duration;
            value = startValue + ( ( int ) ( difference * elapsedTimePercentage ) );
            value = Mathf.Clamp( value, endValue, startValue );
            updateLabel( value );
            yield return null;
        }
        yield return null;
    }

}
