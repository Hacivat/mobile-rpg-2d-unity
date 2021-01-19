using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class StatBehaviour : MonoBehaviour
{
    private TextMeshPro _value;
    public Stats.Type Type;
    private List<InventorySlotBehaviour> _inventorySlots;

    private void Awake()
    {
        _value = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        _inventorySlots = FindObjectsOfType<InventorySlotBehaviour>().ToList();
        Register();

        SetStatValues();
    }

    private void SetStatValues()
    {
        _value.SetText(Value());
    }

    private void SetLevelValues()
    {
        _value.SetText(Type == Stats.Type.Level ? PlayerBehaviour.Instance.data.Level.ToString() : PlayerBehaviour.Instance.data.Exp.ToString());
    }

    private string Value()
    {
        string result = "---";
        switch (Type)
        {
            case Stats.Type.Level:
                result = PlayerBehaviour.Instance.data.Level.ToString();
                break;

            case Stats.Type.Exp:
                result = PlayerBehaviour.Instance.data.Exp.ToString();
                break;
            
            case Stats.Type.Health:
                result = PlayerBehaviour.Instance.data.Health.ToString();
                break;

            case Stats.Type.Strength:
                result = PlayerBehaviour.Instance.data.Strength.ToString();
                break;
            
            case Stats.Type.Dexterity:
                result = PlayerBehaviour.Instance.data.Dexterity.ToString();
                break;
            
            case Stats.Type.Intellect:
                result = PlayerBehaviour.Instance.data.Intellect.ToString();
                break;
            
            case Stats.Type.MinAttack:
                result = PlayerBehaviour.Instance.data.MinAttack.ToString();
                break;
            
            case Stats.Type.MaxAttack:
                result = PlayerBehaviour.Instance.data.MaxAttack.ToString();
                break;
            
            case Stats.Type.Armor:
                result = PlayerBehaviour.Instance.data.Armor.ToString();
                break;
            
            default:
                break;
        }

        return result;
    }

    #region Subscriptions

    private void OnDisable()
    {
        Unregister();
    }

    private void OnDestroy()
    {
        Unregister();
    }

    private void Unregister()
    {
        if (Type != Stats.Type.Exp && Type != Stats.Type.Level)
            foreach (InventorySlotBehaviour slot in _inventorySlots)
                slot.ItemEffectApplied -= SetStatValues;

        else
            PlayerBehaviour.Instance.AppliedExp -= SetLevelValues;

    }

    private void Register()
    {
        if (Type != Stats.Type.Exp && Type != Stats.Type.Level)
            foreach (InventorySlotBehaviour slot in _inventorySlots)
                slot.ItemEffectApplied += SetStatValues;

        else
            PlayerBehaviour.Instance.AppliedExp += SetLevelValues;
    }

    #endregion
}
