using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedChip : ChipEffect
{
    public override string GetDescription()
    {
        return "Some other description";
    }
    public override IEnumerator ProcessEffect()
    {
        Debug.Log( "Process Red chip " );
        yield return null;
    }
}
