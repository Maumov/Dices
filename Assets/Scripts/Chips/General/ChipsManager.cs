using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipsManager : MonoBehaviour
{
    
    [Header( "Chips All Data" )]

    [SerializeField] ChipSetDataScriptableObject AllChips;
    [SerializeField] PlayerChips playerChips;
    public void GiveChipToPlayer( int id )
    {
        playerChips.AddNewChip( AllChips.Chips[ id ] );
    }

    public int GetAllChipsCount()
    {
        return AllChips.Chips.Count;
    }
    public GameObject GetChip( int index )
    {
        return AllChips.Chips[ index ];
    }
}
