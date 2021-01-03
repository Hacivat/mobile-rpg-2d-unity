using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotBehaviour : InventorySlot
{
    public ItemBehaviour GetCurrentItem()
    {
        return currentItem;
    }

    public ItemBehaviour SetCurrentItem(ItemBehaviour item)
    {
        if (!currentItem)
        {
            RepositionNewItem(item);
            return null;
        }

        if (currentItem)
        {
            RepositionOldItem(currentItem, item.currentInventorySlot);
            RepositionNewItem(item);
            return currentItem;
        }

        return null;
    }
}
