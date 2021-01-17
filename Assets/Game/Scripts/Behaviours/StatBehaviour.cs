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
        String value = PlayerBehaviour.Instance.GetStat(Type).ToString();
        _value.SetText(value);
    }

    private void SetLevelValues(int level, int exp)
    {
        String value = PlayerBehaviour.Instance.GetStat(Type).ToString();
        _value.SetText(Type == Stats.Type.Level ? level.ToString() : exp.ToString());
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
