using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] ChipController chip;
    [SerializeField] Vector3 movementVector = Vector3.zero;
    [SerializeField] float speed = 0.1f;

    readonly int selectedNameHash = Animator.StringToHash( "Selected" );
    readonly int unselectedNameHash = Animator.StringToHash( "Unselected" );
    readonly int PopNameHash = Animator.StringToHash( "Pop" );
    
    private void Start()
    {
        selected = false;
        chip.OnSelected += Selected;
        chip.OnUnselected += Unselected;
    }

    private void Update()
    {
        IdleAnimation();
    }
    
    void IdleAnimation()
    {
        movementVector = new Vector3( Mathf.Sin( Time.time ), 0f, Mathf.Cos( Time.time ) );
        movementVector.Normalize();
        transform.Translate( movementVector * speed * Time.deltaTime );
    }

    bool selected = false;
    public void Selected()
    {
        
        if ( selected == false )
        {
            selected = true;
            Debug.Log( "Animation Selected" );
            animator.SetTrigger( selectedNameHash );
        }
    }
    public void Unselected()
    {
        if ( selected == true )
        {
            selected = false;
            Debug.Log( "Animation Unselected" );
            animator.SetTrigger( unselectedNameHash );
        }
    }
    public void Pop()
    {
        animator.SetTrigger( PopNameHash );
    }
}
