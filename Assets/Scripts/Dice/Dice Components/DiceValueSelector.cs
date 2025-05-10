using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceValueSelector : MonoBehaviour
{
    [SerializeField] int[] diceValues = { 1, 2, 3, 4, 5, 6 };

    [SerializeField] DiceVisuals diceVisuals;

    private void OnEnable()
    {
        diceVisuals = GetComponent<DiceVisuals>();
    }

    public void SetupSides( DiceData diceData)
    {
        diceValues = diceData.diceValues;
        diceVisuals.UpdateValues( diceValues );
    }

    [SerializeField] float minAngleToFindValueRolled = 1f;
    public int GetValueRolled()
    {
        int valueRolled = 0;

        if ( Vector3.Angle( transform.up, Vector3.up ) < minAngleToFindValueRolled )
        {
            valueRolled = diceValues[ 0 ];
        }
        if ( Vector3.Angle( -transform.up, Vector3.up ) < minAngleToFindValueRolled )
        {
            valueRolled = diceValues[ 1 ];
        }
        if ( Vector3.Angle( transform.right, Vector3.up ) < minAngleToFindValueRolled )
        {
            valueRolled = diceValues[ 2 ];
        }
        if ( Vector3.Angle( -transform.right, Vector3.up ) < minAngleToFindValueRolled )
        {
            valueRolled = diceValues[ 3 ];
        }
        if ( Vector3.Angle( transform.forward, Vector3.up ) < minAngleToFindValueRolled )
        {
            valueRolled = diceValues[ 4 ];
        }
        if ( Vector3.Angle( -transform.forward, Vector3.up ) < minAngleToFindValueRolled )
        {
            valueRolled = diceValues[ 5 ];
        }
        return valueRolled;
    }

}
