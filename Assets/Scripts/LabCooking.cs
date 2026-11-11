using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LabCooking : MonoBehaviour
{
    public ResourcesChange scriptResourcesChange;
    public GameObject progressBarObj;
    public float timeElapsed = 0f;
    public bool isRunning = false;
    public GameObject textCookingObj;
    public Button button;
    public TMP_Text buttonText;
    public GameObject needTextObj;

    private int cookingTime = 8;
    private TMP_Text timerText;
    private Slider progressBar;
    private bool isCooked;

    // Start is called before the first frame update
    void Start()
    {
        timerText = textCookingObj.GetComponent<TMP_Text>();
        progressBar = progressBarObj.GetComponent<Slider>();
    }

    // Update is called once per frame
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
        if (!isCooked)
        {
            if (scriptResourcesChange.GetGrass() >= 2)
            {
                needTextObj.SetActive(false);
                progressBarObj.SetActive(true);
                scriptResourcesChange.ChangeGrass(-2);
                button.interactable = false;
                textCookingObj.SetActive(true);
                isRunning = true;
                isCooked = true;
            }
        }
        else
        {
            needTextObj.SetActive(true);
            scriptResourcesChange.ChangeTea(1);
            buttonText.text = "Варить";
            isCooked = false;
        }

    }




    private void UpdateTimerDisplay(float timeToDisplay)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        if (seconds < cookingTime)
        {
            timerText.text = string.Format("Варится {0:0} сек.", seconds);
            SetProgress(seconds, cookingTime);
        }
        else
        {
            ResetTimer();
            textCookingObj.SetActive(false);
            progressBarObj.SetActive(false);
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
