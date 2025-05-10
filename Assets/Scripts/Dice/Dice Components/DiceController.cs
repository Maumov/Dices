using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum DiceState
{
    Hand,
    HandSelected,
    Rolling,
    Rolled
}

[Serializable]
public struct DiceData
{
    public string diceTitle;
    public string diceDescription;
    public int buyCost;
    public int sellCost;
    public int[] diceValues;
    public DiceState diceState;
}

public class DiceController : ItemController
{
    [Header( "Dice setup" )]
    [SerializeField] DiceScriptableObject diceScriptableObject;
    [SerializeField] DiceData diceData;

    [SerializeField] bool selected;
    [SerializeField] List<BETS> flags;
    [SerializeField] bool countedOnABet;

    [SerializeField] DicePhysics dicePhysics;
    [SerializeField] DiceAnimation diceAnimation;
    [SerializeField] DiceValueSelector diceValueSelector;
    [SerializeField] DiceVisuals diceVisuals;
    [SerializeField] DiceEffect diceEffect;

    [SerializeField] IMoveTo move;

    BetButtonsManager betButtons;
    PlayerDices playerDices;
    private void Start()
    {
        if ( diceScriptableObject != null)
        {
            SetupItem();
        }
    }

    public void SetCountedOnABet( Dictionary<BETS, bool> results )
    {
        for ( int i = 0 ; i < flags.Count ; i++ )
        {
            if ( betButtons.BetButtonHasChips( flags[ i ] ) )
            {
                countedOnABet = true;
            }
        }
    }
    public string GetTitle()
    {
        return diceData.diceTitle;
    }
    public string GetDescription()
    {
        return diceEffect.GetDescription();
    }

    void Init()
    {
        diceData = diceScriptableObject.diceData;
        diceValueSelector.SetupSides( diceData );
        diceVisuals.SetupVisuals( diceData );
        
        /*
        Type behaviorType = diceScriptableObject.GetBehaviorType();
        if ( behaviorType != null && behaviorType.IsSubclassOf( typeof( MonoBehaviour ) ) )
        {
            gameObject.AddComponent( behaviorType );
        }
        */
        diceEffect = GetComponent<DiceEffect>();
        move = GetComponent<IMoveTo>();
    }

    public override void SetupItem()
    {
        //diceScriptableObject = diceData;
        Init();
    }

    public bool Counted()
    {
        return countedOnABet;
    }

    public void SetFlags()
    {
        flags = new List<BETS>();
    }

    public bool HasFlag(BETS flag)
    {
        return flags.Contains(flag);
    }

    public void AddFlag( BETS newFlag)
    {
        flags.Add(newFlag); 
    }

    public BETS[] GetFlags()
    {
        return flags.ToArray();
    }

    public delegate void DiceAction();
    public event DiceAction OnSelected, OnUnselected;

    public bool IsSelected()
    {
        return selected;
    }

    public DiceState GetState()
    {
        return diceData.diceState;
    }

    public bool FinishedRolling()
    {
        switch ( diceData.diceState )
        {
            case DiceState.Hand:
                return true;
            case DiceState.HandSelected:
                return true;
            case DiceState.Rolling:
                return false;
            case DiceState.Rolled:
                return valueRolled != 0;
        }
        return true;
    }


    private void OnEnable()
    {
        diceData.diceState = DiceState.Hand;   
        betButtons = FindObjectOfType<BetButtonsManager>();
        playerDices = FindObjectOfType<PlayerDices>();
    }

    private void Update()
    {
        switch ( diceData.diceState )
        {
            case DiceState.Hand:
                Hand();
                break;
            case DiceState.HandSelected:
                HandSelected();
                break;
            case DiceState.Rolling:
                Rolling();
                break;
            case DiceState.Rolled:
                Rolled();
                break;
        }

    }

    [Header( "Roll Values" )]
    [SerializeField] int valueRolled;
    private void Rolled()
    {
        if ( valueRolled == 0)
        {
            valueRolled = diceValueSelector.GetValueRolled();
        }
    }

    public int GetValue()
    {
        return valueRolled;
    }
    
    private void Rolling()
    {
        if ( !dicePhysics.IsStillRolling() )
        {
            diceData.diceState = DiceState.Rolled;
        }
        
    }

    void Hand()
    {
        diceAnimation.RotateAnimationInHand();
    }

    void HandSelected()
    {
        diceAnimation.RotateAnimationInHandSelected();
    }
  

    private void OnMouseOver()
    {
        switch ( diceData.diceState )
        {
            case DiceState.Hand:
                diceAnimation.RotateAnimationInHandSelected();
                break;
            case DiceState.HandSelected:
                break;
            case DiceState.Rolling:
                break;
            case DiceState.Rolled:
                break;
        }

    }

    private void OnMouseUpAsButton()
    {
        switch ( diceData.diceState )
        {
            case DiceState.Hand:
                SetSelected();
                break;
            case DiceState.HandSelected:
                SetUnselected();
                break;
            case DiceState.Rolling:
                break;
            case DiceState.Rolled:
                break;
        }
    }
    
    public void SendRolling()
    {
        if ( !selected )
        {
            return;
        }
        OnUnselected?.Invoke();
        diceData.diceState = DiceState.Rolling;
        selected = false;
        dicePhysics.SendRolling();
    }

    void UnStuck()
    {
        diceData.diceState = DiceState.Rolling;
        dicePhysics.Unstuck();
    }

    public void BackToHand()
    {
        selected = false;
        dicePhysics.DisablePhysics();
        
        diceData.diceState = DiceState.Hand;
        valueRolled = 0;
        countedOnABet = false;
        move.MoveToPosition();
    }

    
    bool CanBeSelected()
    {
        
        if ( !playerDices.CanSelectDice() )
        {
            return false;
        }
        
        return true;
    }

    void SetSelected()
    {
        if ( !CanBeSelected() )
        {
            return;
        }
        diceData.diceState = DiceState.HandSelected;
        selected = true;
        OnSelected?.Invoke();
    }

    void SetUnselected()
    {
        diceData.diceState = DiceState.Hand;
        selected = false;
        OnUnselected?.Invoke();
    }

    public virtual IEnumerator ProcessDiceEffect()
    {
        yield return diceEffect.ProcessEffect();
    }

    public override void SellItem()
    {
        
    }

    public override void GiveItem()
    {
        FindObjectOfType<PlayerDices>().AddNewDice( gameObject );
    }

    public override int GetItemBuyPrice()
    {
        return diceData.buyCost;
    }

    public override int GetItemSellPrice()
    {
        return diceData.sellCost;
    }
}
