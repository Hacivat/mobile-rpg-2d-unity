using UnityEngine;

[CreateAssetMenu(fileName = "CommonItem", menuName = "Items/Common Item")]
public class CommonItemSO : ItemSO
{
    CommonItemSO() {
        type = ItemType.Common;
        description = "Common item.";
    }
}
