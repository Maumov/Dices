using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DicesManager : MonoBehaviour
{

    [Header( "dices All Data" )]
    [SerializeField] UnityEngine.GameObject dicePrefab;
    [SerializeField] DiceSetDataScriptableObject AllDices;
    [SerializeField] PlayerDices playerDices;

    public void GiveDiceToPlayer( int id )
    {
        playerDices.AddNewDice( AllDices.dices[ id ] );
    }

    public int GetAllDicesCount()
    {
        return AllDices.dices.Count;
    }
    public GameObject GetDice(int index)
    {
        return AllDices.dices[ index ];
    }

}
