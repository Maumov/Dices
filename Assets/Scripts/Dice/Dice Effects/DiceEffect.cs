using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceEffect : MonoBehaviour, IEffect
{
    protected CalculationManager calculationManager;
    protected DiceController dice;
    protected DiceAnimation diceAnimation;

    public event IEffect.IEffectDelegate OnEffectProcessed;

    private void Start()
    {
        dice = GetComponent<DiceController>();
        calculationManager = FindObjectOfType<CalculationManager>();
        diceAnimation = GetComponent<DiceAnimation>();
    }
    public virtual IEnumerator ProcessEffect()
    {
        yield return null;
    }

    public virtual string GetDescription()
    {
        return "description";
    }
}
