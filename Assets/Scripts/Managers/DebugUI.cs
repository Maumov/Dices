using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    [SerializeField] ChipsManager chipsManager;
    [SerializeField] DicesManager dicesManager;
    [SerializeField] PlayerState playerState;
    [SerializeField] GameFlowInteractions gameFlowInteractions;

    [SerializeField] Button ResetButton;
    [SerializeField] TMPro.TextMeshProUGUI SumDices;
    [SerializeField] TMPro.TextMeshProUGUI[] ResultsUI;

    int sum;
    Dictionary<BETS, bool> results = new Dictionary<BETS, bool>();

    float x = 0f;
    float y = 0f;
    float width = 200f;
    float height = 20f;
    public void SetResultValues( int _sum, Dictionary<BETS, bool> _results )
    {
        sum = _sum;
        results = _results;
    }

    private void OnGUI()
    {
#if UNITY_EDITOR
        Results();
        
        Chips();

        Dices();

        Money();

        GameFlow();
#endif
    }
    
    bool showResults = false;
    void Results()
    {
        x = 10f;
        y = 10f;
        showResults = GUI.Toggle( new Rect( x, y, width, height ), showResults, "Show Results" );
        if ( !showResults )
        {
            return;
        }
        string label = "Sum Of Dices : " + sum;
        y += height * 0.7f;
        GUI.Label( new Rect( x, y, width, height ), label );
        y += height * 0.7f;
        for ( int i = 0 ; i < 41 ; i++ )
        {
            BETS bets = ( BETS ) i;
            bool res = false;
            results.TryGetValue( bets, out res );
            label = string.Format( bets + " : " + res );
            GUI.color = res ? Color.green : Color.white;
            GUI.Label( new Rect( x, y + ( height * 0.7f * i ), width, height ), label );
        }
    }

    bool showChips = false;
    void Chips()
    {
        
        y = 10f;
        width = 120f;
        x += width + 100f;
        showChips = GUI.Toggle( new Rect( x, y, width, height ), showChips, "Chips Menu" );
        showChips = GUI.Toggle( new Rect( x, y, width, height ), showChips, "Chips Menu" );
        if ( !showChips )
        {
            return;
        }
        y += height;
        if ( GUI.Button( new Rect( x, y, width, height ), "White Chip" ) )
        {
            chipsManager.GiveChipToPlayer( 0 );
        }
        y += height;
        if ( GUI.Button( new Rect( x, y, width, height ), "Red Chip" ) )
        {
            chipsManager.GiveChipToPlayer( 1 );
        }
    }

    bool showDices = false;
    void Dices()
    {
        x += width + 100f;
        y = 10f;
        showDices = GUI.Toggle( new Rect( x, y, width, height ), showDices, "Dices Menu" );
        if ( !showDices )
        {
            return;
        }
        y += height;
        if ( GUI.Button( new Rect( x, y, width, height ), "White Dice" ) )
        {
            dicesManager.GiveDiceToPlayer( 0 );
        }
        y += height;
        if ( GUI.Button( new Rect( x, y, width, height ), "Red Dice" ) )
        {
            dicesManager.GiveDiceToPlayer( 1 );
        }
    }

    void Money()
    {
        x += width + 100f;
        y = 30f;
        if ( GUI.Button( new Rect( x, y, width, height ), "Give $10" ) )
        {
            playerState.AddCoins( 10 );
        }
    }

    public void ResetResultValues( int sum )
    {
        results = new Dictionary<BETS, bool>();
        sum = 0;
        /*
        SumDices.text = string.Format( "{0}", sum );
        for ( int i = 0 ; i < ResultsUI.Length ; i++ )
        {
            ResultsUI[ i ].text = string.Format( "False");
        }
        */
    }

    void GameFlow()
    {
        x += width + 100f;

        y = 30f;
        if ( GUI.Button( new Rect( x, y, width, height ), "Mainmenu" ) )
        {
            gameFlowInteractions.GoToMainMenu();
        }

        y += height;
        if ( GUI.Button( new Rect( x, y, width, height ), "StageSelect" ) )
        {
            gameFlowInteractions.GoToStageSelect();
        }

        y += height;
        if ( GUI.Button( new Rect( x, y, width, height ), "Play" ) )
        {
            gameFlowInteractions.GoToPlay();
        }

        y += height;
        if ( GUI.Button( new Rect( x, y, width, height ), "Shop" ) )
        {
            gameFlowInteractions.GoToShop();
        }
        
    }
}
