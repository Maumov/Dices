using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetButton : MonoBehaviour
{
   
    [SerializeField] BetButtonData currentBetData;

    [SerializeField] List<ChipController> chips;
    CalculationManager calculationManager;
    [SerializeField] ItemContainer chipContainer;
    [SerializeField] ChipSelector chipSelector;
    
    public delegate void BetButtonDelegate();
    public event BetButtonDelegate OnBetProcessed, OnDataUpdated, OnChipAdded, OnChipRemoved;

    public BETS GetKey()
    {
        return currentBetData.betButtonId;
    }

    public BetButtonData GetCurrentBetData()
    {
        return currentBetData;
    }

    public bool HasBets()
    {
        return chips.Count > 0;
    }

    private void OnEnable()
    {
        calculationManager = FindObjectOfType<CalculationManager>();
        chipContainer.OnItemRemoved += RemoveChip;
        chipContainer.OnItemAdded += AddChip;
    }

    private void OnDisable()
    {
        chipContainer.OnItemRemoved -= RemoveChip;
        chipContainer.OnItemAdded -= AddChip;
    }

    public string GetTitle()
    {
        return currentBetData.buttonName;
    }
    public string GetDescription()
    {
        return currentBetData.buttonDescription;
    }

    public void ClearChips()
    {
        chips = new List<ChipController>();
        chipContainer.DestroyItems();
        currentBetData.ResetBetStats();
        OnDataUpdated?.Invoke();
    }

    public void SetupValues( BetButtonDataScriptableObject buttonStats )
    {
        chips = new List<ChipController>();
        currentBetData.buttonName = buttonStats.Title;
        currentBetData.buttonDescription = buttonStats.Description;
        currentBetData.betButtonId = buttonStats.typeOfBets;
        SetButtonStats( buttonStats.betButtonData ) ;
        OnDataUpdated?.Invoke();
    }

    void SetButtonStats( CalculationStats stats )
    {
        currentBetData.SetBaseStats( stats );
        OnDataUpdated?.Invoke();
    }

    void AddBetStats( CalculationStats stats )
    {
        currentBetData.AddBetStats( stats );
        OnDataUpdated?.Invoke();
    }

    void RemoveBetStats( CalculationStats stats )
    {
        currentBetData.RemoveBetStats( stats );
        OnDataUpdated?.Invoke();
    }

    int GetChips()
    {
        return currentBetData.CurrentChips;
    }

    int GetMultiplier()
    {
        return currentBetData.CurrentMultiplier;
    }

    public void ProcessBetButton()
    {
        calculationManager.AddBetValues( GetChips(), GetMultiplier() );
        OnBetProcessed?.Invoke();
    }
    public virtual IEnumerator ProcessBetEffect()
    {
        yield return null;
    }

    public virtual IEnumerator ProcessChips()
    {
        for ( int i = 0 ; i < chips.Count ; i++ )
        {
            yield return chips[i].ProcessEffect();
        }
    }

    void AddChip( GameObject chip)
    {
        AddChip( chip.GetComponent<ChipController>() );
    }

    void AddChip( ChipController chip )
    {
        chips.Add( chip );
        chip.SetItemContainer( chipContainer );
        AddBetStats( chip.GetChipStats() );
        Debug.Log( gameObject.name + " " + GetKey() + " added a Chip.");
        OnChipAdded?.Invoke();
    }

    void RemoveChip( GameObject chip ) { 
        RemoveChip( chip.GetComponent<ChipController>() );
    }

    void RemoveChip( ChipController chip )
    {
        chips.Remove( chip );
        RemoveBetStats( chip.GetChipStats() );
        Debug.Log( gameObject.name + " " + GetKey() + " Chip removed." );
        OnChipRemoved?.Invoke();
    }

    public void OnMouseDown()
    {
        chipSelector.BetButtonClicked( this );
    }
}