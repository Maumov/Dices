using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu( fileName = "DataDice", menuName = "ScriptableObjects/DiceSetData", order = 2 )]
public class DiceSetDataScriptableObject : ScriptableObject
{
    public List<GameObject> dices;
}
