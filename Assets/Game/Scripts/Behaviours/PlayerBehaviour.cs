﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour Instance { get; private set; }

    [Header("Character Stats")]
    [SerializeField] private int _health;
    [SerializeField] private int _strength;
    [SerializeField] private int _dexterity;
    [SerializeField] private int _intellect;

    [Header("Attack Power")]
    private int _minAttack;
    private int _maxAttack;

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

        SetAttackValues();
    }

    #region Public Methods

    public int GetHealth()
    {
        return _health;
    }

    public void SetHealth(int value)
    {
        _health += value;
    
        if(_health <= 0)
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
        
        SetAttackValues();
    }

    public int GetDexterity()
    {
        return _dexterity;
    }

    public void SetDexterity(int value)
    {
        _dexterity += value;
        
        SetAttackValues();
    }

    public int GetIntellect()
    {
        return _intellect;
    }

    public void SetIntellect(int value)
    {
        _intellect += value;

        SetAttackValues();
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
            default:
                break;
        }
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

            default:
                return -1;
        }
    }

    public void ApplyItemEffects(ItemBehaviour item)
    {
        foreach (ItemSO.Effects effect in item.data.effects)
            SetStat(effect.targetEffect, effect.effectValue);
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
