using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "DataBetButtons", menuName = "ScriptableObjects/AllBetButtonsData", order = 1 )]

public class BetButtonsSetScriptableObject : ScriptableObject
{
    public List<BetButtonDataScriptableObject> AllBetButtons;
}
