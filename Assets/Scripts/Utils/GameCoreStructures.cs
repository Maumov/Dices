using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BetButtonData
{
    public string buttonName;
    public string buttonDescription;
    public BETS betButtonId;
    [SerializeField] CalculationStats baseStats;
    [SerializeField] CalculationStats betStats;
    [SerializeField] CalculationStats upgrades;
    
    [SerializeField] CalculationStats currentStats => baseStats + upgrades + betStats;

    public void SetBaseStats( CalculationStats newStats )
    {
        baseStats = newStats;
        upgrades.Reset();
    }

    public void AddBaseStats( CalculationStats newStats )
    {
        baseStats += newStats;
    }

    public void AddUpgrade( CalculationStats newUpgrade)
    {
        upgrades += newUpgrade;
    }

    public void AddBetStats( CalculationStats newBet)
    {
        betStats += newBet;
    }

    public void RemoveBetStats( CalculationStats newBet )
    {
        betStats -= newBet;
    }
    public void ResetBetStats()
    {
        betStats -= betStats;
    }

    public int CurrentChips
    {
        get => currentStats.chips;
    }

    public int CurrentMultiplier
    {
        get => currentStats.multiplier;
    }
    public int CurrentMoney
    {
        get => currentStats.moneyReturn;
    }
}
[System.Serializable]
public struct CalculationStats
{
    public int chips;
    public int multiplier;
    public int moneyReturn;

    public void Reset()
    {
        chips = 0;
        multiplier = 0;
        moneyReturn = 0;
    }

    public CalculationStats( int _chips, int _multiplier, int _moneyReturn)
    {
        chips = _chips;
        multiplier = _multiplier;
        moneyReturn = _moneyReturn;
    }

    public static CalculationStats operator +( CalculationStats a, CalculationStats b )
    {
        return new CalculationStats( a.chips + b.chips, a.multiplier + b.multiplier, a.moneyReturn + b.moneyReturn );
    }
    public static CalculationStats operator -( CalculationStats a, CalculationStats b )
    {
        return new CalculationStats( a.chips - b.chips, a.multiplier - b.multiplier, a.moneyReturn - b.moneyReturn );
    }
}

public enum BETS
{
    Contains1,
    Contains2,
    Contains3,
    Contains4,
    Contains5,
    Contains6,
    Containspair1,
    Containspair2,
    Containspair3,
    Containspair4,
    Containspair5,
    Containspair6,
    Containsthrice1,
    Containsthrice2,
    Containsthrice3,
    Containsthrice4,
    Containsthrice5,
    Containsthrice6,
    ContainsQuad1,
    ContainsQuad2,
    ContainsQuad3,
    ContainsQuad4,
    ContainsQuad5,
    ContainsQuad6,
    ContainsPenta1,
    ContainsPenta2,
    ContainsPenta3,
    ContainsPenta4,
    ContainsPenta5,
    ContainsPenta6,
    ContainsPair,
    ContainsThrice,
    ContainsQuad,
    ContainsPenta,
    ContainsDoublePair,
    IsFullHouse,
    IsPair,
    IsThrice,
    IsQuad,
    IsPenta,
    IsDoublePair
}
