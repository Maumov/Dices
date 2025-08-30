using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerChips : MonoBehaviour
{
    [SerializeField] List<GameObject> currentChipsSet;
    [SerializeField] List<ChipController> chips = new List<ChipController>();
    
    [SerializeField] ItemContainer playerChipContainer;

    [SerializeField] RoundFlowController stageFlowController;
    
    public bool CanInteractWithChips()
    {
        return stageFlowController.CanInteractWithChips();
    }

    public int ChipsCount()
    {
        return currentChipsSet.Count;
    }

    public void SetCurrentChips( List<GameObject> startingChips)
    {
        currentChipsSet = new List<GameObject>();
        currentChipsSet.AddRange( startingChips );
    }

    public void AddNewChip( GameObject newChip)
    {
        currentChipsSet.Add( newChip);  
        NewChipAdded( newChip );
    }

    public void SetupPlayerAtStageStart()
    {
        SpawnChips();
    }
    void CleanCurrentChips()
    {
        playerChipContainer.DestroyItems();
        chips.Clear();
    }

    void SpawnChips()
    {
        CleanCurrentChips();
        StartCoroutine( SetChipsToPlayer() );
    }

    [SerializeField] float spawnTime = 0.5f;
    IEnumerator SetChipsToPlayer()
    {
        chips = new List<ChipController> ();
        int chipsToSpawn = currentChipsSet.Count;
        for ( int i = 0 ; i < chipsToSpawn ; i++ )
        {
            NewChipAdded( currentChipsSet[ i ].gameObject );
            yield return new WaitForSeconds( spawnTime );
        }
        yield return null;
    }

    void NewChipAdded( GameObject newChip )
    {
        GameObject go = Instantiate( newChip, Vector3.zero, Quaternion.identity );
        ChipController d = go.GetComponent<ChipController>();
        d.SetupItem();
        d.SetItemContainer( playerChipContainer );
        AddChip( d );
        playerChipContainer.NewItemAdded( go );
    }


    private void OnDisable()
    {
        stageFlowController.OnDicesReseted -= ReturnChipsToPlayer;
    }

    void AddChip( ChipController chipController )
    {
        chips.Add( chipController );
    }

    void RemoveChip( ChipController chipController )
    {
        chips.Remove( chipController );
    }
    void ReturnChipsToPlayer()
    {
        for ( int i = 0 ; i < chips.Count ; i++ )
        {
            chips[ i ].ReturnToPlayer( playerChipContainer );
        }
        playerChipContainer.ReOrganizeItems();
    }

    public void ReturnChipToPlayer( ChipController chipController )
    {
        chipController.ReturnToPlayer( playerChipContainer );
        playerChipContainer.ReOrganizeItems();
    }

    public void SelectChip( ChipController chip )
    {
        for ( int i = 0 ; i < chips.Count ; i++ )
        {
            if ( chips[ i ] == chip )
            {
                chips[i].SetSelected( !chips[i].GetSelected() );
            }
            else
            {
                chips[ i ].SetSelected( false );
            }
        }
    }

    public void DeselectAllChips()
    {
        for ( int i = 0 ; i < chips.Count ; i++ )
        {
            chips[i].SetSelected( false );
        }
    }


    ChipController GetSelectedChip()
    {
        for ( int i = 0 ; i< chips.Count ; i++ )
        {
            if ( chips[i].GetSelected() )
            {
                return chips[ i ];
            }
        }
        return GetNextChipInPlayerContainer();
    }

    ChipController GetNextChipInPlayerContainer()
    {
        for ( int i = 0 ; i< chips.Count ; i++ )
        {
            if ( chips[i].IsInContainer( playerChipContainer) )
            {
                return chips[ i ];
            }
        }
        return null;
    }

    public void AddChipToBet( BetButton button )
    {
        ChipController selectedChip = GetSelectedChip();
        if ( selectedChip != null )
        {
            ItemContainer betButton = button.GetComponent<ItemContainer>();
            selectedChip.AddToBet( betButton );
        }
    }


    /*
    public void TakeFromChipContainer( ChipController chip )
    {
        playerChipContainer.RemoveItem( chip.gameObject );
    }
    */
}
