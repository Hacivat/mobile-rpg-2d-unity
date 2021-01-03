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

        SetValue();
    }

    private void SetValue()
    {
        String value = PlayerBehaviour.Instance.GetStat(Type).ToString();
        _value.SetText(value);
    }

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
        foreach (InventorySlotBehaviour slot in _inventorySlots)
            if (slot.Type == InventorySlot.InventoryType.Character)
                slot.ItemEffectApplied -= SetValue;
    }

    private void Register()
    {
        foreach (InventorySlotBehaviour slot in _inventorySlots)
            if (slot.Type == InventorySlot.InventoryType.Character)
                slot.ItemEffectApplied += SetValue;
    }
}
