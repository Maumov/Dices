using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    bool isShowing = false;
    readonly int showNameHash = Animator.StringToHash( "Show" );
    readonly int hideNameHash = Animator.StringToHash( "Hide" );

    public void Show()
    {
        if ( !isShowing)
        {
            animator.SetTrigger( showNameHash );
        }
        isShowing = true;
    }

    public void Hide()
    {
        if ( isShowing )
        {
            animator.SetTrigger( hideNameHash );
        }
        isShowing = false;
    }
}
