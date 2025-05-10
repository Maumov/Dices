using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour, IMoveTo
{

    [SerializeField] protected bool hasDestination = false;
    protected Vector3 destination;
    protected Vector3 currentVelocity;
    [SerializeField] protected float speed = 1f;

    public Vector3 GetDestination()
    {
        return destination;
    }

    public bool IsMoving()
    {
        return hasDestination;
    }

    public void StopMoving()
    {
        hasDestination = false;
    }

    public void MoveToPosition( Vector3 _destination )
    {
        destination = _destination;
        hasDestination = true;
    }
    public void MoveToPosition()
    {
        MoveToPosition( destination );
    }

    protected virtual void MoveToDestination()
    {
        
        if ( Vector3.Distance( transform.position, destination ) < 0.05f )
        {
            hasDestination = false;
        }
        transform.position = Vector3.SmoothDamp( transform.position, destination, ref currentVelocity, speed );
        //transform.Translate( ( destination - transform.position).normalized * speed * Time.deltaTime );
        
    }

    void Update()
    {
        if ( hasDestination )
        {
            MoveToDestination();
        }
    }

    public void MoveToPositionInstantly( Vector3 _destination )
    {
        destination = _destination;
        transform.position = destination;
    }
}
