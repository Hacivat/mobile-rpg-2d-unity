using UnityEngine.EventSystems;

public class ItemBehaviour : Item
{
    public ItemSO data;
    public InventorySlot currentInventorySlot;

    public void SetCurrentInventorySlot(InventorySlot inventorySlot)
    {
        currentInventorySlot = inventorySlot;
    }
}
