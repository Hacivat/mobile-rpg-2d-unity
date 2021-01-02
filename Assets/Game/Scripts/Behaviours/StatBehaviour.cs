using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatBehaviour : MonoBehaviour
{
    private TextMeshPro _value;
    public Stats.Type Type;

    private void Awake()
    {
        _value = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        SetValue();
    }

    private void SetValue()
    {
        String value = "";

        if(Type == Stats.Type.Health)
        {
            value = PlayerBehaviour.Instance.GetHealth().ToString();
        }

        if (Type == Stats.Type.Strength)
        {
            value = PlayerBehaviour.Instance.GetStrength().ToString();
        }

        if (Type == Stats.Type.Dexterity)
        {
            value = PlayerBehaviour.Instance.GetDexterity().ToString();
        }

        if (Type == Stats.Type.Intellect)
        {
            value = PlayerBehaviour.Instance.GetIntellect().ToString();
        }

        if (Type == Stats.Type.MinAttack)
        {
            value = PlayerBehaviour.Instance.GetMinAttack().ToString();
        }

        if (Type == Stats.Type.MaxAttack)
        {
            value = PlayerBehaviour.Instance.GetMaxAttack().ToString();
        }

        _value.SetText(value);
    }
}
