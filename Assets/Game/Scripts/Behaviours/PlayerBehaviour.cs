using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public event Action ItemEffectApplied;

    public event Action AppliedExp;
    public static PlayerBehaviour Instance { get; private set; }

    public List<ItemBehaviour> CurrentItems = new List<ItemBehaviour>();

    [Serializable]
    public struct BaseData
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

        [Header("Currency")]
        public long Gold;
    }

    public struct CurrentData
    {
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

    [SerializeField] private CurrentData _currentData;
    [SerializeField] private BaseData _baseData;
    public CurrentData currentData => _currentData;
    public BaseData baseData => _baseData;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);

        Initialize();
    }

    private void Initialize()
    {
        SetHealth(0);
        SetStrength(0);
        SetDexterity(0);
        SetIntellect(0);
        SetArmor(0);
        SetMaxAttack(0);
        SetMinAttack(0);
    }

    #region Public Methods

    [Button]
    public void SetExp(int value = 50)
    {
        _baseData.Exp += value;

        if (_baseData.Exp >= _maxExp)
        {
            _baseData.Level++;
            _maxExp += (int)Mathf.Round(_maxExp * _expToLevelPercentage);
            _baseData.Exp = 0;
        }

        AppliedExp?.Invoke();
    }

    public void SetHealth(int value, bool isBase = false)
    {
        if (isBase)
            _baseData.Health += value;
        else
        {
            if (value < 0)
                _currentData.Health += value;
            else
                _currentData.Health = baseData.Health + value;
        }

        if (_currentData.Health <= 0)
            Kill();
    }

    public void SetStrength(int value, bool isBase = false)
    {
        if (isBase)
            _baseData.Strength += value;
        else
        {
            if (value < 0)
                _currentData.Strength += value;
            else
                _currentData.Strength = baseData.Strength + value;
        }
    }

    public void SetDexterity(int value, bool isBase = false)
    {
        if (isBase)
            _baseData.Dexterity += value;
        else
        {
            if (value < 0)
                _currentData.Dexterity += value;
            else
                _currentData.Dexterity = baseData.Dexterity + value;
        }
    }

    public void SetIntellect(int value, bool isBase = false)
    {
        if (isBase)
            _baseData.Intellect += value;
        else
        {
            if (value < 0)
                _currentData.Intellect += value;
            else
                _currentData.Intellect = baseData.Intellect + value;
        }
    }

    public void SetMinAttack(int value, bool isBase = false)
    {
        if (isBase)
            _baseData.MinAttack += value;
        else
        {
            if (value < 0)
                _currentData.MinAttack += value;
            else
                _currentData.MinAttack = baseData.MinAttack + value;
        }
    }

    public void SetMaxAttack(int value, bool isBase = false)
    {
        if (isBase)
            _baseData.MaxAttack += value;
        else
        {
            if (value < 0)
                _currentData.MaxAttack += value;
            else
                _currentData.MaxAttack = baseData.MaxAttack + value;
        }
    }

    public void SetArmor(int value, bool isBase = false)
    {
        if (isBase)
            _baseData.Armor += value;
        else
        {
            if (value < 0)
                _currentData.Armor += value;
            else
                _currentData.Armor = baseData.Armor + value;
        }
    }

    public void SetGold(long value)
    {
        _baseData.Gold += value;
    }

    public void SetStat(Stats.Type stat, int value, bool isBase = false)
    {
        switch (stat)
        {
            case Stats.Type.Health:
                SetHealth(value, isBase);

                break;
            case Stats.Type.Strength:
                SetStrength(value, isBase);

                break;
            case Stats.Type.Dexterity:
                SetDexterity(value, isBase);

                break;
            case Stats.Type.Intellect:
                SetIntellect(value, isBase);

                break;
            case Stats.Type.MinAttack:
                SetMinAttack(value, isBase);

                break;
            case Stats.Type.MaxAttack:
                SetMaxAttack(value, isBase);

                break;
            case Stats.Type.Exp:
                SetExp(value);

                break;
            default:
                break;
        }

        ItemEffectApplied?.Invoke();
    }

    public void RemoveItemEffect(ItemBehaviour item)
    {
        if (CurrentItems.Contains(item))
        {
            foreach (ItemSO.Effects effect in item.data.effects)
                SetStat(effect.targetEffect, -effect.effectValue);

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
                        SetStat(effect.targetEffect, effect.effectValue);

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
