using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] ChipPlayerInteractions chip;
    [SerializeField] Vector3 movementVector = Vector3.zero;
    [SerializeField] float speed = 0.1f;

    readonly int selectedNameHash = Animator.StringToHash( "Selected" );
    readonly int unselectedNameHash = Animator.StringToHash( "Unselected" );
    readonly int PopNameHash = Animator.StringToHash( "Pop" );
    
    private void Start()
    {
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

    public void Selected()
    {
        animator.SetTrigger( selectedNameHash );
    }
    public void Unselected()
    {
        Debug.Log("Animation Unselected");
        animator.SetTrigger( unselectedNameHash );
    }
    public void Pop()
    {
        animator.SetTrigger( PopNameHash );
    }
}
