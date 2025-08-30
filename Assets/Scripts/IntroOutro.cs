using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroOutro : MonoBehaviour
{

    [SerializeField] Image image;
    [Header( "Intro" )]
    [SerializeField] float in_Duration;
    [SerializeField] Color int_StartColor;
    [SerializeField] Color int_endColor;
    [Header( "Outro" )]
    [SerializeField] float out_Duration;
    [SerializeField] Color out_StartColor;
    [SerializeField] Color out_endColor;

    public delegate void introOutro_Callback();

    public void Intro( introOutro_Callback callback )
    {
        StartCoroutine( Animate( in_Duration, int_StartColor, int_endColor, callback ) );
    }

    public void Outro( introOutro_Callback callback)
    {
        StartCoroutine( Animate( out_Duration, out_StartColor, out_endColor, callback ) );
    }

    IEnumerator Animate(float animationTime, Color startColor, Color endColor, introOutro_Callback callback )
    {
        float currentTime = 0f;

        image.color = startColor;

        while( currentTime < animationTime  )
        {
            currentTime += Time.deltaTime;
            Color deltaColor = Color.Lerp( startColor, endColor, currentTime / animationTime );
            image.color = deltaColor;
            yield return null;
        }
        image.color = endColor;
        yield return null;
        callback();
    }

}
