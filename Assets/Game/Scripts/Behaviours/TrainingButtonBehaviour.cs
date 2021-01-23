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
    private int _startCost;

    private void Start()
    {
        _startCost = _trainCost;
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
        _trainCost = _startCost;

        for (int i = 1; i < _stat.StatValue + 1; i++)
        {
            if(_trainCost <= 100)
            {
                _trainCost += Mathf.RoundToInt(9);
            }

            else if (_trainCost <= 1000)
            {
                _trainCost += Mathf.RoundToInt(89);
            }

            else if (_trainCost <= 25000)
            {
                _trainCost += Mathf.RoundToInt(541);
            }

            else
            {
                _trainCost += Mathf.RoundToInt(874);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Train();
    }
}
