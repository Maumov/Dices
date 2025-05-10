using System;
using System.Collections;
using UnityEngine;

public interface IEffect
{
    public delegate void IEffectDelegate();
    public event IEffectDelegate OnEffectProcessed;
    public IEnumerator ProcessEffect();
    public string GetDescription();
    public static String ToHex( Color c )
    {
        string result = string.Format( "#{0:x}{1:x}{2:x}", ( int ) ( c.r * 255 ), ( int ) ( c.g * 255 ), ( int ) ( c.b * 255 ) );
        return result;
    }
}
