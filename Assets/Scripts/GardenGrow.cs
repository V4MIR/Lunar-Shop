using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GardenGrow : MonoBehaviour
{
    public ResourcesChange scriptResourcesChange;
    public GameObject progressBarObj;
    //public Slider progressBar;
    public GameObject textGrowObj;
    public Button button;
    public TMP_Text buttonText;

    private int growTime = 8;
    private TMP_Text timerText;
    private bool isPlanted;
    private float timeElapsed = 0f;
    private bool isRunning = false;
    private Slider progressBar;

    // Start is called before the first frame update
    void Start()
    {
        timerText = textGrowObj.GetComponent<TMP_Text>();
        progressBar = progressBarObj.GetComponent<Slider>();
    }

    void Update()
    {
        if (isRunning)
        {
            timeElapsed += Time.deltaTime;
            UpdateTimerDisplay(timeElapsed);
        }
    }

    public void SetProgress(float currentValue, float maxValue)
    {
        progressBar.value = currentValue / maxValue;
    }


    public void PressButton()
    {
        if (!isPlanted)
        {
            button.interactable = false;
            textGrowObj.SetActive(true);
            progressBarObj.SetActive(true);
            isRunning = true;
            isPlanted = true;
        }
        else
        {
            scriptResourcesChange.ChangeGrass(2);
            buttonText.text = "Посадить";
            isPlanted = false;
        }
        
    }


    

    private void UpdateTimerDisplay(float timeToDisplay)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        if (seconds < growTime)
        {
            timerText.text = string.Format("Растёт {0:0} сек.", seconds);
            SetProgress(seconds, growTime);
        }
        else
        {
            ResetTimer();
            progressBarObj.SetActive(false);
            textGrowObj.SetActive(false);
            buttonText.text = "Собрать";
            button.interactable = true;
        }
        
    }

    public void StartTimer() => isRunning = true;
    public void PauseTimer() => isRunning = false;
    public void ResetTimer()
    {
        isRunning = false;
        timeElapsed = 0f;
        UpdateTimerDisplay(timeElapsed);
    }
}
