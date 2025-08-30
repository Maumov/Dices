using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int currentLevel = 1;
    [SerializeField] int currentSubLevel = 0;
    [SerializeField] int subLevelAmount = 3;
    [SerializeField] int pointsObjective;
    [SerializeField] int currentAmountOfPoints;
    [SerializeField] int currentBonusPoints;
    [SerializeField] int finalEarnings;
    [SerializeField] int bonusForWinning = 50;
    [SerializeField] float[] subLevelMultiplier;
    [SerializeField] LevelUI levelUI;

    [SerializeField] GameFlowManager gameFlowManager;
    [SerializeField] RoundFlowController stageFlowManager;
    private void OnEnable()
    {
       
    }

    private void OnDisable()
    {
       
    }

    public int GetFinalEarnings()
    {
        return finalEarnings;
    }

    void ResetRun()
    {
        currentLevel = 1;
        currentSubLevel = 0;
        SetupLevel();
    }

    void AdvanceLevel()
    {
        if ( currentSubLevel == subLevelAmount )
        {
            currentLevel++;
            currentSubLevel = 0;
        }
        else
        {
            currentSubLevel++;
        }
    }

    void SetupLevel()
    {
        currentAmountOfPoints = 0;
        SetPointsObjective();
        UpdateLevelUI();
    }

    void UpdateLevelUI()
    {
        levelUI.SetLevel( currentLevel, currentSubLevel );
        levelUI.SetPoints( 0 );
        levelUI.SetTarget( pointsObjective );
    }

    void SetPointsObjective()
    {
        //x = level
        //a = 100
        //b = 300
        //c = 100
        //ax2 + bx + c
        int c = 100;
        int bx = ( currentLevel * 100 );
        int ax2 = ( int ) ( Mathf.Pow( currentLevel, 2f ) * 50 );
        int dx3 = ( int ) ( Mathf.Pow( currentLevel, 3f ) * 50 );

        //base
        pointsObjective = dx3 + ax2 + bx + c;
        //sublevel...
        pointsObjective = (int)( pointsObjective * subLevelMultiplier[ currentSubLevel ] );
    }

    public IEnumerator AddScore( int amount, float duration )
    {
        int startPoints = currentAmountOfPoints;
        int targetPoints = currentAmountOfPoints + amount;
        yield return levelUI.SetPoints( targetPoints, duration );
        //yield return levelUI.AnimateAddingNumber( startPoints, targetPoints, duration, levelUI.SetPoints );
        currentAmountOfPoints = targetPoints;
        yield return null;
    }
    public void Upgrade()
    {
        AdvanceLevel();
    }

    void GameOver()
    {
        Debug.Log("GAME OVER");
    }

    public bool playerWon()
    {
        return currentAmountOfPoints >= pointsObjective;
    }
    
    float duration = 1f;
    public IEnumerator ResolvePostCalculation()
    {

        yield return new WaitForSeconds( duration );

        //yield return levelUI.SetPoints(  );
        /*
        int difference = pointsObjective - currentAmountOfPoints;
        if ( difference > 0 )
        {
            StartCoroutine( levelUI.AnimateSubstractingNumber( currentAmountOfPoints, 0, duration, levelUI.SetPoints ) );
            yield return levelUI.AnimateSubstractingNumber( pointsObjective, difference, duration, levelUI.SetTarget );
            currentAmountOfPoints = 0;
            pointsObjective = difference;
        }
        else
        {
            StartCoroutine( levelUI.AnimateSubstractingNumber( currentAmountOfPoints, -difference, duration, levelUI.SetPoints ) );
            yield return levelUI.AnimateSubstractingNumber( pointsObjective, 0, duration, levelUI.SetTarget );
            currentAmountOfPoints = -difference;
            pointsObjective = 0;
        }
        */
        yield return new WaitForSeconds( duration );
         
        yield return null;
    }


    IEnumerator Bonus()
    {
        /*
        currentBonusPoints = currentLevel * bonusForWinning;
        yield return levelUI.AnimateAddingNumber( 0, currentBonusPoints, duration, levelUI.SetBonus );
        yield return new WaitForSeconds( duration );
        */
        yield return null;
    }

    public IEnumerator Won()
    {
        finalEarnings = 0;
        //Bonus for winning
        //yield return Bonus();
        
        //add everything to earnings
        /*
        StartCoroutine( levelUI.AnimateSubstractingNumber( currentAmountOfPoints, 0, duration, levelUI.SetPoints ) );
        yield return levelUI.AnimateAddingNumber( 0, currentAmountOfPoints, duration, levelUI.SetEarnings );
        yield return new WaitForSeconds( duration );
        finalEarnings += currentAmountOfPoints;

        StartCoroutine( levelUI.AnimateSubstractingNumber( currentBonusPoints, 0, duration, levelUI.SetBonus ) );
        yield return levelUI.AnimateAddingNumber( finalEarnings, finalEarnings + currentBonusPoints, duration, levelUI.SetEarnings );
        yield return new WaitForSeconds( duration );
        finalEarnings += currentBonusPoints;
        */
        yield return null;

    }

    public IEnumerator Lose()
    {
        GameOver();
        yield return null;
    }

    public void FlushEaningsToPlayer()
    {
        finalEarnings = 0;
        //levelUI.SetEarnings( finalEarnings );
    }

}
