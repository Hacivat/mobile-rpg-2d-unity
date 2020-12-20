using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public ItemBehaviour currentItem;
    private Camera _mainCam;
    private void Awake()
    {
        _mainCam = Camera.main;

        if (currentItem)
        {
            currentItem = Instantiate(currentItem, transform.position, Quaternion.identity);
            currentItem.SetCurrentInventorySlot(this);
        }
    }
    
    public virtual void Positioning(ItemBehaviour item) {
        item.transform.position = transform.position;
        item.SetCurrentInventorySlot(this);
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
        int layer = LayerMask.GetMask("InventorySlot");
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(eventData.position), Vector2.zero, 20f, layer);
        if (hit.collider)
        {
            InventorySlotBehaviour inventorySlot = hit.collider.GetComponent<InventorySlotBehaviour>();
            inventorySlot.SetCurrentItem(currentItem);
        }
        else
        {
            currentItem.transform.position = currentItem.currentInventorySlot.transform.position;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown" + eventData.position);
    }

    #endregion
}
