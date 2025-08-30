using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public enum OrganizationMode
    {
        None,
        StackX,
        StackZ,
        Circle
    }
    [SerializeField] OrganizationMode organizationMode = OrganizationMode.StackX;

    [SerializeField] List<GameObject> currentItems;
    
    [SerializeField] Transform originPosition;
    [SerializeField] float spacing = 1f;

    public delegate void ItemContainerDelegate( GameObject go);
    public event ItemContainerDelegate OnItemAdded, OnItemRemoved;

    public void NewItemAdded( GameObject newItem )
    {
        currentItems.Add( newItem );
        newItem.transform.SetParent( originPosition );
        SetItemPosition();
        OnItemAdded?.Invoke( newItem );
    }

    public void SimpleAdd( GameObject item)
    {
        currentItems.Add( item );
        OnItemAdded?.Invoke( item );
    }

    public void SimpleRemove( GameObject item )
    {
        currentItems.Remove( item );
        OnItemRemoved?.Invoke( item );

    }

    public void RemoveItem( GameObject item )
    {
        currentItems.Remove( item );
        SetItemPosition();
        OnItemRemoved?.Invoke( item );
    }

    public void ReOrganizeItems()
    {
        SetItemPosition();
    }

    void SetItemPosition()
    {
        for ( int i = 0 ; i < currentItems.Count ; i++ )
        {
            currentItems[ i ].GetComponent<IMoveTo>().MoveToPosition( ItemPosition( i ) );
        }
    }

    int chipsCount;
    float x,y, z, angle;

    Vector3 ItemPosition( int i)
    {
        Vector3 position = originPosition.position;
        switch ( organizationMode )
        {
            case OrganizationMode.None:
                break;
            case OrganizationMode.StackZ:
                position = originPosition.position + new Vector3( 0f, 0f, i * spacing );
                break;
            case OrganizationMode.Circle:
                chipsCount = currentItems.Count;
                angle = ( 360f / chipsCount ) * i * Mathf.Deg2Rad;
                x = Mathf.Cos( angle );
                y = Mathf.Sin( angle );
                Vector3 result = new Vector3( x, 0f, y );
                position = originPosition.position + ( result * spacing );
                break;
            case OrganizationMode.StackX:
                x = ( spacing * i ) - ( ( currentItems.Count / 2f ) * spacing );
                position = originPosition.transform.position + new Vector3( x, 0f, 0f );
                break;
        }
        return position;
    }

    public void DestroyItems()
    {
        for ( int i = currentItems.Count - 1 ; i >= 0 ; i-- )
        {
            if ( currentItems[i] != null )
            {
                Destroy( currentItems[ i ] );
            }
        }
        currentItems = new List<GameObject>();
    }

    public bool HasItems()
    {
        return currentItems.Count > 0;
    }

    public GameObject GetItem( int index)
    {
        if ( index >= currentItems.Count)
        {
            index = currentItems.Count - 1;
        }
        if ( currentItems.Count == 0)
        {
            return null;
        }
        return currentItems[ index ];
    }

}
