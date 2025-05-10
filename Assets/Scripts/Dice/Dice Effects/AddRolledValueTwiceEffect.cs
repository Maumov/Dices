using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRolledValueTwiceEffect : DiceEffect
{
    public override IEnumerator ProcessEffect()
    {
        calculationManager.AddDiceValue( dice.GetValue() );
        diceAnimation.Pop();
        yield return calculationManager.WaitForSeconds();

        calculationManager.AddDiceValue( dice.GetValue() );
        diceAnimation.Pop();
        yield return calculationManager.WaitForSeconds();
    }
    public override string GetDescription()
    {
        return "Counts the rolled value of the dice twice when scored";
    }
}
