using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    [SerializeField] ChipsManager chipsManager;
    [SerializeField] DicesManager dicesManager;
    [SerializeField] PlayerState playerState;

    [SerializeField] Button ResetButton;
    [SerializeField] TMPro.TextMeshProUGUI SumDices;
    [SerializeField] TMPro.TextMeshProUGUI[] ResultsUI;

    int sum;
    Dictionary<BETS, bool> results = new Dictionary<BETS, bool>();

    public void SetResultValues( int _sum, Dictionary<BETS, bool> _results )
    {
        sum = _sum;
        results = _results;
    }

    private void OnGUI()
    {
        Results();
        
        Chips();

        Dices();

        Money();
    }
    
    bool showResults = false;
    void Results()
    {
        showResults = GUI.Toggle( new Rect( 10f, 10f, 200f, 20f ), showResults, "Show Results" );
        if ( !showResults )
        {
            return;
        }
        string label = "Sum Of Dices : " + sum;
        GUI.Label( new Rect( 10f, 21f, 200f, 20f ), label );
        for ( int i = 0 ; i < 41 ; i++ )
        {
            BETS bets = ( BETS ) i;
            bool res = false;
            results.TryGetValue( bets, out res );
            label = string.Format( bets + " : " + res );
            GUI.color = res ? Color.green : Color.white;
            GUI.Label( new Rect( 10f, 32f + ( 11 * i ), 200f, 20f ), label );
        }
    }

    bool showChips = false;
    void Chips()
    {
        showChips = GUI.Toggle( new Rect( 220f, 10f, 200f, 20f ), showChips, "Chips Menu" );
        if ( !showChips )
        {
            return;
        }

        if ( GUI.Button( new Rect( 220f, 30f, 200f, 20f ), "White Chip" ) )
        {
            chipsManager.GiveChipToPlayer( 0 );
        }
        if ( GUI.Button( new Rect( 220f, 50f, 200f, 20f ), "Red Chip" ) )
        {
            chipsManager.GiveChipToPlayer( 1 );
        }
    }

    bool showDices = false;
    void Dices()
    {
        showDices = GUI.Toggle( new Rect( 420f, 10f, 200f, 20f ), showDices, "Dices Menu" );
        if ( !showDices )
        {
            return;
        }

        if ( GUI.Button( new Rect( 420f, 30f, 200f, 20f ), "White Dice" ) )
        {
            dicesManager.GiveDiceToPlayer( 0 );
        }
        if ( GUI.Button( new Rect( 420f, 50f, 200f, 20f ), "Red Dice" ) )
        {
            dicesManager.GiveDiceToPlayer( 1 );
        }
    }

    void Money()
    {
        if ( GUI.Button( new Rect( 620f, 30f, 200f, 20f ), "Give $10" ) )
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
}
