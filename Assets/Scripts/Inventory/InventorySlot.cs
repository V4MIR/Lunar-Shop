using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private Button button;

    private InventoryItem currentItem;
    private InventoryUI inventoryUI;

    public void Setup(
        InventoryItem item,
        InventoryUI ui)
    {
        currentItem = item;
        inventoryUI = ui;

        itemIcon.sprite = item.item.icon;
        itemIcon.enabled = true;

        amountText.text = item.amount.ToString();

        button.interactable = true;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    public void Clear()
    {
        currentItem = null;

        itemIcon.sprite = null;
        itemIcon.enabled = false;

        amountText.text = "";

        button.interactable = false;
        button.onClick.RemoveAllListeners();
    }

    private void OnClick()
    {
        if (currentItem != null)
        {
            inventoryUI.ShowInfo(currentItem);
        }
    }
}