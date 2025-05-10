using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChips : MonoBehaviour
{
    [SerializeField] List<GameObject> currentChipsSet;
    [SerializeField] List<ChipController> chips = new List<ChipController>();
    
    [SerializeField] ItemContainer playerChipContainer;

    [SerializeField] StageFlowController stageFlowController;
    [SerializeField] GameFlowManager gameFlowManager;
    private void OnEnable()
    {    
        stageFlowController.OnDicesReseted += ReturnChipsToPlayer;
        stageFlowController.OnStageStart += SetupPlayerAtStageStart;
    }

    public bool CanInteractWithChips()
    {
        return stageFlowController.CanInteractWithChips();
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

    void SetupPlayerAtStageStart()
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
        if ( !gameFlowManager.IsInState( GameFlowState.Play ) )
        {
            return;
        }
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

   
    /*
    public void TakeFromChipContainer( ChipController chip )
    {
        playerChipContainer.RemoveItem( chip.gameObject );
    }
    */
}
