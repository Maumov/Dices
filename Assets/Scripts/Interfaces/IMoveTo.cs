using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveTo
{
    bool IsMoving();

    void MoveToPosition( Vector3 _destination );

    void MoveToPositionInstantly( Vector3 _destination);

    void MoveToPosition();

    void StopMoving();

    Vector3 GetDestination();
}
