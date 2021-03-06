using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class StatBehaviour : MonoBehaviour
{
    [SerializeField] private bool _isFieldBase = false;
    private TextMeshPro _value;
    private List<InventorySlotBehaviour> _inventorySlots;
    public Stats.Type Type;

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

    public int GetTextValue()
    {
        return Convert.ToInt32(_value.text);
    }

    private void SetStatValues()
    {
        _value.SetText(Value());
    }

    private void SetLevelValues()
    {
        _value.SetText(Type == Stats.Type.Level ? PlayerBehaviour.Instance.baseData.Level.ToString() : PlayerBehaviour.Instance.baseData.Exp.ToString());
    }

    private string Value()
    {
        string result = "---";

        if (_isFieldBase)
            switch (Type)
        {
            case Stats.Type.Level:
                result = PlayerBehaviour.Instance.baseData.Level.ToString();
                break;

            case Stats.Type.Exp:
                result = PlayerBehaviour.Instance.baseData.Exp.ToString();
                break;
            
            case Stats.Type.Health:
                result = PlayerBehaviour.Instance.baseData.Health.ToString();
                break;

            case Stats.Type.Strength:
                result = PlayerBehaviour.Instance.baseData.Strength.ToString();
                break;
            
            case Stats.Type.Dexterity:
                result = PlayerBehaviour.Instance.baseData.Dexterity.ToString();
                break;
            
            case Stats.Type.Intellect:
                result = PlayerBehaviour.Instance.baseData.Intellect.ToString();
                break;
            
            case Stats.Type.MinAttack:
                result = PlayerBehaviour.Instance.baseData.MinAttack.ToString();
                break;
            
            case Stats.Type.MaxAttack:
                result = PlayerBehaviour.Instance.baseData.MaxAttack.ToString();
                break;
            
            case Stats.Type.Armor:
                result = PlayerBehaviour.Instance.baseData.Armor.ToString();
                break;

            case Stats.Type.Gold:
                result = PlayerBehaviour.Instance.baseData.Gold.ToString();
                break;

            default:
                break;
        }

        else
            switch (Type)
            {
                case Stats.Type.Level:
                    result = PlayerBehaviour.Instance.baseData.Level.ToString();
                    break;

                case Stats.Type.Exp:
                    result = PlayerBehaviour.Instance.baseData.Exp.ToString();
                    break;

                case Stats.Type.Health:
                    result = PlayerBehaviour.Instance.currentData.Health.ToString();
                    break;

                case Stats.Type.Strength:
                    result = PlayerBehaviour.Instance.currentData.Strength.ToString();
                    break;

                case Stats.Type.Dexterity:
                    result = PlayerBehaviour.Instance.currentData.Dexterity.ToString();
                    break;

                case Stats.Type.Intellect:
                    result = PlayerBehaviour.Instance.currentData.Intellect.ToString();
                    break;

                case Stats.Type.MinAttack:
                    result = PlayerBehaviour.Instance.currentData.MinAttack.ToString();
                    break;

                case Stats.Type.MaxAttack:
                    result = PlayerBehaviour.Instance.currentData.MaxAttack.ToString();
                    break;

                case Stats.Type.Armor:
                    result = PlayerBehaviour.Instance.currentData.Armor.ToString();
                    break;

                case Stats.Type.Gold:
                    result = PlayerBehaviour.Instance.baseData.Gold.ToString();
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
                PlayerBehaviour.Instance.ItemEffectApplied -= SetStatValues;

        else
            PlayerBehaviour.Instance.AppliedExp -= SetLevelValues;

    }

    private void Register()
    {
        if (Type != Stats.Type.Exp && Type != Stats.Type.Level)
            foreach (InventorySlotBehaviour slot in _inventorySlots)
                PlayerBehaviour.Instance.ItemEffectApplied += SetStatValues;

        else
            PlayerBehaviour.Instance.AppliedExp += SetLevelValues;
    }

    #endregion
}
