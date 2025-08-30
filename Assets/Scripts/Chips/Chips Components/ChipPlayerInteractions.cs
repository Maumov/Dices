using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChipPlayerInteractions : MonoBehaviour
{
    
    [SerializeField] Vector3 pointOfContact = Vector3.zero;
    [SerializeField] float draggingY = 0.9f;

    [SerializeField] LayerMask layerMaskBeginDrag;

    [SerializeField] LayerMask layerMaskDragging;
    [SerializeField] float distance = 30f;

    [SerializeField] ChipController chipController;

    IMoveTo move;

    private void OnEnable()
    {
        move = GetComponent<IMoveTo>();
    }

    void Start()
    {
        /*
        EventTrigger trigger = GetComponentInChildren<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener( ( data ) => { OnDragDelegate( ( PointerEventData ) data ); } );
        trigger.triggers.Add( entry );
        */
    }

    public void OnBeginDrag( BaseEventData data )
    {
        /*
        if ( !CanBeInteracted() )
        {
            return;
        }

        move.StopMoving();
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        RaycastHit outHit = new RaycastHit();
        if ( Physics.Raycast( ray, out outHit, distance, layerMaskBeginDrag ) )
        {
            pointOfContact = transform.position - outHit.point;
        }
        */
    }

    public void OnDragDelegate( PointerEventData data )
    {
        /*
        if ( !CanBeInteracted() )
        {
            return;
        }

        move.StopMoving();
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        RaycastHit outHit = new RaycastHit();
        if ( Physics.Raycast( ray, out outHit, distance, layerMaskDragging ) )
        {
            float dis = Vector3.Distance( ray.origin, outHit.point );
            dis *= draggingY ;
            Vector3 result = ray.origin + (ray.direction * dis);
            transform.position = result + pointOfContact;
        }
        */
    }

    public void MouseOver( BaseEventData data)
    {
        
    }

    public void OnEndDrag( BaseEventData data )
    {

    }

    public void MouseDown( BaseEventData data )
    {
        if ( !CanBeInteracted() )
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData;

        if ( pointerEventData.button == PointerEventData.InputButton.Left )
        {
            Clicked();
        }
        if ( pointerEventData.button == PointerEventData.InputButton.Right )
        {
            PlayerChips playerChips = FindObjectOfType<PlayerChips>();
            playerChips.ReturnChipToPlayer( chipController );
        }
        /*
        if ( !CanBeInteracted() )
        {
            return;
        }
        move.StopMoving();
        OnSelected?.Invoke();
        */
    }

    public void Clicked()
    {
        PlayerChips playerChips = FindObjectOfType<PlayerChips>();

        playerChips.SelectChip( chipController );
    }


    public void MouseUp( BaseEventData data )
    {
        /*
        if ( !CanBeInteracted() )
        {
            return;
        }
        Debug.Log("Mouse up");
        OnUnselected?.Invoke();
        */
    }

    bool CanBeInteracted()
    {
        return chipController.CanBeInteractedWith();
    }
}
