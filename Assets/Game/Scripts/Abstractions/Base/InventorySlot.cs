using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public enum InventoryType { Inventory, Character, Drop };
    [SerializeField] private InventoryType type = InventoryType.Inventory;

    public ItemBehaviour currentItem;
    private Camera _mainCam;
    private int _layer;
    private void Awake()
    {
        _layer = LayerMask.GetMask("InventorySlot");
        _mainCam = Camera.main;

        if (currentItem)
        {
            currentItem = Instantiate(currentItem, transform.position, Quaternion.identity);
            currentItem.SetCurrentInventorySlot(this);
        }
    }
    
    public void RepositionNewItem(ItemBehaviour item) {
        currentItem = item;
        item.transform.position = transform.position;
        item.SetCurrentInventorySlot(this);
    }

    public void RepositionOldItem(ItemBehaviour oldItem, InventorySlot newInventorySlot)
    {
        newInventorySlot.currentItem = oldItem;
        oldItem.transform.position = newInventorySlot.transform.position;
        oldItem.SetCurrentInventorySlot(newInventorySlot);
    }

    private void ResetItemPosition()
    {
        currentItem.transform.position = currentItem.currentInventorySlot.transform.position;
    }

    #region Interface Implementations

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = _mainCam.ScreenToWorldPoint(eventData.position);
        newPosition.z = 0;
        currentItem.transform.position = newPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(eventData.position), Vector2.zero, 20f, _layer);
        if (hit.collider)
        {
            InventorySlotBehaviour inventorySlot = hit.collider.GetComponent<InventorySlotBehaviour>();
            if(!inventorySlot.SetCurrentItem(currentItem))
                currentItem = null;
        }
        else
        {
            ResetItemPosition();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown" + eventData.position);
    }

    #endregion
}
