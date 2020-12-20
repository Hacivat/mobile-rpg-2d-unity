using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSO : ScriptableObject {
    public enum ItemType { Common, Equipment }

    public ItemType type;
    public string itemName;
    public int amount = 1;
    public string description;
    public float weight = .0f;
}
