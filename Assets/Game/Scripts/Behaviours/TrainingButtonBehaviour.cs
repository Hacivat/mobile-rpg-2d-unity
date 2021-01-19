using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrainingButtonBehaviour : MonoBehaviour, IPointerDownHandler
{
    private StatBehaviour _stat;


    private void Start()
    {
        _stat = transform.parent.Find("val").GetComponent<StatBehaviour>();
    }

    private void Train()
    {
        PlayerBehaviour.Instance.SetStat(_stat.Type, 1);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Train();
    }
}
