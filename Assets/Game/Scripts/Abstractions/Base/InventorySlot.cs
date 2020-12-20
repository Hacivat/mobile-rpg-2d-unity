using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public ItemBehaviour currentItem;
    private Camera _mainCam;
    private void Awake()
    {
        _mainCam = Camera.main;

        if (currentItem)
            currentItem = Instantiate(currentItem, transform.position, Quaternion.identity);

    }
    
    public virtual void Positioning(ItemBehaviour item) {
        item.transform.position = transform.position;
    }

    #region Interface Implementations

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag" + eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = _mainCam.ScreenToWorldPoint(eventData.position);
        newPosition.z = 0;
        currentItem.transform.position = newPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit hit;
        int layer = LayerMask.GetMask("InventorySlot");
        Ray ray = _mainCam.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out hit, 20, layer))
        {
            if (hit.collider)
            {
                InventorySlotBehaviour inventorySlot = hit.collider.GetComponent<InventorySlotBehaviour>();
                inventorySlot.SetCurrentItem(currentItem);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown" + eventData.position);
    }

    #endregion
}
