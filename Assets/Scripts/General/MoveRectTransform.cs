using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRectTransform : Move
{
    RectTransform rectTransform;

    [SerializeField] Vector3 testPosition;

    [ContextMenu("TestMoveToTestPosition")]
    void TestNewDestination()
    {
        MoveToPosition(testPosition);
    }

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    protected override void MoveToDestination()
    {
        if ( hasDestination )
        {
            if ( Vector3.Distance( rectTransform.anchoredPosition, destination ) < 1f )
            {
                hasDestination = false;
            }
            rectTransform.anchoredPosition = Vector3.SmoothDamp( rectTransform.anchoredPosition, destination, ref currentVelocity, speed );
            //transform.Translate( ( destination - transform.position).normalized * speed * Time.deltaTime );
        }
    }

    void Update()
    {
        if ( hasDestination )
        {
            MoveToDestination();
        }
    }
}
