using System;

using UnityEngine;

[CreateAssetMenu( fileName = "DataDice", menuName = "ScriptableObjects/DiceData", order = 1 )]

public class DiceScriptableObject : ScriptableObject
{
    public DiceData diceData;
    
    /*
    [SerializeField] private string effect; // Store type as a string

    [Header("Visuals")]
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

    public void SetEffectType( Type effectType )
    {
        effect = effectType?.AssemblyQualifiedName; // Store full type name
    }
    */
}
