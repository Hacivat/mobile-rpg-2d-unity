using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    private ItemBehaviour _item;

    protected void Current(ItemBehaviour item)
    {
        _item = item;
    }
}

