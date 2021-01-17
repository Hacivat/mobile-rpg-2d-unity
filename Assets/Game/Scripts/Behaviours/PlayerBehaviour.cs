using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public event Action<int, int> AppliedExp;
    public static PlayerBehaviour Instance { get; private set; }

    public List<ItemBehaviour> CurrentItems = new List<ItemBehaviour>();

    [Header("Level")]
    [SerializeField] private int _level;
    [SerializeField] private int _exp;

    [Header("Character Stats")]
    [SerializeField] private int _health;
    [SerializeField] private int _strength;
    [SerializeField] private int _dexterity;
    [SerializeField] private int _intellect;
    [SerializeField] private int _armor;

    [Header("Attack Power")]
    [SerializeField] private int _minAttack;
    [SerializeField] private int _maxAttack;

    [Header("Settings")]
    [SerializeField] private float _expToLevelPercentage;
    [SerializeField] private int _maxExp;

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
        _exp += value;

        if (_exp >= _maxExp)
        {
            _level++;
            _maxExp += (int)Mathf.Round(_maxExp * _expToLevelPercentage);
            _exp = 0;
        }

        AppliedExp?.Invoke(_level, _exp);
    }

    public int GetExp()
    {
        return _exp;
    }

    public int GetLevel()
    {
        return _level;
    }

    public int GetHealth()
    {
        return _health;
    }

    public void SetHealth(int value)
    {
        _health += value;

        if (_health <= 0)
        {
            Kill();
        }
    }

    public int GetStrength()
    {
        return _strength;
    }

    public void SetStrength(int value)
    {
        _strength += value;
    }

    public int GetDexterity()
    {
        return _dexterity;
    }

    public void SetDexterity(int value)
    {
        _dexterity += value;
    }

    public int GetIntellect()
    {
        return _intellect;
    }

    public void SetIntellect(int value)
    {
        _intellect += value;
    }

    public int GetMinAttack()
    {
        return _minAttack;
    }

    public void SetMinAttack(int value)
    {
        _minAttack += value;
    }

    public int GetMaxAttack()
    {
        return _maxAttack;
    }

    public void SetMaxAttack(int value)
    {
        _maxAttack += value;
    }

    public int GetArmor()
    {
        return _armor;
    }

    public void SetArmor(int value)
    {
        _armor += value;
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

    public int GetStat(Stats.Type stat)
    {
        switch (stat)
        {
            case Stats.Type.Health:
                return GetHealth();

            case Stats.Type.Strength:
                return GetStrength();

            case Stats.Type.Dexterity:
                return GetDexterity();

            case Stats.Type.Intellect:
                return GetIntellect();

            case Stats.Type.MinAttack:
                return GetMinAttack();

            case Stats.Type.MaxAttack:
                return GetMaxAttack();

            case Stats.Type.Armor:
                return GetArmor();

            case Stats.Type.Level:
                return GetLevel();

            case Stats.Type.Exp:
                return GetExp();

            default:
                return -1;
        }
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

    private void SetAttackValues()
    {
        _minAttack = 5;
        _maxAttack = 10;
    }

    #endregion
}
