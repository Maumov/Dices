using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    [SerializeField] GameObject[] points;


    void DisableAll()
    {
        foreach ( GameObject g in points )
        {
            g.SetActive( false );
        }
    }
    public void SetSideValue( int value )
    {
        DisableAll();
        if ( value == 1 || value == 3 || value == 5)
        {
            points[0].SetActive( true );
        }
        if ( value >= 2)
        {
            points[1].SetActive( true );
            points[2].SetActive( true );
        }
        if ( value >= 4 )
        {
            points[ 3 ].SetActive( true );
            points[ 4 ].SetActive( true );
        }
        if ( value >= 6)
        {
            points[ 5 ].SetActive( true );
            points[ 6 ].SetActive( true );
        }
    }

    
}
