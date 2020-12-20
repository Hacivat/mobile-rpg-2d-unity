using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    private Camera _mainCam;

    private ItemBehaviour _item;

    #region Interface Implementations

    
    #endregion

    protected void Current(ItemBehaviour item)
    {
        _item = item;
    }
}

