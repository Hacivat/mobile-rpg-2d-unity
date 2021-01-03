using System;
using UnityEngine;

public class ItemBehaviour : Item
{
    public ItemSO data;
    public InventorySlot currentInventorySlot;

    public void SetCurrentInventorySlot(InventorySlot inventorySlot)
    {
        currentInventorySlot = inventorySlot;

        if(currentInventorySlot.Type == InventorySlot.InventoryType.Character)
        {
            PlayerBehaviour.Instance.ApplyItemEffects(this);
        }
    }
}
