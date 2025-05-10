using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "PlayerStart", menuName = "ScriptableObjects/PlayerStartData", order = 2 )]
public class PlayerStartScriptableObject : ScriptableObject
{
    public PlayerStart playerStart;

    private void Reset()
    {
        playerStart.rolls = 3;
        playerStart.startMoney = 10;
        playerStart.maxChips = 5;
        playerStart.maxDices = 10;
        playerStart.DiceSetName = "Dice Set Name";
        playerStart.DiceSetDescription = "Dice Set Description";
    }
}
[System.Serializable]
public struct PlayerStart
{
    public string DiceSetName;
    public string DiceSetDescription;

    public int startMoney;
    public int rolls;
    public int maxDices;
    public int maxChips;

    public DiceSetDataScriptableObject diceSet;
    public ChipSetDataScriptableObject chipsSet;
    public BetButtonsSetScriptableObject betButtonsSet;
}