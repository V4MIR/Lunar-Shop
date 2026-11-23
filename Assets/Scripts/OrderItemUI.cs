using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderItemUI : MonoBehaviour
{
    [Header("UI Элементы карточки")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI countText;

    private Order currentOrderData;
    private OrderManager manager;

    //вызывается из менеджера
    public void Setup(Order data, OrderManager orderManager)
    {
        currentOrderData = data;
        manager = orderManager;

        nameText.text = data.itemName;
        countText.text = "x " + data.amount;
    }

    //вызывается при клике на карточку
    public void OnClick()
    {
        manager.SelectOrder(currentOrderData);
    }
}