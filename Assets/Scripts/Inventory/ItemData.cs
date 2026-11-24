using UnityEngine;

public enum ItemType
{
    Plant,
    Potion
}

[CreateAssetMenu(
    fileName = "NewItem",
    menuName = "Inventory/Item"
)]
public class ItemData : ScriptableObject
{
    public string itemName;

    [TextArea(3, 6)]
    public string description;

    public Sprite icon;
    public ItemType itemType;
}