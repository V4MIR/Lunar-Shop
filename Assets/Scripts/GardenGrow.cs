using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GardenGrow : MonoBehaviour
{
    public ResourcesChange scriptResourcesChange;

    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private ItemData itemMint;

    [SerializeField] private Sprite plantFirstStage;
    [SerializeField] private Sprite plantSecondStage;
    private float growTime = 8f;
    private int bedPrice = 15;
    private int harvestAmount = 2;

    private enum BedState { Locked, Empty, Growing, Ready }

    private class Bed
    {
        public Button button;
        public TMP_Text buttonText;
        public TMP_Text timerText;
        public Slider slider;
        public Image plant;
        public GameObject lockObject;
        public BedState state;
        public float elapsed;
    }

    private readonly Bed[] beds = new Bed[6];

    private void Start()
    {
        WindowsChange windows = GetComponent<WindowsChange>();
        if (windows == null)
        {
            enabled = false;
            return;
        }

        if (scriptResourcesChange == null)
            scriptResourcesChange = GetComponent<ResourcesChange>();

        Transform garden = windows.gardenPlace.transform;
        Transform template = garden.Find("Bed 1/Plant 1");

        for (int i = 0; i < beds.Length; i++)
        {
            Transform root = garden.Find("Bed " + (i + 1));
            Transform plant = root.Find("Plant 1");
            if (plant == null)
            {
                plant = Instantiate(template.gameObject, root, false).transform;
                plant.name = "Plant 1";
            }
            plant.SetAsFirstSibling();
            Transform lockTransform = root.Find("Lock");
            Bed bed = new Bed
            {
                button = root.Find("Button").GetComponent<Button>(),
                timerText = root.Find("TextGrow").GetComponent<TMP_Text>(),
                slider = root.Find("Slider").GetComponent<Slider>(),
                plant = plant.GetComponent<Image>(),
                lockObject = lockTransform != null ? lockTransform.gameObject : null,
                state = i == 0 ? BedState.Empty : BedState.Locked
            };
            bed.buttonText = bed.button.GetComponentInChildren<TMP_Text>(true);
            bed.plant.raycastTarget = false;
            bed.slider.minValue = 0f;
            bed.slider.maxValue = 1f;
            bed.slider.wholeNumbers = false;
            bed.slider.interactable = false;
            if (bed.slider.handleRect != null)
                bed.slider.handleRect.gameObject.SetActive(false);

            int index = i;
            bed.button.onClick = new Button.ButtonClickedEvent();
            bed.button.onClick.AddListener(() => PressButton(index));
            beds[i] = bed;
            Refresh(bed);
        }
    }

    private void Update()
    {
        foreach (Bed bed in beds)
        {
            if (bed == null) continue;
            if (bed.state == BedState.Locked)
            {
                bed.button.interactable = scriptResourcesChange.GetMoney() >= bedPrice;
            }
            else if (bed.state == BedState.Growing)
            {
                bed.elapsed = Mathf.Min(bed.elapsed + Time.deltaTime, growTime);
                UpdateProgress(bed);
                if (bed.elapsed >= growTime)
                {
                    bed.state = BedState.Ready;
                    Refresh(bed);
                }
            }
        }
    }

    private void PressButton(int index)
    {
        Bed bed = beds[index];
        switch (bed.state)
        {
            case BedState.Locked:
                if (scriptResourcesChange.GetMoney() < bedPrice) return;
                scriptResourcesChange.ChangeMoney(-bedPrice);
                bed.state = BedState.Empty;
                break;
            case BedState.Empty:
                bed.elapsed = 0f;
                bed.state = BedState.Growing;
                break;
            case BedState.Growing:
                return;
            case BedState.Ready:
                inventoryUI.AddItem(itemMint, harvestAmount);
                bed.elapsed = 0f;
                bed.state = BedState.Empty;
                break;
        }
        Refresh(bed);
    }

    private void Refresh(Bed bed)
    {
        bool locked = bed.state == BedState.Locked;
        bool growing = bed.state == BedState.Growing;
        bool ready = bed.state == BedState.Ready;

        if (bed.lockObject != null) bed.lockObject.SetActive(locked);
        bed.plant.sprite = ready ? plantSecondStage : plantFirstStage;
        bed.plant.gameObject.SetActive(growing || ready);
        bed.button.gameObject.SetActive(!growing);
        bed.button.interactable = !locked || scriptResourcesChange.GetMoney() >= bedPrice;
        bed.slider.gameObject.SetActive(growing);
        bed.timerText.gameObject.SetActive(locked || growing);
        bed.buttonText.text = locked ? "Купить" : ready ? "Собрать" : "Посадить";
        if (locked) bed.timerText.text = $"За {bedPrice} монет";
        if (growing) UpdateProgress(bed);
        else bed.slider.value = 0f;
    }

    private void UpdateProgress(Bed bed)
    {
        bed.slider.value = Mathf.Clamp01(bed.elapsed / Mathf.Max(0.01f, growTime));
        int remaining = Mathf.CeilToInt(Mathf.Max(0f, growTime - bed.elapsed));
        bed.timerText.text = $"Растёт {remaining} сек.";
    }
}