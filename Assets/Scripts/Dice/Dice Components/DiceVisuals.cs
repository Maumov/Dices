using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DiceVisuals : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI[] valuesUI;
    [SerializeField] DiceSide[] diceSides;
    [SerializeField] MeshRenderer meshRenderer;
    public void SetupVisuals( DiceData diceData )
    {
        for ( int i = 0 ; i < diceData.diceValues.Length ; i++ )
        {
            valuesUI[ i ].text = diceData.diceValues[ i ].ToString();
            diceSides[i].SetSideValue( diceData.diceValues[ i ] );
        }

        //meshRenderer.material.color = diceData.color;
    }

    public void UpdateValues( int[] newValues)
    {
        for ( int i = 0 ; i < newValues.Length ; i++ )
        {
            valuesUI[ i ].text = newValues[ i ].ToString();
        }
    }
}
