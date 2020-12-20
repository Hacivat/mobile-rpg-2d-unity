public class ItemBehaviour : Item
{
    public ItemSO data;
    public InventorySlot currentInventorySlot;

    public void ApplyMove(InventorySlot inventorySlot) {
        currentInventorySlot = inventorySlot;
    }
}
