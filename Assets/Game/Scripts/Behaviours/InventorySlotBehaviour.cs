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

    public bool SetCurrentItem(ItemBehaviour item)
    {
        if (!currentItem)
        {
            Repositioning(item, this);
            return true;
        }

        if (currentItem)
        {
            currentItem.SetCurrentInventorySlot(item.currentInventorySlot);
            
            Repositioning(currentItem, item.currentInventorySlot);
            Repositioning(item, this);
            return true;
        }

        return false;
    }
}
