using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject ShopItemPrefab;
    [SerializeField] Transform ShopItemsContainer;
    [SerializeField] Camera camera;
    [SerializeField] int shopItemsQuantity;
    [SerializeField] GameFlowInteractions gameFlowInteractions;
    [SerializeField] GameFlowManager gameFlowController;
    [SerializeField] ChipsManager chipsManager;
    [SerializeField] DicesManager dicesManager;
    [SerializeField] ItemContainer shopItemsContainer;
    [SerializeField] PlayerState playerState;

    private void OnEnable()
    {
        playerState.OnMoneyUpdated += MoneyUpdated;
    }

    private void OnDisable()
    {
        playerState.OnMoneyUpdated -= MoneyUpdated;
    }

    [ContextMenu("TestGenerateSingleItem")]
    public void TestCreateSingleShopItem()
    {
        CreateShopItem();
    }

    void CreateShopItem()
    {
        GameObject newShopItem = Instantiate( ShopItemPrefab );
        GameObject item = SelectItemToSellAtShop();
        newShopItem.GetComponent<ShopItem>().InitShopItem( item, camera );
        newShopItem.transform.SetParent( ShopItemsContainer );
        RectTransform rect = newShopItem.GetComponent<RectTransform>();
        rect.localRotation = Quaternion.identity;
        rect.localScale = Vector3.one;
        rect.localPosition = Vector3.zero;
        shopItemsContainer.SimpleAdd( newShopItem );
    }


    public void InitShopItems()
    {
        shopItemsContainer.DestroyItems();
        StartCoroutine( GenerateShopItems() );
    }

    public void EndShopItems()
    {
        shopItemsContainer.DestroyItems();
    }

    IEnumerator GenerateShopItems()
    {
        for ( int i = 0 ; i < shopItemsQuantity ; i++ )
        {
            CreateShopItem();
            yield return null;
        }
        shopItemsContainer.ReOrganizeItems();
    }


    GameObject SelectItemToSellAtShop()
    {
        int dicesCount = dicesManager.GetAllDicesCount();
        int chipsCount = chipsManager.GetAllChipsCount();
        int allItemsCount = dicesCount + chipsCount;
        int randomItem = Random.Range( 0, allItemsCount );
        return randomItem < dicesCount ? dicesManager.GetDice( randomItem ) : chipsManager.GetChip( randomItem % dicesCount );
    }


    public void Continue()
    {
        gameFlowInteractions.GoToNextLevel();
    }

    public void BuyItem( ShopItem itemToBuy)
    {
        if ( CanBuyItem( itemToBuy ) )
        {
            DeductMoney( itemToBuy );
            shopItemsContainer.RemoveItem( itemToBuy.gameObject );
            itemToBuy.GiveItem();
        }
    }

    bool CanBuyItem( ShopItem itemToBuy )
    {
        PlayerState playerState = FindObjectOfType<PlayerState>();
        int itemPrice = itemToBuy.GetItemBuyPrice();
        return playerState.HasAmount( itemPrice );
    }
    void DeductMoney( ShopItem itemToBuy)
    {
        PlayerState playerState = FindObjectOfType<PlayerState>();
        int itemPrice = itemToBuy.GetItemBuyPrice();
        playerState.RemoveCoins( itemPrice );
    }

    public void SellItem( ItemController item )
    {
        PlayerState playerState = FindObjectOfType<PlayerState>();
        int coinValueOfItem = item.GetItemSellPrice();
        playerState.AddCoins( coinValueOfItem );
    }

    void MoneyUpdated()
    {
        ShopItem[] items = FindObjectsOfType<ShopItem>();
        foreach ( ShopItem item in items )
        {
            item.CheckBuyRequirements();
        }
    }

}
