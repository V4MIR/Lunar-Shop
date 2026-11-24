using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private ItemData itemTea;

    public ResourcesChange scriptResourcesChange;

    [Header("Список заказов (Справа)")]
    public GameObject orderItemPrefab;
    public Transform contentParent;

    [Header("Панель деталей (Слева)")]
    public TextMeshProUGUI detailNameText;
    public TextMeshProUGUI detailDescText;
    public GameObject completeButton;

    private List<Order> orders = new List<Order>();
    private Order selectedOrder;

    private void Start()
    {
        completeButton.GetComponent<Button>().onClick.AddListener(CompleteSelectedOrder);
        completeButton.SetActive(false);

        Order order_1 = new Order();
        order_1.itemName = "Мятный чай";
        order_1.amount = 1;
        order_1.description = "15 монет";
        order_1.rewardMoney = 15;

        AddOrder(order_1);
        AddOrder(order_1);
        AddOrder(order_1);
        AddOrder(order_1);
        AddOrder(order_1);
        AddOrder(order_1);
        AddOrder(order_1);
    }

    public void AddOrder(Order newOrder)
    {
        orders.Add(newOrder);
        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        foreach (Order order in orders)
        {
            GameObject item = Instantiate(orderItemPrefab, contentParent);
            item.GetComponent<OrderItemUI>().Setup(order, this);
        }
    }

    public void SelectOrder(Order orderToSelect)
    {
        selectedOrder = orderToSelect;

        detailNameText.text = orderToSelect.itemName;
        detailDescText.text = "\nКоличество: " + orderToSelect.amount + "\nНаграда: " + orderToSelect.description;

        completeButton.SetActive(true);
    }

    private void CompleteSelectedOrder()
    {
        if (selectedOrder != null && inventoryUI.GetItemAmount(itemTea) >= selectedOrder.amount)
        {
            scriptResourcesChange.ChangeMoney(selectedOrder.rewardMoney);
            inventoryUI.AddItem(itemTea, -selectedOrder.amount);
            orders.Remove(selectedOrder);

            detailNameText.text = "";
            detailDescText.text = "";
            completeButton.SetActive(false);
            selectedOrder = null;

            UpdateUI();
        }
    }
}