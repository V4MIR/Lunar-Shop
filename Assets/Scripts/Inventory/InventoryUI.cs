using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [Header("Inventory")]
    public List<InventoryItem> items = new();

    [Header("Slots")]
    [SerializeField] private InventorySlot[] slots;

    [Header("Info Panel")]
    [SerializeField] private Image infoIcon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private TMP_Text descriptionText;

    private ItemType currentType = ItemType.Plant;

    private void OnEnable()
    {
        RefreshInventory();
        ClearInfo();
    }

    public void ShowPlants()
    {
        currentType = ItemType.Plant;
        RefreshInventory();
        ClearInfo();
    }

    public void ShowPotions()
    {
        currentType = ItemType.Potion;
        RefreshInventory();
        ClearInfo();
    }

    public void RefreshInventory()
    {
        foreach (InventorySlot slot in slots)
        {
            slot.Clear();
        }

        int index = 0;

        foreach (InventoryItem item in items)
        {
            if (item.item == null)
                continue;

            if (item.item.itemType != currentType)
                continue;

            if (item.amount <= 0)
                continue;

            if (index >= slots.Length)
                break;

            slots[index].Setup(item, this);
            index++;
        }
    }

    public void ShowInfo(InventoryItem item)
    {
        infoIcon.sprite = item.item.icon;
        infoIcon.enabled = true;

        nameText.text = item.item.itemName;
        amountText.text = "Количество: " + item.amount;
        descriptionText.text = item.item.description;
    }

    public void ClearInfo()
    {
        infoIcon.sprite = null;
        infoIcon.enabled = false;

        nameText.text = "";
        amountText.text = "";
        descriptionText.text = "";
    }

    public void AddItem(ItemData item, int amount = 1)
    {
        if (item == null)
            return;

        InventoryItem existingItem =
            items.Find(x => x.item == item);

        if (existingItem != null)
        {
            existingItem.amount += amount;
        }
        else
        {
            items.Add(new InventoryItem
            {
                item = item,
                amount = amount
            });
        }

        RefreshInventory();
        ClearInfo();
    }

    public int GetItemAmount(ItemData item)
    {
        InventoryItem existingItem =
            items.Find(x => x.item == item);

        if (existingItem != null)
        {
            return existingItem.amount;
        }

        return 0;
    }
}