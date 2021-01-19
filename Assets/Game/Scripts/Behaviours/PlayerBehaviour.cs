using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public event Action AppliedExp;
    public static PlayerBehaviour Instance { get; private set; }

    public List<ItemBehaviour> CurrentItems = new List<ItemBehaviour>();

    [Serializable]

    public struct Data
    {
        [Header("Level")]
        public int Level;
        public long Exp;

        [Header("Character Stats")]
        public int Health;
        public int Strength;
        public int Dexterity;
        public int Intellect;
        public int Armor;

        [Header("Attack Power")]
        public int MinAttack;
        public int MaxAttack;
    }

    [Header("Settings")]
    [SerializeField] private float _expToLevelPercentage;
    [SerializeField] private int _maxExp;

    [SerializeField] private Data _data;
    public Data data => _data;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    #region Public Methods

    [Button]
    public void SetExp(int value = 50)
    {
        _data.Exp += value;

        if (_data.Exp >= _maxExp)
        {
            _data.Level++;
            _maxExp += (int)Mathf.Round(_maxExp * _expToLevelPercentage);
            _data.Exp = 0;
        }

        AppliedExp?.Invoke();
    }

    public long GetExp()
    {
        return _data.Exp;
    }

    public int GetLevel()
    {
        return _data.Level;
    }

    public int GetHealth()
    {
        return _data.Health;
    }

    public void SetHealth(int value)
    {
        _data.Health += value;

        if (_data.Health <= 0)
        {
            Kill();
        }
    }

    public int GetStrength()
    {
        return _data.Strength;
    }

    public void SetStrength(int value)
    {
        _data.Strength += value;
    }

    public int GetDexterity()
    {
        return _data.Dexterity;
    }

    public void SetDexterity(int value)
    {
        _data.Dexterity += value;
    }

    public int GetIntellect()
    {
        return _data.Intellect;
    }

    public void SetIntellect(int value)
    {
        _data.Intellect += value;
    }

    public int GetMinAttack()
    {
        return _data.MinAttack;
    }

    public void SetMinAttack(int value)
    {
        _data.MinAttack += value;
    }

    public int GetMaxAttack()
    {
        return _data.MaxAttack;
    }

    public void SetMaxAttack(int value)
    {
        _data.MaxAttack += value;
    }

    public int GetArmor()
    {
        return _data.Armor;
    }

    public void SetArmor(int value)
    {
        _data.Armor += value;
    }

    public Data GetData()
    {
        return _data;
    }

    public void SetStat(Stats.Type stat, int value)
    {
        switch (stat)
        {
            case Stats.Type.Health:
                SetHealth(value);

                break;
            case Stats.Type.Strength:
                SetStrength(value);

                break;
            case Stats.Type.Dexterity:
                SetDexterity(value);

                break;
            case Stats.Type.Intellect:
                SetIntellect(value);

                break;
            case Stats.Type.MinAttack:
                SetMinAttack(value);

                break;
            case Stats.Type.MaxAttack:
                SetMaxAttack(value);

                break;
            case Stats.Type.Exp:
                SetExp(value);

                break;
            default:
                break;
        }

        //TODO: stat update triggers will activate from here. Not in slots.
    }

    public void RemoveItemEffect(ItemBehaviour item)
    {
        if (CurrentItems.Contains(item))
        {
            foreach (ItemSO.Effects effect in item.data.effects)
            {
                SetStat(effect.targetEffect, -effect.effectValue);
            }

            CurrentItems.Remove(item);
        }
    }

    public void ApplyItemEffects()
    {
        List<InventorySlotBehaviour> characterSlots = FindObjectsOfType<InventorySlotBehaviour>()
                                                      .Where(slot => slot.Type == InventorySlot.InventoryType.Character).ToList();

        foreach (InventorySlotBehaviour slot in characterSlots)
        {
            if (slot.currentItem)
            {
                if (!CurrentItems.Contains(slot.currentItem))
                {
                    foreach (ItemSO.Effects effect in slot.currentItem.data.effects)
                    {
                        SetStat(effect.targetEffect, effect.effectValue);
                    }

                    CurrentItems.Add(slot.currentItem);
                }
            }
        }
    }

    #endregion

    #region Private Methods
    private void Kill()
    {
        //Killed logic
    }

    #endregion
}
