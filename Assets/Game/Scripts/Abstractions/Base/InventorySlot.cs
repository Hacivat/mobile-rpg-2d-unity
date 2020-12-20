using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private ItemBehaviour _currentItem;

    public virtual ItemBehaviour GetCurrentItem(ItemBehaviour item) {
        return _currentItem;
    }

    public virtual void SetCurrentItem(ItemBehaviour item) {
        if (!_currentItem) {
            Positioning(item);

            item.ApplyMove(this);
            return;
        }

        if(_currentItem.data.type == item.data.type) {
            _currentItem.data.amount += item.data.amount;

            Destroy(item);
            return;
        }

        if (_currentItem.data.type != item.data.type) {
            Positioning(item);

            item.currentInventorySlot.Positioning(_currentItem);
            item.ApplyMove(this);
            return;
        }
    }

    public virtual void Positioning(ItemBehaviour item) {
        item.transform.position = transform.position;
    }
}
