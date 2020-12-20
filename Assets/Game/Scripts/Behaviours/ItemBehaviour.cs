using UnityEngine.EventSystems;

public class ItemBehaviour : Item
{
    public ItemSO data;
    public InventorySlot currentInventorySlot;

    ItemBehaviour()
    {
        Current(this);
    }


    private void Awake()
    {
           
    }

    public void ApplyMove(InventorySlot inventorySlot) {
        if (inventorySlot)
            currentInventorySlot = inventorySlot;
    }
}
