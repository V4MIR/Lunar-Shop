using UnityEngine;

public class WindowsChange : MonoBehaviour
{
    [Header("Windows")]
    [SerializeField] private GameObject mainWindows;
    public GameObject gardenPlace;
    [SerializeField] private GameObject labPlace;
    [SerializeField] private GameObject ordersPlace;
    [SerializeField] private GameObject inventory;

    private GameObject[] windows;

    private void Awake()
    {
        windows = new GameObject[]
        {
            mainWindows,
            gardenPlace,
            labPlace,
            ordersPlace,
            inventory
        };
    }

    private void ShowWindow(GameObject targetWindow)
    {
        foreach (GameObject window in windows)
        {
            window.SetActive(window == targetWindow);
        }
    }

    public void ToMainWindows() => ShowWindow(mainWindows);
    public void ToGarden() => ShowWindow(gardenPlace);
    public void ToLab() => ShowWindow(labPlace);
    public void ToOrders() => ShowWindow(ordersPlace);
    public void ToInventory() => ShowWindow(inventory);
}