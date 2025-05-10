using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRolledValueEffect : DiceEffect
{
    public override IEnumerator ProcessEffect()
    {
        calculationManager.AddDiceValue( dice.GetValue() );
        diceAnimation.Pop();
        //yield return calculationManager.WaitForSeconds();
        yield return null;
    }
    public override string GetDescription()
    {
        return "Adds the rolled value of the dice when scored";
    }
}
