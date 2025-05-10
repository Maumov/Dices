using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemController : MonoBehaviour
{
    public abstract void SetupItem();
    public abstract void SellItem();

    public abstract void GiveItem();

    public abstract int GetItemBuyPrice();
    public abstract int GetItemSellPrice();

}
