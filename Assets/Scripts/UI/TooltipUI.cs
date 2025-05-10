using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public Vector3 offset;
    ITexts texts;

    static bool shouldShowTooltip = true;
    private void OnEnable()
    {
        texts = GetComponent<ITexts>();
    }

    private void OnMouseEnter()
    {
        if ( !shouldShowTooltip )
        {
            return;
        }
        MouseOverElementUI.instance.Show( texts.GetTitle(), texts.GetDescription(), transform.position + offset );
    }

    private void OnMouseDown()
    {
        MouseOverElementUI.instance.Hide();
        shouldShowTooltip = false;   
    }
    private void OnMouseUp()
    {
        shouldShowTooltip = true;
    }

    private void OnMouseExit()
    {
        MouseOverElementUI.instance.Hide();
    }

}
