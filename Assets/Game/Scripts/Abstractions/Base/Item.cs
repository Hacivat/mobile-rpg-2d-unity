using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private Vector3 _startPosition;
    private Vector3 _offsetToMouse;
    private float _zDistanceToCamera;

    #region Interface Implementations

    public void OnBeginDrag(PointerEventData eventData) 
    {
        Debug.Log("OnBeginDrag" + eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag" + eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag" + eventData.position);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick" + eventData.position);
    }

    #endregion
}

