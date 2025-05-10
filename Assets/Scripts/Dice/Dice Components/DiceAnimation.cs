using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceAnimation : MonoBehaviour
{
    readonly int selectedNameHash = Animator.StringToHash( "Selected" );
    readonly int unselectedNameHash = Animator.StringToHash( "Unselected" );
    readonly int popNameHash = Animator.StringToHash( "Pop" );

    [Header( "Rotation" )]
    [SerializeField] float idleTurnSpeed = 10f;
    [SerializeField] float handSelectedTurnSpeed = 20f;
    [SerializeField] Vector3 rotationVector => new Vector3( 2f, 0.5f, 1f );


    [SerializeField] Animator animator;
    [SerializeField] DiceController dice;
    
    private void OnEnable()
    {
        dice.OnSelected += Selected;
        dice.OnUnselected += Unselected;
    }

    public void RotateAnimationInHand()
    {
        RotateAnimation( idleTurnSpeed );
    }

    public void RotateAnimationInHandSelected()
    {
        RotateAnimation( handSelectedTurnSpeed );
    }

    void RotateAnimation( float speed )
    {
        transform.Rotate( rotationVector * speed * Time.deltaTime );
    }
    public void Pop()
    {
        animator.SetTrigger( popNameHash );
    }
    public void Selected()
    {
        animator.SetTrigger( selectedNameHash );
    }
    public void Unselected()
    {
        animator.SetTrigger( unselectedNameHash );
    }
}
