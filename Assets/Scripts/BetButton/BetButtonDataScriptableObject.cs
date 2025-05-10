using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu( fileName = "DataBetButton", menuName = "ScriptableObjects/BetButtonData", order = 1 )]
public class BetButtonDataScriptableObject : ScriptableObject
{
    public string Title;
    public string Description = "Bet description <color=blue>{0} , <color=red>{1}, <color=yellow>{2}";
    public BETS typeOfBets;
    public CalculationStats betButtonData;
}
