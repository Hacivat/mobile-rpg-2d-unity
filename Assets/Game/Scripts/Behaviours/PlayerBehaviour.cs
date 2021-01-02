using System.Collections;
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
