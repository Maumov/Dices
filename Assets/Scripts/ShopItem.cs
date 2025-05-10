using System.Collections;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] GameObject original;
    [SerializeField] GameObject itemGameObject;
    [SerializeField] Transform itemPosition;
    [SerializeField] IMoveTo moveTo;

    [SerializeField] Camera camera;
    [SerializeField] float itemDistanceFromCamera;

    [SerializeField] int itemBuyPrice;
    [SerializeField] int itemSellPrice;

    public void InitShopItem( GameObject item, Camera cam)
    {
        original = item;
        camera = cam;
        moveTo = GetComponent<IMoveTo>();
        StartCoroutine( ShowItem( item ) );
    }

    IEnumerator ShowItem( GameObject item)
    {
        itemGameObject = Instantiate(item);
        //itemGameObject.transform.SetParent(itemPosition);
        ItemController d = itemGameObject.GetComponent<ItemController>();
        d.SetupItem();
        SetItemPosition();
        itemBuyPrice = d.GetItemBuyPrice();
        itemSellPrice = d.GetItemSellPrice();
        //itemGameObject.transform.scale = Vector3.one;
        yield return null;
    }
    [ContextMenu("ReDoItemPosition")]
    void SetItemPosition()
    {
        itemGameObject.GetComponent<IMoveTo>().MoveToPositionInstantly( transform.position + RepositionItemGameObject() );   
    }

    private void Update()
    {
        if ( moveTo.IsMoving() )
        {
            SetItemPosition();    
        }
    }

    Vector3 RepositionItemGameObject()
    {
        Vector3 position = ( camera.transform.position - transform.position ).normalized * itemDistanceFromCamera;
        //position.y = itemYPosition;
        Debug.DrawRay( transform.position, position, Color.red, 2f );
        return position;
    }

    public void StartBuyItem()
    {
        ShopManager shopManager = FindObjectOfType<ShopManager>();
        shopManager.BuyItem( this );
    }

    public void GiveItem()
    {
        ItemController d = original.GetComponent<ItemController>();
        d.GiveItem();
        Destroy( gameObject );
    }

    public int GetItemBuyPrice()
    {
        return itemBuyPrice; 
    }

    private void OnDestroy()
    {
        if ( itemGameObject != null )
        {
            Destroy( itemGameObject );
        }
    }
}
