using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public event Action ItemEffectApplied;
    public enum InventoryType { Inventory, Character, Drop };
    public InventoryType Type = InventoryType.Inventory;

    [ConditionalField(nameof(Type), false, InventoryType.Character)]
    public Equipments.Type equipmentType = Equipments.Type.Weapon;

    public ItemBehaviour currentItem;
    private Camera _mainCam;
    private int _layer;
    private void Awake()
    {
        _layer = LayerMask.GetMask("InventorySlot");
        _mainCam = Camera.main;
    }

    private void Start()
    {
        if (currentItem)
        {
            currentItem = Instantiate(currentItem, transform.position, Quaternion.identity);
            currentItem.SetCurrentInventorySlot(this);
        }
    }

    public void RepositionNewItem(ItemBehaviour item)
    {
        currentItem = item;
        item.transform.position = transform.position;
        item.SetCurrentInventorySlot(this);

        ItemEffectApplied?.Invoke();
    }

    public void RepositionOldItem(ItemBehaviour oldItem, InventorySlot newInventorySlot)
    {
        newInventorySlot.currentItem = oldItem;
        oldItem.transform.position = newInventorySlot.transform.position;
        oldItem.SetCurrentInventorySlot(newInventorySlot);
    }

    public void ResetItemPosition()
    {
        currentItem.transform.position = transform.position;
    }

    #region Interface Implementations

    public void OnDrag(PointerEventData eventData)
    {
        if (!currentItem)
        {
            Debug.Log(name + " !currentItem");
            return;
        }

        Vector3 newPosition = _mainCam.ScreenToWorldPoint(eventData.position);
        newPosition.z = 0;
        currentItem.transform.position = newPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!currentItem)
        {
            Debug.Log(name + " !currentItem");
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(eventData.position), Vector2.zero, 20f, _layer);
        if (hit.collider)
        {
            InventorySlotBehaviour newInventorySlot = hit.collider.GetComponent<InventorySlotBehaviour>();

            if (currentItem.data.type != ItemSO.ItemType.Equipment && newInventorySlot.Type == InventoryType.Character)
            {
                ResetItemPosition();
                return;
            }

            if (currentItem.data.type == ItemSO.ItemType.Equipment && 
                newInventorySlot.Type == InventoryType.Character && 
                newInventorySlot.equipmentType != currentItem.data.equipmentType)
            {
                ResetItemPosition();
                return;
            }

            if (!newInventorySlot.SetCurrentItem(currentItem))
                currentItem = null;
        }
        else
        {
            ResetItemPosition();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown" + eventData.position);
    }

    #endregion
}
