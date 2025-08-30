using System;
using System.Collections;
using UnityEngine;
using static ChipPlayerInteractions;


[Serializable]
public struct ChipData
{
    public string chipTitle;
    public string chipDescription;
    public int buyCost;
    public int sellCost;
    public CalculationStats stats;
    public Color color;
}
public class ChipController : ItemController
{
    [SerializeField] ChipDataScriptableObject chipDataScriptableObject;
    [SerializeField] ChipData chipData;
    [SerializeField] ChipEffect chipEffect;
    [SerializeField] ChipVisuals chipVisuals;
    [SerializeField] ItemContainer currentContainer;
    [SerializeField] bool selected;

    public delegate void ChipInteraction();
    public event ChipInteraction OnSelected, OnUnselected;
    public bool GetSelected()
    {
        return selected;
    }

    public void SetSelected( bool value )
    {
        selected = value;
        if ( selected )
        {
            OnSelected?.Invoke();
        }
        else
        {
            OnUnselected?.Invoke();
        }
    }

    PlayerChips playerChips;
    private void OnEnable()
    {
        playerChips = FindObjectOfType<PlayerChips>();
    }

    public bool CanBeInteractedWith()
    {
        return playerChips.CanInteractWithChips();
    }

    public void SetItemContainer( ItemContainer newContainer )
    {
        currentContainer = newContainer;
    }

    public CalculationStats GetChipStats()
    {
        return chipData.stats;
    }
    public string GetTitle()
    {
        return chipData.chipTitle;
    }
    public string GetDescription()
    {
        return chipEffect.GetDescription();
    }

    void Init()
    {
        chipData = chipDataScriptableObject.chipData;
        chipVisuals.SetupVisuals( chipData );
    }
    public override void SetupItem()
    {
        Init();
    }

    void Selected()
    {
        /*
        RemoveFromCurrentContainer( false );
        */
    }

    void Unselected()
    {
        /*
        Debug.Log("Chip Controller Unselected");
        TryToAddToBet();
        */
    }

    [SerializeField] LayerMask layerMaskBetButton;
    void TryToAddToBet()
    {
        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = Vector3.down;
        RaycastHit hit;
        
        if ( Physics.Raycast( ray, out hit, transform.position.y + 15f, layerMaskBetButton ) )
        {
            ItemContainer betButton = hit.collider.GetComponent<ItemContainer>();
            betButton.NewItemAdded( gameObject );
        }
        else
        {
            ReturnToContainer( false );
        }
    }

    public void ReturnToPlayer( ItemContainer playerChipsContainer )
    {
        if ( playerChipsContainer == currentContainer)
        {
            return;
        }
        RemoveFromCurrentContainer( true );
        SetItemContainer( playerChipsContainer );
        ReturnToContainer( true );
    }

    public void AddToBet( ItemContainer newContainer )
    {
        RemoveFromCurrentContainer( true );
        SetItemContainer( newContainer );
        ReturnToContainer( false );
        SetSelected( false );
    }

    public bool IsInContainer( ItemContainer container)
    {
        return container == currentContainer;
    }

    void ReturnToContainer( bool simple)
    {
        if ( simple )
        {
            currentContainer.SimpleAdd( gameObject );
        }
        else
        {
            currentContainer.NewItemAdded( gameObject );
        }
        
    }

    void RemoveFromCurrentContainer( bool simpleRemove )
    {
        if ( simpleRemove )
        {
            currentContainer.SimpleRemove( gameObject );
        }
        else
        {
            currentContainer.RemoveItem( gameObject );
        }
        
    }

    public IEnumerator ProcessEffect()
    {
        yield return chipEffect.ProcessEffect();
    }

    public override void SellItem()
    {

    }

    public override void GiveItem()
    {
        FindObjectOfType<PlayerChips>().AddNewChip( gameObject );
    }

    public override int GetItemBuyPrice()
    {
        return chipData.buyCost;
    }

    public override int GetItemSellPrice()
    {
        return chipData.sellCost;
        
    }

    public override bool CanBuyItem()
    {
        PlayerState playerState = FindObjectOfType<PlayerState>();
        return playerState.GetChipsCount() < playerState.GetMaxChips();
    }
}
