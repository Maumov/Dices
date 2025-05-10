using System;
using UnityEngine;
[CreateAssetMenu( fileName = "DataChips", menuName = "ScriptableObjects/ChipData", order = 1 )]

public class ChipDataScriptableObject : ScriptableObject
{
    public ChipData chipData;
    /*
    public string chipName = "Name";
    public string description = "Description";

    public int buyCost = 3;
    public int sellCost = 2;

    public CalculationStats stats;
    
    [SerializeField] private string effect; // Store type as a string

    [Header( "Visuals" )]
    public Color color = Color.white;

    public string Effect
    {
        get => effect;
        set => effect = value;
    }

    public Type GetBehaviorType()
    {
        return Type.GetType( effect );
    }

    public void SetEffectType( Type effectType)
    {
        effect = effectType?.AssemblyQualifiedName; // Store full type name
    }
    */
}
