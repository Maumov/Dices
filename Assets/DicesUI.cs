using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicesUI : MonoBehaviour
{
    [SerializeField] ItemContainer dicesContainer;
    [SerializeField] PlayerDices playerDices;
    [SerializeField] GameObject dicesPanel;
    List<DiceController> dices = new List<DiceController>();

    bool sw = false;
    public void ToggleCurrentDicesCollection()
    {
        if ( !sw )
        {
            //show
            dicesPanel.SetActive( true );
            List<GameObject> diceSet = playerDices.GetCurrentDiceSet();
            SpawnDices( diceSet );
        }
        else
        {
            //hide
            StopAllCoroutines();
            dicesPanel.SetActive( false );
            CleanCurrentDices();
        }
        sw = !sw;
    }

    public void SpawnDices( List<GameObject> diceSet )
    {
        CleanCurrentDices();
        StartCoroutine( SetDices( diceSet ) );
    }

    void CleanCurrentDices()
    {
        dicesContainer.DestroyItems();
        dices.Clear();
    }

    [SerializeField] float spawnTime = 0.5f;
    IEnumerator SetDices( List<GameObject> diceSet )
    {
        dices = new List<DiceController>();
        int dicesToSpawn = diceSet.Count;
        for ( int i = 0 ; i < dicesToSpawn ; i++ )
        {
            NewDiceAdded( diceSet[ i ] );
            yield return new WaitForSeconds( spawnTime );
        }
        yield return null;
    }

    void NewDiceAdded( GameObject newDice )
    {
        GameObject go = Instantiate( newDice, Vector3.zero, Quaternion.identity );
        DiceController d = go.GetComponent<DiceController>();
        d.SetupItem();
        AddDice( d );
        dicesContainer.NewItemAdded( go );
    }
    void AddDice( DiceController newDice )
    {
        dices.Add( newDice );
    }
}
