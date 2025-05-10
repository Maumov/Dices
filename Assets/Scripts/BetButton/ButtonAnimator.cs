using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] BetButton betButton;
    private void OnEnable()
    {
        betButton.OnBetProcessed += PlayPopAnimation;
    }

    readonly int popNameHash = Animator.StringToHash( "Pop" );
    readonly int betNameHash = Animator.StringToHash( "Bet" );

    void PlayRemoveBetAnimation()
    {
        animator.SetTrigger( betNameHash );
    }

    void PlayPopAnimation()
    {
        animator.SetTrigger( popNameHash );
    }
}
