using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChipEffect : MonoBehaviour, IEffect
{
    public event IEffect.IEffectDelegate OnEffectProcessed;

    public virtual string GetDescription()
    {
        return "description";
    }

    public virtual IEnumerator ProcessEffect()
    {
        Debug.Log( "Process chip " );
        yield return null;
    }
}
