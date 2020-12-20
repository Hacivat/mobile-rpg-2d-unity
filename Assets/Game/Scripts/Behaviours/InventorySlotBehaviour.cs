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

    public void SetCurrentItem(ItemBehaviour item)
    {
        if (!currentItem)
        {
            if (!item.currentInventorySlot)
            {
                Instantiate(item.gameObject, transform.position, Quaternion.identity);
                return;
            }

            Positioning(item);

            item.ApplyMove(this);
            return;
        }

        if (currentItem.data.type == item.data.type)
        {
            currentItem.data.amount += item.data.amount;

            Destroy(item.transform);
            return;
        }

        if (currentItem.data.type != item.data.type)
        {
            Positioning(item);

            item.currentInventorySlot.Positioning(currentItem);
            item.ApplyMove(this);
            return;
        }
    }
}
