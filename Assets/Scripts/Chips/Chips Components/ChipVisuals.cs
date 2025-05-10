using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipVisuals : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;

    public void SetupVisuals( ChipData diceData )
    {
        meshRenderer.material.color = diceData.color;
    }

}
