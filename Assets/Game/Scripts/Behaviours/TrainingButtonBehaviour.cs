using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrainingButtonBehaviour : MonoBehaviour, IPointerDownHandler
{
    private StatBehaviour _stat;
    private TextMeshPro _text;
    private int _trainCost = 5;
    
    private void Start()
    {
        _stat = transform.parent.Find("val").GetComponent<StatBehaviour>();
        _text = transform.Find("Text").GetComponent<TextMeshPro>();
        
        CalculateCost();
        _text.SetText(_trainCost.ToString());
    }

    private void Train()
    {
        PlayerBehaviour.Instance.SetStat(_stat.Type, 1);
        //PlayerBehaviour.Instance.SetStat(Stats.Type.Gold, -_trainCost);
        CalculateCost();
        _text.SetText(_trainCost.ToString());
    }

    private void CalculateCost()
    {
        for (int i = 1; i < _stat.StatValue + 1; i++)
        {
            _trainCost += Mathf.RoundToInt(_trainCost * .8f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Train();
    }
}
