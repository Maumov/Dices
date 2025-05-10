using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu( fileName = "DataChips", menuName = "ScriptableObjects/ChipsData", order = 1 )]
public class ChipSetDataScriptableObject : ScriptableObject
{
    public List<GameObject> Chips;
}
