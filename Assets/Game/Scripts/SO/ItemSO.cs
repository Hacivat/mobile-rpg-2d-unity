using MyBox;
using UnityEngine;

public abstract class ItemSO : ScriptableObject {
    public enum ItemType { Common, Equipment }

    [System.Serializable]
    public struct Effects
    {
        public Stats.Type targetEffect;
        public int effectValue;
    }
    public Effects[] effects;

    public ItemType type;

    [ConditionalField(nameof(type), false, ItemType.Equipment)]
    public Equipments.Type equipmentType;

    public string itemName;
    public int amount = 1;
    public string description;
    public float weight = .0f;
}

